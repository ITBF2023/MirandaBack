using Domain.Common;

namespace Domain.Dto
{
    public class CompetenciaLaboralResponse : BaseResponse
    {
        public int IdCompetenciaLaboral { get; set; }

        public Int16 Compromiso { get; set; }

        public Int16 Eficiencia { get; set; }

        public Int16 Eficacia { get; set; }

        public Int16 Proactividad { get; set; }

        public Int16 TrabajoEnEquipo { get; set; }

        public Int16 CalidadTrabajo { get; set; }

        public Int16 OrientacionCliente { get; set; }

        public Int16 OrientacionResultado { get; set; }

        public Int16 ComunicacionColaboracion { get; set; }

        public int IdUsuarioCreacion { get; set; }

        public string UsuarioCreacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int? IdUsuarioModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}