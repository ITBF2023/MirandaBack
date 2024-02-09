using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("ReferenciaLaboralCandidato")]
    public class ReferenciaLaboralCandidato
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long Id { get; set; }

        [Required]
        [ForeignKey("Candidato")]
        public int IdCandidato { get; set; }

        [ForeignKey("IdCandidato")]
        public Candidato? Candidato { get; set; }

        [Required]
        public string Empresa { get; set; } = string.Empty;

        [Required]
        public string Telefono { get; set; } = string.Empty;

        [Required]
        public string NombreContacto { get; set; } = string.Empty;

        public string CargoContacto { get; set; } = string.Empty;
        public string MotivoRetiro { get; set; } = string.Empty;

        [Required]
        public string CargoDesempenado { get; set; } = string.Empty;

        public string Desempeno { get; set; } = string.Empty;
        public bool Verificado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public string FuncionCargo { get; set; }
    }
}