using AutoMapper;
using KHQ.Domain;
using KHQ.Domain.DTOs;
using KHQ.Repo.UOW;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Image = KHQ.Domain.Entities.Image;

namespace KHQ.Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommonController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpPost("SaveImages")]
        public async Task<IActionResult> SaveImages(List<IFormFile> images, [FromForm] int imageType, [FromForm] Guid? fKey = null ,[FromForm] string existingImages = "", [FromForm] string sortOrderData = "")
        {
            try
            {
                List<string> existingImagePaths = new List<string>();
                SortOrderData sortOrder = null;

                if (!string.IsNullOrEmpty(existingImages))
                {
                    existingImagePaths = ParseExistingImages(existingImages);
                }

                // Parse sort order data if provided
                if (!string.IsNullOrEmpty(sortOrderData))
                {
                    sortOrder = ParseSortOrderData(sortOrderData);
                }

                if (fKey != Guid.Empty)
                {
                    // Delete unwanted old images (those not in the sort order data)
                    if (sortOrder != null)
                    {
                        var imagesToKeep = sortOrder.ExistingImages.Select(ei => ei.Path).ToList();
                        await DeleteOldImages(imagesToKeep, fKey );
                    }
                    else
                    {
                        // Fallback to existing logic if no sort order data
                        await DeleteOldImages(existingImagePaths, fKey);
                    }
                }
                else
                {
                    fKey = Guid.NewGuid();
                }

                var newImagesToSave = new List<Image>();
                string[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray().Select(c => c.ToString()).ToArray();
                string folderPath = Path.Combine("wwwroot", "Images");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                // Handle existing images with sort order
                if (sortOrder != null && sortOrder.ExistingImages.Any())
                {
                    var existingImagesInDb = new List<Image>();
                    if (fKey != null)
                    {
                        existingImagesInDb = await _unitOfWork.Repository<Image>()
                            .Queryable()
                            .Where(x => x.F_Key == fKey && sortOrder.ExistingImages.Select(ei => ei.Path).Contains(x.PathLink))
                            .ToListAsync();
                    }
                    else
                    {
                        existingImagesInDb = await _unitOfWork.Repository<Image>()
                            .Queryable()
                            .Where(x => sortOrder.ExistingImages.Select(ei => ei.Path).Contains(x.PathLink))
                            .ToListAsync();
                    }

                    foreach (var existingImageData in sortOrder.ExistingImages)
                    {
                        var existingImage = existingImagesInDb.FirstOrDefault(x => x.PathLink == existingImageData.Path);
                        if (existingImage != null)
                        {
                            existingImage.Sort = existingImageData.SortOrder;
                            _unitOfWork.Repository<Image>().Update(existingImage);
                        }
                    }
                }
                else if (existingImagePaths.Any())
                {
                    // Fallback to sequential ordering if no sort order data
                    var existingImagesInDb = await _unitOfWork.Repository<Image>()
                        .Queryable()
                        .Where(x => x.F_Key == fKey && existingImagePaths.Contains(x.PathLink))
                        .ToListAsync();

                    for (int i = 0; i < existingImagePaths.Count; i++)
                    {
                        var existingImage = existingImagesInDb.FirstOrDefault(x => x.PathLink == existingImagePaths[i]);
                        if (existingImage != null)
                        {
                            existingImage.Sort = i;
                            _unitOfWork.Repository<Image>().Update(existingImage);
                        }
                    }
                }

                // Add new uploaded images with proper sort order
                if (images != null && images.Count > 0)
                {
                    for (int i = 0; i < images.Count; i++)
                    {
                        var id = Guid.NewGuid();
                        var file = images[i];
                        if (file.Length > 0)
                        {
                            int sortIndex = 0;

                            // Determine sort index based on sort order data
                            if (sortOrder != null && sortOrder.NewImageOrder.Any())
                            {
                                var newImageOrder = sortOrder.NewImageOrder.OrderBy(nio => nio.SortOrder).ToList();
                                if (i < newImageOrder.Count)
                                {
                                    sortIndex = newImageOrder[i].SortOrder;
                                }
                                else
                                {
                                    // If we have more images than sort order data, continue sequentially
                                    var maxExistingSort = sortOrder.ExistingImages.Any() ? sortOrder.ExistingImages.Max(ei => ei.SortOrder) : -1;
                                    var maxNewSort = newImageOrder.Max(nio => nio.SortOrder);
                                    sortIndex = Math.Max(maxExistingSort, maxNewSort) + 1 + (i - newImageOrder.Count);
                                }
                            }
                            else
                            {
                                // Fallback: place new images after existing ones
                                var maxExistingSort = existingImagePaths.Count - 1;
                                sortIndex = maxExistingSort + 1 + i;
                            }

                            string fileExtension = Path.GetExtension(file.FileName);
                            string sortedName = alphabet.Length > sortIndex ? alphabet[sortIndex] : $"Image_{sortIndex}";
                            string newFileName = $"{sortedName}_{id}{fileExtension}";
                            string fullPath = Path.Combine(folderPath, newFileName);

                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            newImagesToSave.Add(new Image
                            {
                                Id = id,
                                F_Key = fKey ?? Guid.NewGuid(),
                                PathLink = $"/Images/{newFileName}",
                                ImageType = (ImageType)imageType,
                                ImageName = newFileName,
                                Sort = sortIndex
                            });
                        }
                    }
                }

                // Save only new images (existing ones are already updated)
                if (newImagesToSave.Any())
                {
                    await _unitOfWork.Repository<Image>().AddRange(newImagesToSave);
                }

                await _unitOfWork.SaveChangesAsync();

                // Get total count for response
                var totalCount = (sortOrder?.ExistingImages.Count ?? existingImagePaths.Count) + newImagesToSave.Count;

                return Ok(new { success = true, count = totalCount, fkey = fKey });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        private SortOrderData ParseSortOrderData(string sortOrderData)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sortOrderData))
                    return null;

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<SortOrderData>(sortOrderData, options);
            }
            catch (JsonException)
            {
                return null;
            }
        }

        private List<string> ParseExistingImages(string existingImages)
        {
            var result = new List<string>();

            if (string.IsNullOrWhiteSpace(existingImages))
                return result;

            // Trim whitespace
            existingImages = existingImages.Trim();

            // Check if it looks like JSON (starts with [ and ends with ])
            if (existingImages.StartsWith("[") && existingImages.EndsWith("]"))
            {
                try
                {
                    result = JsonSerializer.Deserialize<List<string>>(existingImages);
                    return result;
                }
                catch (JsonException)
                {
                    // If JSON parsing fails, fall through to string parsing
                }
            }

            // Parse as delimited string - try common delimiters
            char[] delimiters = { ',', ';', '|', '\n', '\r' };

            result = existingImages
                .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                .Select(path => path.Trim())
                .Where(path => !string.IsNullOrWhiteSpace(path))
                .ToList();

            return result;
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(Guid id, Guid fKey)
        {
            if (id == Guid.Empty || fKey == Guid.Empty)
                return BadRequest("Invalid parameters");

            var image = await _unitOfWork.Repository<Image>().Queryable().Where(i => i.Id == id && i.F_Key == fKey).FirstOrDefaultAsync();
            if (image == null)
                return NotFound("Image not found");

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", image.PathLink.TrimStart('/'));
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            _unitOfWork.Repository<Image>().Delete(image);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new { success = true });
        }

        private async Task<bool> DeleteOldImages(List<string> existingImagePaths , Guid? fKey = null)
        {
            try
            {
                var oldImages = new List<Image>();
                if (fKey != null)
                {
                    oldImages = await _unitOfWork.Repository<Image>().Queryable().Where(x => x.F_Key == fKey).ToListAsync();
                }
                else
                {
                    oldImages = await _unitOfWork.Repository<Image>().Queryable().ToListAsync();
                }

                bool anyDeleted = false;

                foreach (var image in oldImages)
                {
                    // Compare against the PathLink (database path) not the file system path
                    if (!existingImagePaths.Contains(image.PathLink))
                    {
                        // Delete the physical file
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", image.PathLink.TrimStart('/'));
                        if (System.IO.File.Exists(filePath))
                            System.IO.File.Delete(filePath);

                        // Delete from database
                        await _unitOfWork.Repository<Image>().Delete(image);
                        anyDeleted = true;
                    }
                }

                // Save changes once after all deletions
                if (anyDeleted)
                {
                    await _unitOfWork.SaveChangesAsync();
                }

                return anyDeleted;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SortOrderData
    {
        public List<ExistingImageSort> ExistingImages { get; set; } = new List<ExistingImageSort>();
        public List<NewImageSort> NewImageOrder { get; set; } = new List<NewImageSort>();
    }

    public class ExistingImageSort
    {
        public string Path { get; set; }
        public int SortOrder { get; set; }
    }

    public class NewImageSort
    {
        public string PhotoId { get; set; }
        public int SortOrder { get; set; }
    }
}
