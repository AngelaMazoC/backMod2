using AutoMapper;

namespace firstback.Oportunidades
{
    public class OportunidadProfile : Profile
    {
        public OportunidadProfile()
        {
            CreateMap<Oportunidad, OportunidadDTO>();
            CreateMap<OportunidadDTO, Oportunidad>();
        }
    }
}