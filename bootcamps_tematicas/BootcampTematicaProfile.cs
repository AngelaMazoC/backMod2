using AutoMapper;

namespace firstback.BootcampsTematicas
{
    public class BootcampTematicaProfile : Profile
    {
        public BootcampTematicaProfile()
        {
            CreateMap<BootcampTematicaDto, BootcampTematica>();
            CreateMap<BootcampTematica, BootcampTematicaDto>();
        }
    }
}