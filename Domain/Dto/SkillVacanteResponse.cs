using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class SkillVacanteResponse
    {
        public int Id { get; set; }

        [Required]
        public int IdCategoria { get; set; }

        public string DescripcionCategoria { get; set; } = string.Empty;

        public string DescripcionSkill { get; set; }

        public bool RequiereConocimiento { get; set; }

        public int Experiencia { get; set; }
    }
}