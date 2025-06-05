namespace firstback.InstitucionesBootcamps
{
    public interface IInstitucionBootcampService
    {
        Task<IEnumerable<InstitucionBootcampDto>> GetAllAsync();
        Task<InstitucionBootcampDto?> GetByIdAsync(int idInstitucion, int idBootcamp);
        Task<InstitucionBootcampDto> CreateAsync(InstitucionBootcampDto institucionBootcampDto);
        Task<bool> DeleteAsync(int idInstitucion, int idBootcamp);
    }
}
