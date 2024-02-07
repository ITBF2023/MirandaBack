using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class ComisionResponse : BaseResponse
    {
        public long IdComision { get; set; }

        [Required]
        public int IdUsuarioComision { get; set; }

        [Required]
        public long IdEmpleado { get; set; }

        public DateTime FechaIngreso { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
    }
}