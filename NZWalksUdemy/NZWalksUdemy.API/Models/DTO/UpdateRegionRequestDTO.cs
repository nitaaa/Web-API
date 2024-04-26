using System.ComponentModel.DataAnnotations;

namespace NZWalksUdemy.API.Models.DTO
{
    public class UpdateRegionRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code must be 3 Characters.")]
        [MaxLength(3, ErrorMessage = "Code must be 3 Characters.")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name must be 100 Characters or less.")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
