using System.ComponentModel.DataAnnotations;

namespace nzWalksApi.Models.DTO
{
    public class UpdateRegionDto
    {

        [Required]
        [MinLength(3, ErrorMessage = "Code Has to be a min of 3 Characters")]
        [MaxLength(5, ErrorMessage = "Code Cannot exceed 5 characters")]

        public string Code { get; set; }

        [Required]
       
        [MaxLength(100, ErrorMessage = "Name Cannot exceed 100 characters")]

        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
