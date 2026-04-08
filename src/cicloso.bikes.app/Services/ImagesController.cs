using Azure.Storage.Files.Shares;
using Microsoft.AspNetCore.Mvc;


namespace cicloso.bikes.app.Services
{
    [ApiController]
    [Route("api/images")]
    public class ImagesController : ControllerBase
    {
        string connectionString;
        string shareName;

        public ImagesController(IConfiguration configuration)
        {
            connectionString = configuration["ConnectionStrings:bikesStorage"];
            shareName = configuration["AzureStorage:fileShareName"];

        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> Get(string fileName)
        {
            var serviceClient = new ShareClient(connectionString, shareName);
            var directoryClient = serviceClient.GetRootDirectoryClient();
            var fileClient = directoryClient.GetFileClient(fileName);

            if (!await fileClient.ExistsAsync()) 
                return NotFound();

            var downloadInfo = await fileClient.DownloadAsync();

            // Copy content to memory stream
            var stream = new MemoryStream();
            await downloadInfo.Value.Content.CopyToAsync(stream);
            stream.Position = 0;

            var contentType = Path.GetExtension(fileName).ToLower() switch
            {
                ".png" => "image/png",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".gif" => "image/gif",
                _ => "application/octet-stream"
            };

            return File(stream, contentType);
        }
    }
}
