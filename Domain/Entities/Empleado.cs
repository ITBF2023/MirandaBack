using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Empleado")]
    public class Empleado
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEmpleado { get; set; }

        [Required]
        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }

        [ForeignKey("IdCliente")]
        public Cliente? Cliente { get; set; }

        [Required]
        [ForeignKey("Candidato")]
        public int IdCandidato { get; set; }

        [ForeignKey("IdCandidato")]
        public Candidato? Candidato { get; set; }

        public string? CertEpslesPath { get; set; }
        public string? CertPensionesCesantiaslesPath { get; set; }
        public string? ExamenMedicolesPath { get; set; }
        public string? ContratoPath { get; set; }
        public string? FormatoEntregaEquipoPath { get; set; }
        public string? CuentaBancariaPath { get; set; }
        public bool Activo { get; set; }
    }
}