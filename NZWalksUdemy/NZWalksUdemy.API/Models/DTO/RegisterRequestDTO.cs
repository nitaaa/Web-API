using System.ComponentModel.DataAnnotations;

namespace NZWalksUdemy.API.Models.DTO
{
    public class RegisterRequestDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public string[] roles { get; set; }
    }
}
