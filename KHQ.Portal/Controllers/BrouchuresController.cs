using KHQ.Domain;
using KHQ.Domain.Entities;
using KHQ.Domain.ViewModel;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Mvc;

namespace KHQ.Portal.Controllers
{
    public class BrouchuresController : Controller
    {
        private readonly IBrouchuresSrv _BrouchuresSrv;
        private readonly IImageService _imageService;
        private readonly IWebHostEnvironment _env;

        public BrouchuresController(IBrouchuresSrv brouchuresSrv, IImageService imageService, IWebHostEnvironment env)
        {
            _imageService = imageService;
            _BrouchuresSrv = brouchuresSrv;
            _env = env;
        }
        
        public async Task<IActionResult> Index()
        {
            IEnumerable<BrouchuresVM> brouchuresData = await _BrouchuresSrv.GetAllAsync();
            return View(brouchuresData);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var brochure = await _BrouchuresSrv.GetByIdAsync(id);
            if (brochure == null) return NotFound();

            return Ok(new
            {
                brochure.Id,
                FileUrl = Url.Action("Download", "Brouchures", new { id = brochure.Id })
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] BrouchuresVM brouchuresVM, [FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Please upload a PDF file.");

            // Check file type (optional but recommended)
            if (!file.ContentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Only PDF files are allowed.");

            // Get the upload path (inside wwwroot)
            string sharedFolder = Path.Combine(_env.ContentRootPath, "..", "SharedImages");
            if (!Directory.Exists(sharedFolder))
                Directory.CreateDirectory(sharedFolder);

            // Create unique file name
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(sharedFolder, fileName);

            // Save file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Save to database using your service
            brouchuresVM.FileName = $"{file.FileName}";
            brouchuresVM.FilePath = $"/{sharedFolder}/{fileName}";
            var result = await _BrouchuresSrv.AddAsync(brouchuresVM);

            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest("Error while saving brouchure.");
            }
        }

        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromForm] BrouchuresVM brouchuresVM, [FromForm] IFormFile file)
        {
            var existing = await _BrouchuresSrv.GetByIdAsync(brouchuresVM.Id);
            if (existing == null)
                return NotFound("Brochure not found.");

            // Update file if a new one is uploaded
            if (file != null && file.Length > 0)
            {
                if (!file.ContentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase))
                    return BadRequest("Only PDF files are allowed.");

                string sharedFolder = Path.Combine(_env.ContentRootPath, "..", "SharedImages");
                if (!Directory.Exists(sharedFolder))
                    Directory.CreateDirectory(sharedFolder);

                // Delete old file
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), existing.FilePath.TrimStart('/'));
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);

                // Save new file
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var filePath = Path.Combine(sharedFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                brouchuresVM.FileName = $"{file.FileName}";
                brouchuresVM.FilePath = $"/SharedImages/{file.FileName}";
            }
            else
            {
                // Keep old file path if no new file
                brouchuresVM.FilePath = existing.FilePath;
                brouchuresVM.FileName = existing.FileName;
            }

            var result = await _BrouchuresSrv.UpdateAsync(brouchuresVM);
            if (result > 0)
                return Ok("Brochure updated successfully.");

            return BadRequest("Error while updating brochure.");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _BrouchuresSrv.DeleteAsync(id);
            if (result > 0)
            {
                var res = await _imageService.DeleteImagesAsync(id);
                if (res > 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("Download/{id}")]
        public async Task<IActionResult> Download(Guid id)
        {
            var brouchure = await _BrouchuresSrv.GetByIdAsync(id);
            if (brouchure == null)
                return NotFound("Brouchure not found.");

            // Get the physical path of the file
            string sharedFolder = Path.Combine(_env.ContentRootPath, "..", "SharedImages");
            if (!Directory.Exists(sharedFolder))
                Directory.CreateDirectory(sharedFolder);
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), sharedFolder, brouchure.FilePath.TrimStart('/'));

            if (!System.IO.File.Exists(fullPath))
                return NotFound("File not found on server.");

            var fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
            var fileName = Path.GetFileName(fullPath);

            // Return file for browser download
            return File(fileBytes, "application/pdf", fileName);
        }
    }
}
