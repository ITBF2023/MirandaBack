using Domain.Common;
using Domain.Dto;

namespace Core.Interfaces
{
    public interface IFallaInjustificadaService
    {
        Task<BaseResponse> Create(FallaInjustificadaRequest request);

        Task<List<FallaInjustificadaResponse>> GetAll();

        Task<List<FallaInjustificadaResponse>> GetByEmpleado(int id);
    }
}