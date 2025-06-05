namespace firstback.BootcampsTematicas
{
    public interface IBootcampTematicaService
    {
        Task<IEnumerable<BootcampTematicaDto>> GetAllAsync();
        Task<BootcampTematicaDto?> GetByIdAsync(int idBootcamp, int idTematica);
        Task<BootcampTematicaDto> CreateAsync(BootcampTematicaDto bootcampTematicaDto);
        Task<bool> DeleteAsync(int idBootcamp, int idTematica);
    }
}