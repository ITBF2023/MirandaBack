using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Candidato")]
    public class Candidato : Auditoria
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int IdCandidato { get; set; }

        [Required]
        public string Documento { get; set; } = string.Empty;

        [Required]
        [ForeignKey("TipoDocumento")]
        public int IdTipoDocumento { get; set; }

        [ForeignKey("IdTipoDocumento")]
        public TipoDocumento? TipoDocumento { get; set; }

        [Required]
        public string? PrimerNombre { get; set; } = string.Empty;

        public string? SegundoNombre { get; set; } = string.Empty;

        [Required]
        public string? PrimerApellido { get; set; } = string.Empty;

        public string? SegundoApellido { get; set; } = string.Empty;
        public string? NumeroTelefonico { get; set; } = string.Empty;
        public string? Correo { get; set; } = string.Empty;
        public string? UrlCV { get; set; }

        [Required]
        [ForeignKey("Estado")]
        public int? IdEstado { get; set; }

        [ForeignKey("IdEstado")]
        public EstadoCandidato? Estado { get; set; }

        public string? JustificacionEstado { get; set; }

        public decimal? ValorRecurso { get; set; }

        public string? Comentarios { get; set; }

        public bool? Activo { get; set; } = true;

        public int? IdUserCreated { get; set; }
        public DateTime? DateCreated { get; set; }

        public int? UserIdModified { get; set; }
        public DateTime? DateModified { get; set; }

        [ForeignKey("TipoSalario")]
        public int IdTipoSalario { get; set; }

        [ForeignKey("IdTipoSalario")]
        public TipoSalario TipoSalario { get; set; }

        [Required]
        [ForeignKey("Vacante")]
        public int IdVacante { get; set; }

        [ForeignKey("IdVacante")]
        public Vacante Vacante { get; set; }

        public DateTime? FechaNacimiento { get; set; }
    }
}