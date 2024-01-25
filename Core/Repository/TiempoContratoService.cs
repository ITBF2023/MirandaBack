using AutoMapper;
using Core.Interfaces;
using DataAccess.Interface;
using Domain.Dto;
using Domain.Entities;

namespace Core.Repository
{
    public class TiempoContratoService : ITiempoContratoService
    {
        private readonly IRepository<TiempoContrato> tiempoContratoRepository;
        private readonly IMapper mapper;

        public TiempoContratoService(IMapper mapper, IRepository<TiempoContrato> tiempoContratoRepository)
        {
            this.mapper = mapper;
            this.tiempoContratoRepository = tiempoContratoRepository;
        }

        public async Task<List<TiempoContratoResponse>> GetAll()
        {
            List<TiempoContratoResponse> listResponse;

            try
            {
                var listTiempoContrato = await tiempoContratoRepository.GetAll();
                listResponse = mapper.Map<List<TiempoContratoResponse>>(listTiempoContrato);
                return listResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }
    }
}