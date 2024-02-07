using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Comision")]
    public class Comision
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdComision { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int IdUsuarioComision { get; set; }

        [ForeignKey("IdUsuarioComision")]
        public Usuario? Usuario { get; set; }

        [Required]
        [ForeignKey("Empleado")]
        public int IdEmpleado { get; set; }

        [ForeignKey("IdEmpleado")]
        public Empleado? Empleado { get; set; }

        public DateTime FechaIngreso { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
    }
}