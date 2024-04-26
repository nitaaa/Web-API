using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksUdemy.API.CustomActionFilters;
using NZWalksUdemy.API.Models.Domain;
using NZWalksUdemy.API.Models.DTO;
using NZWalksUdemy.API.Repositories;

namespace NZWalksUdemy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO request)
        {
            validateFileUpload(request);

            if( ModelState.IsValid )
            {
                var imageDM = new Image()
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.File.FileName,
                    Description = request.Description
                };

                await imageRepository.Upload(imageDM);

                return Ok(imageDM);
            }

            return BadRequest(ModelState);
        }

        private void validateFileUpload(ImageUploadRequestDTO request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName))) {
                ModelState.AddModelError("file", "Unsupported file extension");
            }
            if (request.File.Length > 10485760) {
                ModelState.AddModelError("file", "File size is larger than 10MB");
            }



        }
    }
}
