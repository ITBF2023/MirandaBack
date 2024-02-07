using AutoMapper;
using Core.Interfaces;
using DataAccess.Interface;
using Domain.Dto;
using Domain.Entities;

namespace Core.Repository
{
    public class CompetenciaEmpleadoService : ICompetenciaEmpleadoService
    {
        private readonly IRepository<CompetenciaLaboralEmpleado> competenciaLaboralRepository;
        private readonly IRepository<CompetenciaPersonalEmpleado> competenciaPersonalRepository;
        private readonly IMapper mapper;

        public CompetenciaEmpleadoService(IMapper mapper,
            IRepository<CompetenciaLaboralEmpleado> competenciaLaboralRepository,
            IRepository<CompetenciaPersonalEmpleado> competenciaPersonalRepository
            )
        {
            this.competenciaLaboralRepository = competenciaLaboralRepository;
            this.competenciaPersonalRepository = competenciaPersonalRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtiene la competencia laboral
        /// </summary>
        /// <param name="idEmpleado">Identificador de empleado</param>
        /// <returns></returns>
        public async Task<CompetenciaLaboralResponse> GetCompetenciaLaboral(long idEmpleado)
        {
            try
            {
                var competenciaLaboral = await competenciaLaboralRepository.GetByParam(p => p.IdEmpleado == idEmpleado);
                var competenciaLaboralResponse = mapper.Map<CompetenciaLaboralResponse>(competenciaLaboral);

                return competenciaLaboralResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene la competencia personal
        /// </summary>
        /// <param name="idEmpleado">Identificador de empleado</param>
        /// <returns></returns>
        public async Task<CompetenciaPersonalResponse> GetCompetenciaPersonal(long idEmpleado)
        {
            try
            {
                var competenciaPersonal = await competenciaPersonalRepository.GetByParam(p => p.IdEmpleado == idEmpleado);
                var competenciaPersonalResponse = mapper.Map<CompetenciaPersonalResponse>(competenciaPersonal);

                return competenciaPersonalResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}