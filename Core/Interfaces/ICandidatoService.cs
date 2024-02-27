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

        Task<List<CandidatoResponse>> GetAllContratados();

        Task<List<EstudioCandidatoResponse>> GetAllEstudiosCandidato(int idCandidato);

        Task<List<CandidatoResponse>> GetByDocument(string document);

        /// <summary>
        /// Obtener candidato por el id
        /// </summary>
        /// <param name="id">Id del cantidato</param>
        /// <returns></returns>
        Task<CandidatoResponse> GetById(int id);

        /// <summary>
        /// Obtener candidatos por id de vacante
        /// </summary>
        /// <param name="id">Id de vacante</param>
        /// <returns></returns>
        Task<List<CandidatoResponse>> GetByIdVacante(int id);

        Task<List<ReferenciasPersonalesResponse>> GetAllRefPersonalesCandidato(int idCandidato);

        Task<List<ReferenciasLaboralesResponse>> GetAllRefLaboralesCandidato(int idCandidato);

        //Task<object> GetAllCandidatosFilter(int cedula);
    }
}