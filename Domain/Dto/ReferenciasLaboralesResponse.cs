using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class ReferenciasLaboralesResponse
    {
        [Required]
        public long IdReferenciasLaboralesCandidato { get; set; }

        [Required]
        public int IdCandidato { get; set; }

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
        public bool Activo { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinalizacion { get; set; }

        public string FuncionCargo { get; set; } = string.Empty;
    }
}