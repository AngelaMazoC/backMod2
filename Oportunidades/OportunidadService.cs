using AutoMapper;
using BackendProject.Data;
using Microsoft.EntityFrameworkCore;

namespace firstback.Oportunidades
{
    public class OportunidadService : IOportunidadService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OportunidadService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Oportunidad>> GetAllAsync()
        {
            var oportunidades = await _context.Oportunidades
            .Include(o => o.Categoria)
            .Include(o => o.Institucion)
            .ToListAsync();
            return _mapper.Map<IEnumerable<Oportunidad>>(oportunidades);
        }

        public async Task<Oportunidad?> GetByIdAsync(int id)
        {
            return await _context.Oportunidades
            .Include(o => o.Categoria)
            .Include(o => o.Institucion)
            .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<int> CreateAsync(OportunidadDTO oportunidadDTO)
        {
            var oportunidad = _mapper.Map<Oportunidad>(oportunidadDTO);
            _context.Oportunidades.Add(oportunidad);
            await _context.SaveChangesAsync();
            return oportunidad.Id;
        }

        public async Task UpdateAsync(int id, OportunidadDTO oportunidadDTO)
        {
            var oportunidad = await _context.Oportunidades.FindAsync(id);

            if (oportunidad != null)
            {
                _mapper.Map(oportunidadDTO, oportunidad);
                _context.Oportunidades.Update(oportunidad);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var oportunidad = await _context.Oportunidades.FindAsync(id);
            if (oportunidad != null)
            {
                _context.Oportunidades.Remove(oportunidad);
                await _context.SaveChangesAsync();
            }
        }
    }
}