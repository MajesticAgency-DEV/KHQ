using AutoMapper;
using KHQ.Domain;
using KHQ.Domain.DTOs;
using KHQ.Domain.Entities;
using KHQ.Repo.UOW;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
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
        public async Task<IActionResult> SaveImages(List<IFormFile> images, [FromForm] Guid fKey, [FromForm] int imageType, [FromForm] string existingImages = "")
        {
            try
            {
                List<string> existingImagePaths = new List<string>();

                if (!string.IsNullOrEmpty(existingImages))
                {
                    // Try to determine if it's JSON or a delimited string
                    existingImagePaths = ParseExistingImages(existingImages);
                }

                if (fKey != Guid.Empty)
                {
                    // delete the old images
                    await DeleteOldImages(fKey, existingImagePaths);
                }
                else
                {
                    fKey = Guid.NewGuid();
                }

                // Parse existing images if provided
                var savedImages = new List<Image>();
                string[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray().Select(c => c.ToString()).ToArray();
                string folderPath = Path.Combine("wwwroot", "Images");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                int sortIndex = 0;

                // First, add existing images to maintain order
                foreach (var existingPath in existingImagePaths)
                {
                    var id = Guid.NewGuid();
                    savedImages.Add(new Image
                    {
                        Id = id,
                        F_Key = fKey,
                        PathLink = existingPath,
                        ImageType = (ImageType)imageType, // Convert int to enum
                        ImageName = Path.GetFileName(existingPath),
                        Sort = sortIndex++
                    });
                }

                // Then add new uploaded images
                if (images != null && images.Count > 0)
                {
                    for (int i = 0; i < images.Count; i++)
                    {
                        var id = Guid.NewGuid();
                        var file = images[i];
                        if (file.Length > 0)
                        {
                            string fileExtension = Path.GetExtension(file.FileName);
                            string sortedName = alphabet.Length > sortIndex ? alphabet[sortIndex] : $"Image_{sortIndex}";
                            string newFileName = $"{sortedName}_{id}{fileExtension}";
                            string fullPath = Path.Combine(folderPath, newFileName);

                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            savedImages.Add(new Image
                            {
                                Id = id,
                                F_Key = fKey,
                                PathLink = $"/Images/{newFileName}",
                                ImageType = (ImageType)imageType, // Convert int to enum
                                ImageName = newFileName,
                                Sort = sortIndex++
                            });
                        }
                    }
                }

                if (savedImages.Any())
                {
                    await _unitOfWork.Repository<Image>().AddRange(savedImages);
                    await _unitOfWork.SaveChangesAsync();
                }

                return Ok(new { success = true, count = savedImages.Count, fkey = fKey });
            }
            catch (Exception ex)
            {
                throw ex;
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


        [HttpPost]
        public async Task<IActionResult> UpdateImageSort([FromBody] List<ImageSortUpdateDto> sortedImages)
        {
            if (sortedImages == null || !sortedImages.Any())
                return BadRequest("Invalid sort data.");

            var imageIds = sortedImages.Select(i => i.Id).ToList();
            var images = await _unitOfWork.Repository<Image>().Queryable().Where(i => imageIds.Contains(i.Id)).ToListAsync();

            foreach (var update in sortedImages)
            {
                var image = images.FirstOrDefault(i => i.Id == update.Id);
                if (image != null)
                {
                    image.Sort = update.Sort;
                }
            }

            await _unitOfWork.SaveChangesAsync();

            return Ok(new { success = true });
        }

        private async Task<bool> DeleteOldImages(Guid fKey, List<string> existingImagePaths)
        {
            try
            {
                var oldImages = await _unitOfWork.Repository<Image>().Queryable().Where(x => x.F_Key == fKey).ToListAsync();
                foreach (var image in oldImages)
                {
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", image.PathLink.TrimStart('/'));
                    if(!existingImagePaths.Contains(filePath))
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);

                    await _unitOfWork.Repository<Image>().Delete(image);
                    await _unitOfWork.SaveChangesAsync();
                }
                var result = 1;
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
