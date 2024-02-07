using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("CompetenciaLaboralEmpleado")]
    public class CompetenciaLaboralEmpleado
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
        public Int16 Eficiencia { get; set; }

        [Required]
        public Int16 Eficacia { get; set; }

        [Required]
        public Int16 Proactividad { get; set; }

        [Required]
        public Int16 TrabajoEnEquipo { get; set; }

        [Required]
        public Int16 CalidadTrabajo { get; set; }

        [Required]
        public Int16 OrientacionCliente { get; set; }

        [Required]
        public Int16 OrientacionResultado { get; set; }

        [Required]
        public Int16 ComunicacionColaboracion { get; set; }

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