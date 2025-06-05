using AutoMapper;

namespace firstback.tematicas
{
    public class TematicaProfile : Profile
    {
        public TematicaProfile()
        {
            CreateMap<Tematica, TematicaDTO>().ReverseMap();
        }
    }
}