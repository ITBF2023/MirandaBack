namespace Domain.Dto
{
    public class CertificadosResponse
    {
        public List<CertificadoEstudiantilResponse> CertificadoEstudiantilResponses { get; set; }
        public List<CertificadoPersonalResponse> CertificadoPersonalResponses { get; set; }
        public List<CertificadoLaboralResponse> CertificadoLaboralResponses { get; set; }
    }

    public class CertificadoEstudiantilResponse
    {
        public int IdCertificadoEstudiantil { get; set; }
        public string? UrlCertificado { get; set; }
        public bool Activo { get; set; }
    }

    public class CertificadoPersonalResponse
    {
        public int IdCertificado { get; set; }
        public string? UrlCertificado { get; set; }
        public bool Activo { get; set; }
    }

    public class CertificadoLaboralResponse
    {
        public int IdCertificado { get; set; }
        public string? UrlCertificado { get; set; }
        public bool Activo { get; set; }
    }
}