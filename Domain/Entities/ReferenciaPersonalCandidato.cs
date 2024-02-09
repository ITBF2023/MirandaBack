using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    [Table("ReferenciaPersonalCandidato")]
    public class ReferenciaPersonalCandidato
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long Id { get; set; }

        [Required]
        [ForeignKey("Candidato")]
        public int IdCandidato { get; set; }

        [ForeignKey("IdCandidato")]
        public Candidato? Candidato { get; set; }

        public string NombreContacto { get; set; } = string.Empty;

        [Required]
        public string Telefono { get; set; } = string.Empty;

        [Required]
        public string Parentesco { get; set; } = string.Empty;

        [Required]
        public int TiempoConocido { get; set; }

        public bool Verificado { get; set; }
    }
}