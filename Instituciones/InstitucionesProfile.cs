using AutoMapper;

namespace firstback.Instituciones
{
    public class InstitucionProfile : Profile
    {
        public InstitucionProfile()
        {
            CreateMap<InstitucionDTO, Institucion>();
            CreateMap<Institucion, InstitucionDTO>();
        }
    }
}