using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("LlamadoAtencion")]
    public class LlamadoAtencion
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Empleado")]
        public int IdEmpleado { get; set; }

        [ForeignKey("IdEmpleado")]
        public Empleado Empleado { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        public string? Comentario { get; set; }

        public DateTime FechaCreacion { get; set; }

        [ForeignKey("UsuarioCreacion")]
        public int IdUsuarioCreacion { get; set; }

        [ForeignKey("IdUsuarioCreacion")]
        public Usuario UsuarioCreacion { get; set; }
    }
}