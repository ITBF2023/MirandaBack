using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class EstudioCandidatoRequest
    {
        public int IdTipoEstudio { get; set; }
        public string Institucion { get; set; } = string.Empty;
        public int YearFinally { get; set; }
        public string TituloObtenido { get; set; } = string.Empty;
    }
}
