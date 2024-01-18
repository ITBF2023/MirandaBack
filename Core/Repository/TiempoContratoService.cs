using AutoMapper;
using Core.Interfaces;
using DataAccess.Interface;
using Domain.Common;
using Domain.Common.Enum;
using Domain.Dto;
using Domain.Entities;
using System.Net;

namespace Core.Repository
{
    public class TiempoContratoService : ITiempoContratoService
    {
        private readonly IRepository<TiempoContrato> tiempoContratoRepository;
        private readonly IMapper mapper;

        public TiempoContratoService( IMapper mapper, IRepository<TiempoContrato> tiempoContratoRepository)
        {
            this.mapper = mapper;
            this.tiempoContratoRepository = tiempoContratoRepository;
        }

        public async Task<List<TiempoContratoResponse>> GetAll()
        {
            var listResponse = new List<TiempoContratoResponse>();
            try
            {
                var listTiempoContrato = await tiempoContratoRepository.GetAll();
                listResponse = MapperListesponse(listTiempoContrato);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listResponse;
        }

        private List<TiempoContratoResponse> MapperListesponse(List<TiempoContrato> list)
        {
            List<TiempoContratoResponse> listResponse = new List<TiempoContratoResponse>();

            list.ForEach(c =>
            {
                var item = new TiempoContratoResponse();
                item.Id = c.IdTiempoContrato;
                item.Descripcion = c.Descripcion;
                listResponse.Add(item);
            });
            return listResponse;
        }
    }
}