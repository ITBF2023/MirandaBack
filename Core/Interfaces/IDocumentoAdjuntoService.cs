using Domain.Common;
using Domain.Dto;

namespace Core.Interfaces
{
    public interface IDocumentoAdjuntoService
    {
        Task<BaseResponse> Create(DocumentoAdjuntoRequest request);

        Task<BaseResponse> Update(DocumentoAdjuntoRequest request);

        Task<List<DocumentoAdjuntoResponse>> GetByCandidato(int idCandidato);

        Task<DocumentoAdjuntoResponse> GetById(int id);

        Task<List<DocumentoEntregadoResponse>> GetDocumentosEntregados();
    }
}