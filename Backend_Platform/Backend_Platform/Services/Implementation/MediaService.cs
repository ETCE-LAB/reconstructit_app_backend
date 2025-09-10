using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Backend_Platform.Services.Implementation
{
    public class MediaService:IMediaService
    {
        public string? UploadMedia(IFormFile media)
        {
            var connectionString = "CUSTOMCONSTR_reconstructitBlob";
            if (connectionString == null)
            {
                return null;
            }

            try
            {
                var blobServiceClient = new BlobServiceClient(connectionString);
                var containerClient = blobServiceClient.GetBlobContainerClient("media");
                containerClient.CreateIfNotExists(PublicAccessType.Blob);

                var blobClient = containerClient.GetBlobClient($"{Guid.NewGuid()}-{media.FileName.Replace(" ", "")}");
                var blobHttpHeader = new BlobHttpHeaders { };
                if (media.FileName.EndsWith("pdf"))
                {
                    blobHttpHeader.ContentType = "application/pdf";
                }
                else if (media.FileName.EndsWith("jpg"))
                {
                    blobHttpHeader.ContentType = "image/jpg";
                }
                else if (media.FileName.EndsWith("png"))
                {
                    blobHttpHeader.ContentType = "image/png";
                }
                else if (media.FileName.EndsWith("stl", StringComparison.OrdinalIgnoreCase))
                {
                    blobHttpHeader.ContentType = "application/sla";
                }

                blobClient.Upload(media.OpenReadStream(), new BlobUploadOptions { HttpHeaders = blobHttpHeader });

                return blobClient.Uri.AbsoluteUri;
            }
            catch
            {
                return null;
            }
        }
    }
}
