using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class ReferenciasPersonalesResponse
    {
        [Required]
        public long IdReferenciasPersonalesCandidato { get; set; }

        public string NombreContacto { get; set; } = string.Empty;

        [Required]
        public string Telefono { get; set; } = string.Empty;

        [Required]
        public string Parentesco { get; set; } = string.Empty;

        [Required]
        public int TiempoConocido { get; set; }

        public bool Verificado { get; set; }
        public bool Activo { get; set; }
    }
}