using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class VacanteRequest
    {
        public int IdVacante { get; set; }

        [Required]
        public int IdCliente { get; set; }

        [Required]
        public string DescripcionCargo { get; set; } = string.Empty;

        [Required]
        public string Profesion { get; set; } = string.Empty;

        [Required]
        public int TiempoExperiencia { get; set; }

        [Required]
        public int IdContrato { get; set; }

        public string Horario { get; set; } = string.Empty;

        [Required]
        public int IdModalidadTrabajo { get; set; }

        public bool PruebaTecnica { get; set; }
        public string DescripcionFunciones { get; set; } = string.Empty;
        public string Comentarios { get; set; } = string.Empty;

        [Required]
        public int IdUser { get; set; }

        public List<SkillVacanteRequest>? ListSkillsVacante { get; set; }

        public bool Prioridad { get; set; }

        public decimal Salario100Prestacion { get; set; }

        public decimal SalarioPorcentual { get; set; }

        public decimal SalarioPrestacionServicios { get; set; }

        [Required]
        public int IdTiempoContrato { get; set; }

        [Required]
        public int IdRangoEdad { get; set; }

        public List<IdiomaVacanteRequest> ListaIdiomas { get; set; }
    }

    public class SkillVacanteRequest
    {
        [Required]
        public int IdCategoria { get; set; }

        /// <summary>
        /// Describe el skill detallando apartir de la categoria
        /// </summary>
        public string DescripcionSkill { get; set; } = string.Empty;

        public bool RequiereConocimiento { get; set; }

        public int Experiencia { get; set; }
    }
}