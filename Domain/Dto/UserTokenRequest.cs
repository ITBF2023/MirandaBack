using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class UserTokenRequest
    {
        [Required]
        public string Correo { get; set; } = string.Empty;

        [Required]
        public string Contraseña { get; set; } = string.Empty;
    }
}