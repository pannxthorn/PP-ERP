using MediatR;
using Microsoft.AspNetCore.Mvc;
using PP_ERP.Application.Upload.Commands;

namespace PP_ERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController(ISender sender) : ControllerBase
    {
        [HttpPost("image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var result = await sender.Send(new CommandUploadImage
            {
                FileStream = file.OpenReadStream(),
                FileName = file.FileName,
                ContentType = file.ContentType
            });
            return Ok(result);
        }
    }
}
