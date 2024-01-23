using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class UserRequest
    {
        public int Id { get; set; }

        [Required]
        public string Correo { get; set; } = string.Empty;

        public string Contraseña { get; set; } = string.Empty;

        public List<RolRequest> Roles { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string Telefono { get; set; }

        public string FotoBase64 { get; set; }

        public bool Estado { get; set; }
    }
}