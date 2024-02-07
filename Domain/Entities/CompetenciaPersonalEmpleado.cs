using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    [Table("CompetenciaPersonalEmpleado")]
    public class CompetenciaPersonalEmpleado
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Empleado")]
        public int IdEmpleado { get; set; }

        [Required]
        [ForeignKey("IdEmpleado")]
        public Empleado Empleado { get; set; }

        [Required]
        public Int16 Compromiso { get; set; }

        [Required]
        public Int16 Colaboracion { get; set; }

        [Required]
        public Int16 Servicio { get; set; }

        [Required]
        public Int16 Responsabilidad { get; set; }

        [Required]
        [ForeignKey("UsuarioCreacion")]
        public int IdUsuarioCreacion { get; set; }

        [Required]
        [ForeignKey("IdUsuarioCreacion")]
        public Usuario UsuarioCreacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        [ForeignKey("UsuarioModificacion")]
        public int? IdUsuarioModificacion { get; set; }

        [ForeignKey("IdUsuarioModificacion")]
        public Usuario? UsuarioModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}