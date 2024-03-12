using AutoMapper;
using Domain.Dto;
using Domain.Entities;

namespace Core.Profiles
{
    public class DocumentoAdjuntoProfile : Profile
    {
        public DocumentoAdjuntoProfile()
        {
            CreateMap<DocumentoAdjuntoRequest, DocumentoAdjunto>()
              .ReverseMap();

            CreateMap<DocumentoAdjunto, DocumentoAdjuntoResponse>()
                .ForMember(dest => dest.NombreUsuarioCreacion, act => act.MapFrom(src => string.Format("{0} {1}", src.UsuarioCreacion.Nombres, src.UsuarioCreacion.Apellidos)))
                .ForMember(dest => dest.DescripcionTipoArchivo, act => act.MapFrom(src => src.TipoArchivo.Descripcion))
                .ForMember(dest => dest.NombreArchivo, act => act.MapFrom(src => src.Nombre));
        }
    }
}