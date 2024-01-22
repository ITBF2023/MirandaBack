using Domain.Common;

namespace Domain.Dto
{
    public class UserResponse : BaseResponse
    {
        public int IdUser { get; set; }
        public string Correo { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Foto { get; set; }
        public List<RolResponse> Roles { get; set; }
        public bool Estado { get; set; }
    }
}