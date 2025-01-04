using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalk.API.Models.Domin;
using NZWalk.API.Models.DTO;
using NZWalk.API.Repositories;

namespace NZWalk.API.Controllers
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
        //Post: /api/Images/Uplode
        [HttpPost]
        [Route("Uplode")]
        public async Task<IActionResult> Uplode([FromForm] ImageUplodeRequestDto requestDto)
        {
            ValidateFileUplode(requestDto);
            if(ModelState.IsValid)
            {
                // Convert Dto to Domin Model
                var ImageDominModel = new Image
                {
                    File = requestDto.File,
                    FileDescription = requestDto.FileDescription,
                    FileExtension = Path.GetExtension(requestDto.File.FileName),
                    FileName = requestDto.FileName,
                    FileZiseInBytes = requestDto.File.Length
                };
                // Use Repository to uplode image 
                await imageRepository.Uplode(ImageDominModel);

                return Ok(ImageDominModel);
            }
            return BadRequest(ModelState);
        }
        private void ValidateFileUplode(ImageUplodeRequestDto requestDto)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };
            if(!allowedExtension.Contains(Path.GetExtension(requestDto.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported File Extension");
            }
            if(requestDto.File.Length > 10485760)
            {
                ModelState.AddModelError("file",  "File Zise more than 10MB, please uplode a smaller size file.");
            }
        }
    }
}
