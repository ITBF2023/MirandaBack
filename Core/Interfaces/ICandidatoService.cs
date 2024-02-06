using Domain.Common;
using Domain.Dto;

namespace Core.Interfaces
{
    public interface ICandidatoService
    {
        Task<BaseResponse> Create(CandidatoRequest candidatoRequest);

        Task<BaseResponse> Update(CandidatoRequest candidatoRequest);

        Task<BaseResponse> UpdateActiveCandidato(CandidatoActiveRequest candidatoStateRequest);

        Task<BaseResponse> UpdateStateCandidato(CandidatoStateRequest candidatoStateRequest);

        Task<BaseResponse> UpdateVerifyRefLaborales(List<ReferenciasLaboralesVerifyRequest> referenciasLaboralesVerifyRequests);

        Task<BaseResponse> UpdateVerifyRefPersonales(List<ReferenciasPersonalesVerifyRequest> referenciasPersonalesVerifyRequests);

        Task<List<CandidatoResponse>> GetAllCandidatos();

        Task<List<EstudioCandidatoResponse>> GetAllEstudiosCandidato(int idCandidato);

        Task<List<CandidatoResponse>> GetByDocument(string document);

        /// <summary>
        /// Obtener candidato por el id
        /// </summary>
        /// <param name="id">Id del cantidato</param>
        /// <returns></returns>
        Task<CandidatoResponse> GetById(int id);

        Task<List<ReferenciasPersonalesResponse>> GetAllRefPersonalesCandidato(int idCandidato);

        Task<List<ReferenciasLaboralesResponse>> GetAllRefLaboralesCandidato(int idCandidato);

        //Task<object> GetAllCandidatosFilter(int cedula);
    }
}