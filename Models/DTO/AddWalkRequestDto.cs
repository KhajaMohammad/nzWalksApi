using nzWalksApi.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace nzWalksApi.Models.DTO
{
    public class AddWalkRequestDto
    {


        [Required]
       
        public string Name { get; set; }

        public string Description { get; set; }

        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        public Guid DifficultyId { get; set; }

        public Guid RegionId { get; set; }

        // Navigation property

        
    }



}

