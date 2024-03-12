using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("TipoArchivo")]
    public class TipoArchivo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Descripcion { get; set; } = string.Empty;

        public string CodigoPath { get; set; }
    }
}