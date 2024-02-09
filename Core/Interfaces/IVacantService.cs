using Domain.Common;
using Domain.Dto;

namespace Core.Interfaces
{
    public interface IVacantService
    {
        Task<BaseResponse> Create(VacanteRequest vacanteRequest);

        Task<BaseResponse> Update(VacanteRequest vacanteRequest);

        Task<BaseResponse> UpdateState(VacanteStateRequest vacanteStateRequest);

        Task<List<VacanteDetailResponse>> GetAllVacantes();

        Task<VacanteResponse> GetById(int idVacante);

        Task<List<SkillVacanteResponse>> GetSkillsByIdVacante(int idVacante);

        Task<List<VacanteResponse>> GetByIdCliente(int idCliente);
    }
}