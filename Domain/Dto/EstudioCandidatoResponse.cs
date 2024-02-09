using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class EstudioCandidatoResponse
    {
        [Required]
        public long IdEstudioCandidato { get; set; }

        [Required]
        public int IdCandidato { get; set; }

        public int IdTipoEstudio { get; set; }
        public string Institucion { get; set; } = string.Empty;
        public int YearFinally { get; set; }
        public string TituloObtenido { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
    }
}