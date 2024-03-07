using Domain.Common;
using Domain.Dto;

namespace Core.Interfaces
{
    public interface ILlamadoAtencionService
    {
        Task<BaseResponse> Create(LlamadoAtencionRequest request);

        Task<List<LlamadoAtencionResponse>> GetAll();

        Task<List<LlamadoAtencionResponse>> GetByEmpleado(int id);
    }
}