using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("TiempoContrato")]
    public class TiempoContrato
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int IdTiempoContrato { get; set; }

        [Required]
        public string Descripcion { get; set; } = string.Empty;
    }
}