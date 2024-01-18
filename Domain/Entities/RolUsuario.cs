using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("RolUsuario")]
    public class RolUsuario
    {
        [Required]
        [ForeignKey("Rol")]
        public int IdRol { get; set; }

        [ForeignKey("IdRol")]
        public Rol Rol { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [Required]
        [ForeignKey("UsuarioCreacion")]
        public int IdUsuarioCreacion { get; set; }

        [ForeignKey("IdUsuarioCreacion")]
        public Usuario UsuarioCreacion { get; set; }

        public bool Activo { get; set; }
    }
}