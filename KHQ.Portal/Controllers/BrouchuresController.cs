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

        public async Task<IActionResult> GetById(Guid id)
        {
            var brouchuresData = await _BrouchuresSrv.GetByIdAsync(id);
            return View(brouchuresData);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
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
            brouchuresVM.FilePath = $"/uploads/brouchures/{fileName}";
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
        [HttpPost]
        public async Task<IActionResult> Update(BrouchuresVM brouchuresVM)
        {
            var result = await _BrouchuresSrv.UpdateAsync(brouchuresVM);
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest();
            }
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
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", brouchure.FilePath.TrimStart('/'));

            if (!System.IO.File.Exists(fullPath))
                return NotFound("File not found on server.");

            var fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
            var fileName = Path.GetFileName(fullPath);

            // Return file for browser download
            return File(fileBytes, "application/pdf", fileName);
        }
    }
}
