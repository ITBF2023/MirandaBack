using Domain.Dto;

namespace Core.Interfaces
{
    public interface ITiempoContratoService
    {
        Task<List<TiempoContratoResponse>> GetAll();
    }
}