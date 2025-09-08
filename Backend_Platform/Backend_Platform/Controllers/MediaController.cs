
using Backend_Platform.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Platform.Controllers
{
    

    
        [ApiController]
    [Route("api/[controller]")]
    [Authorize]
        public class MediaController : ControllerBase
        {
            private IMediaService _mediaService;

            public MediaController(IMediaService mediaService)
            {
                _mediaService = mediaService;
            }

        [RequestFormLimits(MultipartBodyLengthLimit = 1073741824)] // 1 GB
        [RequestSizeLimit(1073741824)]
        [HttpPost]
            public ActionResult<MediaRecords.UploadMediaResult> Media(IFormFile media)
            {
                var uploadedFileUrl = _mediaService.UploadMedia(media);

                if (uploadedFileUrl == null)
                {
                    return Problem("Uploading media failed");
                }

                return Ok(new MediaRecords.UploadMediaResult(uploadedFileUrl));
            }
        }
    public class MediaRecords
    {
        public record UploadMediaResult(string FileUri);

    }
}

    

