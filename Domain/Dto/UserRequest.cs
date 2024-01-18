using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class UserRequest
    {
        [Required]
        public string Correo { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public int IdRol { get; set; }
    }

    public class UserCreateRequest
    {
        public int Id { get; set; }

        [Required]
        public string Correo { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        [Required]
        public int IdRol { get; set; }
    }
}