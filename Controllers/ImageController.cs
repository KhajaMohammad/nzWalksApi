using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nzWalksApi.Models.Domain;
using nzWalksApi.Models.DTO;
using nzWalksApi.Repositories;

namespace nzWalksApi.Controllers
{
    [Route("api/[controller]")] // Define the base route for the controller.
    [ApiController] // Mark the controller as an API controller for automatic binding of model.
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository imageRepository; // Declare a private field for the image repository.

        // Constructor that accepts the image repository, which handles business logic for images.
        public ImageController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        // POST: /api/images/upload
        [HttpPost]
        [Route("upload")] // Define the route for the image upload endpoint.
        public async Task<IActionResult> Upload([FromForm] ImageUploadDto image)
        {
            ValidateFileUpload(image); // Validate the uploaded file (size and extension).

            // Check if the model state is valid (after validation).
            if (ModelState.IsValid)
            {
                // Convert the ImageUploadDto (which is the input data) into the domain model (Image).
                var imageDomainModel = new Image
                {
                    File = image.File, // The file data from the request.
                    FileExtension = Path.GetExtension(image.File.FileName), // Extract the file extension.
                    FileName = image.FileName, // File name from the request body.
                    FileDescription = image.FileDescription, // File description from the request body.
                };

                // Call the image repository to upload the image (this would typically store it in a database or cloud storage).
                await imageRepository.Upload(imageDomainModel);

                // Return an HTTP 200 OK response with the domain model of the uploaded image.
                return Ok(imageDomainModel);
            }

            // If the model state is not valid (e.g., failed validation), return a BadRequest.
            return BadRequest(ModelState);
        }

        // Method to validate the file upload (extension and size).
        private void ValidateFileUpload(ImageUploadDto image)
        {
            // Define allowed file extensions for image uploads (only images).
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            // Check if the uploaded file extension is among the allowed ones.
            if (!allowedExtensions.Contains(Path.GetExtension(image.File.FileName)))
            {
                // If the extension is not valid, add a model error.
                ModelState.AddModelError("File", "UnSupportedFileExtension");
            }

            // Check if the file size exceeds 10MB (10 * 1024 * 1024 bytes).
            if (image.File.Length > 10485760) // 10MB
            {
                // If the file is too large, add a model error.
                ModelState.AddModelError("File", "File Size More Than 10MB ");
            }
        }
    }
}
