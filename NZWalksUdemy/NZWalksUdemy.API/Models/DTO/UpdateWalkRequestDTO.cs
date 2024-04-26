using System.ComponentModel.DataAnnotations;

namespace NZWalksUdemy.API.Models.DTO
{
    public class UpdateWalkRequestDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name must be 100 Characters or less.")]
        public string Name { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "Description must be 250 Characters or less.")]
        public string Description { get; set; }
        [Required]
        [Range(1, 50)]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
