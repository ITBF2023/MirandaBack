using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class DocumentoEntregadoResponse
    {
        public int IdCandidato { get; set; }

        public string NombreCandidato { get; set; }

        public int DocumentosCargados { get; set; }
    }
}
