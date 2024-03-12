using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class DocumentoAdjuntoRequest
    {
        public int? Id { get; set; }

        [Required]
        public int IdCandidato { get; set; }

        [Required]
        public int IdTipoArchivo { get; set; }

        [Required]
        public string Archivo { get; set; }

        [Required]
        public int IdUsuarioCreacion { get; set; }
    }
}