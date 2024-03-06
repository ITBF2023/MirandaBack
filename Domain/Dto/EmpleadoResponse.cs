using Domain.Common;

namespace Domain.Dto
{
    public class EmpleadoResponse : BaseResponse
    {
        public int IdEmpleado { get; set; }

        public int IdCandidato { get; set; }

        public bool Activo { get; set; }
    }
}