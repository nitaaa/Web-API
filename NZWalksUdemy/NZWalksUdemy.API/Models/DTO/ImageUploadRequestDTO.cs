using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalksUdemy.API.Models.DTO
{
    public class ImageUploadRequestDTO
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        public string? Description { get; set; }
    }
}
