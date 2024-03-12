using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class DocumentoAdjuntoResponse
    {
        public int? Id { get; set; }

        public int IdCandidato { get; set; }

        public int IdTipoArchivo { get; set; }

        public string DescripcionTipoArchivo { get; set; }

        public string NombreArchivo { get; set; }

        public int IdUsuarioCreacion { get; set; }

        public string NombreUsuarioCreacion { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}