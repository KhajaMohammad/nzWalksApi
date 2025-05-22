namespace nzWalksApi.Repositories
{
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using nzWalksApi.Data;
    using nzWalksApi.Models.Domain;

    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment; // Provides information about the web hosting environment.
        private readonly IHttpContextAccessor httpContextAccessor; // Provides access to the current HTTP context.
        private readonly NzWalksDbContext nzWalksDbContext; // The DbContext for interacting with the database.

        // Constructor to inject dependencies
        public LocalImageRepository(
            NzWalksDbContext nzWalksDbContext, // Injecting the database context to interact with the "images" table.
            IWebHostEnvironment webHostEnvironment, // Injecting the hosting environment to access physical directories.
            IHttpContextAccessor httpContextAccessor // Injecting HTTP context accessor to build URLs.
        )
        {
            this.webHostEnvironment = webHostEnvironment; // Initializes the environment for file path operations.
            this.httpContextAccessor = httpContextAccessor; // Initializes the HTTP context to build full URL.
            this.nzWalksDbContext = nzWalksDbContext; // Initializes the DbContext for saving image data to DB.
        }

        // Method for uploading an image
        public async Task<Image> Upload(Image image)
        {
            // Construct the local file path where the image will be saved on the server
            var localFilePath = Path.Combine(
                webHostEnvironment.ContentRootPath, // The root path of the application (e.g., "C:/wwwroot").
                "Images", // The "Images" folder where images will be stored.
                $"{image.FileName}{image.FileExtension}" // The file name and extension.
            );

            // Open a FileStream to write the uploaded file to the server's file system.
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream); // Asynchronously copy the uploaded file to the local file system.

            // Build the full URL path for the uploaded image that will be accessible via the browser
            var urlFilePath =
                $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            // Set the FilePath property of the Image object to the URL where it can be accessed.
            image.FilePath = urlFilePath;

            // Add the image record to the database
            await nzWalksDbContext.image.AddAsync(image);
            await nzWalksDbContext.SaveChangesAsync(); // Save changes to the database asynchronously.

            return image; // Return the image object, which now contains its path and other data.
        }
    }
}
