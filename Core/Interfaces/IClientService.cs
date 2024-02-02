using Domain.Common;
using Domain.Dto;

namespace Core.Interfaces
{
    public interface IClientService
    {
        Task<BaseResponse> CreateClient(ClientRequest clientRequest);

        Task<BaseResponse> UpdateCliet(ClientRequest clientRequest);

        Task<ClientResponse> GetClientByDocument(string nit);

        Task<ClientResponse> GetClientByID(int id);

        Task<List<ClientResponse>> GetListClients();

        Task<List<SPEmployeesByClientResponse>> GetEmployeesByClient(int idClient);

        Task<List<VacantesEmpresaResponse>> GetVacantsByClient(int idClient);

        Task<BaseResponse> CancelProccessCandidateByClient(CancelProcessClientRequest cancelProcessClientRequest);
    }
}