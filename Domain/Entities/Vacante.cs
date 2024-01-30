using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Vacante")]
    public class Vacante : Auditoria
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdVacante { get; set; }

        [Required]
        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }

        [ForeignKey("IdCliente")]
        public Cliente? Cliente { get; set; }

        public string DescripcionCargo { get; set; } = string.Empty;
        public string Profesion { get; set; } = string.Empty;
        public int TiempoExperiencia { get; set; }

        [Required]
        [ForeignKey("Contrato")]
        public int IdContrato { get; set; }

        [ForeignKey("IdContrato")]
        public TipoContrato? Contrato { get; set; }

        [ForeignKey("IdSalario")]
        public TipoSalario? Salario { get; set; }

        public string Horario { get; set; } = string.Empty;

        [Required]
        [ForeignKey("ModalidadTrabajo")]
        public int IdModalidadTrabajo { get; set; }

        [ForeignKey("IdModalidadTrabajo")]
        public ModalidadTrabajo? ModalidadTrabajo { get; set; }

        public bool PruebaTecnica { get; set; }
        public string DescripcionFunciones { get; set; } = string.Empty;

        [Required]
        [ForeignKey("EstadoVacante")]
        public int IdEstadoVacante { get; set; }

        [ForeignKey("IdEstadoVacante")]
        public EstadoVacante? EstadoVacante { get; set; }

        public string Comentarios { get; set; } = string.Empty;

        /// <summary>
        /// Prioridad 0:Baja 1:Alta
        /// </summary>
        public bool Prioridad { get; set; }

        public decimal Salario100Prestacion { get; set; }

        public decimal SalarioPorcentual { get; set; }

        public decimal SalarioPrestacionServicios { get; set; }

        [Required]
        [ForeignKey("TiempoContrato")]
        public int IdTiempoContrato { get; set; }

        [ForeignKey("IdTiempoContrato")]
        public TiempoContrato? TiempoContrato { get; set; }
    }
}