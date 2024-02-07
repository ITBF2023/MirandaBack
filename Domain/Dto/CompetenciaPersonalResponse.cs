using Domain.Common;

namespace Domain.Dto
{
    public class CompetenciaPersonalResponse : BaseResponse
    {
        public int IdCompetenciaPersonal { get; set; }

        public Int16 Compromiso { get; set; }

        public Int16 Colaboracion { get; set; }

        public Int16 Servicio { get; set; }

        public Int16 Responsabilidad { get; set; }

        public int IdUsuarioCreacion { get; set; }

        public string UsuarioCreacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int? IdUsuarioModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}