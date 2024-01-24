using Domain.Entities;

namespace Domain.Dto
{
    public class RolUsuarioResponse
    {
        public Rol Rol { get; set; }

        public Usuario Usuario { get; set; }
    }
}