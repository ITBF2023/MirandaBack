using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class CandidatoResponse : BaseResponse
    {
        public int IdCandidato { get; set; }

        [Required]
        public string Documento { get; set; } = string.Empty;

        [Required]
        public int IdTipoDocumento { get; set; }

        public string DescripcionTipoDocumento { get; set; }

        [Required]
        public string PrimerNombre { get; set; } = string.Empty;

        public string SegundoNombre { get; set; } = string.Empty;

        [Required]
        public string PrimerApellido { get; set; } = string.Empty;

        public string SegundoApellido { get; set; } = string.Empty;
        public string NumeroTelefonico { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string? UrlCV { get; set; }
        public bool Activo { get; set; } = true;
        public string? Nombre { get; set; }

        public string? Cliente { get; set; }

        public string Comentarios { get; set; }

        public int IdTipoSalario { get; set; }

        public int IdVacante { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public int IdEstado { get; set; }

        public string DescripcionEstado { get; set; }

        public string JustificacionEstado { get; set; }

        public decimal ValorRecurso { get; set; }

        public string DescripcionCargo { get; set; }
    }
}