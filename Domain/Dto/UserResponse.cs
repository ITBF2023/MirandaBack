using Domain.Common;

namespace Domain.Dto
{
    public class UserResponse : BaseResponse
    {
        public string Correo { get; set; } = string.Empty;
        public int IdRol { get; set; }
    }

    public class UsersResponse
    {
        public string Correo { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
    }
}