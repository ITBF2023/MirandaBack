using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class CandidatoRequest
    {
        public int IdCandidato { get; set; }

        [Required]
        public string Documento { get; set; } = string.Empty;

        [Required]
        public int IdTipoDocumento { get; set; }

        [Required]
        public string PrimerNombre { get; set; } = string.Empty;

        public string SegundoNombre { get; set; } = string.Empty;

        [Required]
        public string PrimerApellido { get; set; } = string.Empty;

        public string SegundoApellido { get; set; } = string.Empty;
        public string NumeroTelefonico { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Base64CV { get; set; } = string.Empty;
        public int IdUser { get; set; }

        public int IdTipoSalario { get; set; }

        public int IdVacante { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Comentarios { get; set; }

        public int IdEstado { get; set; }

        public string DescripcionEstado { get; set; }

        public decimal ValorRecurso { get; set; }

        public List<EstudioCandidatoRequest>? ListEstudio { get; set; }
        public List<ReferenciaLaboralCandidatoRequest>? ListReferenciaLaboral { get; set; }
        public List<ReferenciaPersonalCandidatoRequest>? ListReferenciaPersonal { get; set; }
    }
}