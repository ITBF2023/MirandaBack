using Domain.Common;
using Domain.Dto;

namespace Core.Interfaces
{
    public interface IEmpleadoService
    {
        Task<BaseResponse> CreateCertificados(ContratoCreateRequest contratoRequest);

        Task<BaseResponse> UpdateCertificados(ContratoEditRequest contratoRequest);

        Task<List<EmpleadoResponse>> GetAll();

        Task<SPInfoEmployeeResponse> GetInfoEmployeeById(long idEmpleado);

        Task<List<SPHistoricalNoverltyEmployeeResponse>> GetHistoricalNoverltyEmployees(long idEmpleado);

        Task<CertificadosResponse> GetCertificadosByEmployee(long idEmpleado);

        Task<EmpleadoResponse> GetById(int id);
    }
}