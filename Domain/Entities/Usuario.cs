using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUser { get; set; }

        [Required]
        public string Password { get; set; } = string.Empty;

        public string? Nombres { get; set; }

        public string? Apellidos { get; set; }

        public string? Telefono { get; set; }

        public string? Correo { get; set; }

        public string? Foto { get; set; }
    }
}