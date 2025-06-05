namespace firstback.InstitucionesBootcamps
{
    public static class InstitucionBootcampMapper
    {
        public static InstitucionBootcamp MapToEntity(InstitucionBootcampDto dto)
        {
            return new InstitucionBootcamp
            {
                Id_Institucion = dto.IdInstitucion,
                Id_Bootcamp = dto.IdBootcamp
            };
        }
    }
}