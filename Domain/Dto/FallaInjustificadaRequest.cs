using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class FallaInjustificadaRequest
    {
        [Required]
        public int IdEmpleado { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        public string? Comentario { get; set; }

        public int IdUsuario { get; set; }
    }
}