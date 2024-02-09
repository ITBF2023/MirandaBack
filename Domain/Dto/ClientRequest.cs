using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class ClientRequest : UserAuditoria
    {
        public int IdCliente { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Nit { get; set; } = string.Empty;

        public string? UrlEmpresa { get; set; }

        public string Base64File { get; set; }

        public bool Estado { get; set; }
    }
}