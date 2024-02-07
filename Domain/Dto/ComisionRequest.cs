using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class ComisionRequest
    {
        public long IdComision { get; set; }

        [Required]
        public int IdUsuarioComision { get; set; }

        [Required]
        public int IdEmpleado { get; set; }

        public DateTime FechaIngreso { get; set; }
        public bool Activo { get; set; }
    }

    public class ComisionStatusRequest
    {
        [Required]
        public long IdComision { get; set; }

        public bool Activo { get; set; }
    }
}