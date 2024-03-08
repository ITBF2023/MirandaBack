using Domain.Common;
using Domain.Dto;

namespace Core.Interfaces
{
    public interface IProcesoDiciplinarioService
    {
        Task<BaseResponse> Create(ProcesoDiciplinarioRequest request);

        Task<List<ProcesoDiciplinarioResponse>> GetAll();

        Task<List<ProcesoDiciplinarioResponse>> GetByEmpleado(int id);
    }
}