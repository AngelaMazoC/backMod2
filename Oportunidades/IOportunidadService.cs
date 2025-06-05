namespace firstback.Oportunidades
{
    public interface IOportunidadService
    {
        Task<IEnumerable<Oportunidad>> GetAllAsync();
        Task<Oportunidad?> GetByIdAsync(int id);
        Task<int> CreateAsync(OportunidadDTO oportunidadDTO);
        Task UpdateAsync(int id, OportunidadDTO oportunidad);
        Task DeleteAsync(int id);
    }
}