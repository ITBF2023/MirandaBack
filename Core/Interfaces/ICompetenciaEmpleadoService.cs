using Domain.Dto;

namespace Core.Interfaces
{
    public interface ICompetenciaEmpleadoService
    {
        /// <summary>
        /// Obtiene la competencia laboral
        /// </summary>
        /// <param name="idEmpleado">Identificador de empleado</param>
        /// <returns></returns>
        Task<CompetenciaLaboralResponse> GetCompetenciaLaboral(long idEmpleado);

        /// <summary>
        /// Obtiene la competencia personal
        /// </summary>
        /// <param name="idEmpleado">Identificador de empleado</param>
        /// <returns></returns>
        Task<CompetenciaPersonalResponse> GetCompetenciaPersonal(long idEmpleado);
    }
}