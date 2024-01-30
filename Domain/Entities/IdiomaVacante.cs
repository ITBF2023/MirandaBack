using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("IdiomaVacante")]
    public class IdiomaVacante
    {
        [Required]
        [ForeignKey("Idioma")]
        public int IdIdioma { get; set; }

        [Required]
        [ForeignKey("IdIdioma")]
        public int Idioma { get; set; }

        [Required]
        [ForeignKey("Vacante")]
        public int IdVacante { get; set; }

        [Required]
        [ForeignKey("IdVacante")]
        public int Vacante { get; set; }

        public int Porcentaje { get; set; }
    }
}