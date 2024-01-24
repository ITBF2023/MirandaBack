using Domain.Common;

namespace Domain.Dto
{
    public class ClientResponse : BaseResponse
    {
        public int IdCliente { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Nit { get; set; } = string.Empty;
        public string PathLogo { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }
}