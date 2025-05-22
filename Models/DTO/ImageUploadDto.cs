using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace nzWalksApi.Models.DTO
{
    public class ImageUploadDto
    {
        [Required]
        public IFormFile File { get; set; }

        [Required]
        public string FileName { get; set; }

        public string? FileDescription { get; set; }
    }
}
