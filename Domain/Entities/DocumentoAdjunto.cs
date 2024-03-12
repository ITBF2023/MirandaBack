using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("DocumentoAdjunto")]
    public class DocumentoAdjunto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Candidato")]
        public int IdCandidato { get; set; }

        [ForeignKey("IdCandidato")]
        public Candidato? Candidato { get; set; }

        [Required]
        [ForeignKey("TipoArchivo")]
        public int IdTipoArchivo { get; set; }

        [ForeignKey("IdTipoArchivo")]
        public TipoArchivo? TipoArchivo { get; set; }

        public string Nombre { get; set; }

        [Required]
        [ForeignKey("UsuarioCreacion")]
        public int IdUsuarioCreacion { get; set; }

        [ForeignKey("IdUsuarioCreacion")]
        public Usuario? UsuarioCreacion { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}