using Microsoft.EntityFrameworkCore;
using BackendProject.Data;

namespace firstback.InstitucionesBootcamps
{
    public class InstitucionBootcampService : IInstitucionBootcampService
    {
        private readonly ApplicationDbContext _context;

        public InstitucionBootcampService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InstitucionBootcampDto>> GetAllAsync()
        {
            return await _context.InstitucionBootcamps
                .Select(ib => new InstitucionBootcampDto
                {
                    IdInstitucion = ib.Id_Institucion,
                    IdBootcamp = ib.Id_Bootcamp
                })
                .ToListAsync();
        }

        public async Task<InstitucionBootcampDto?> GetByIdAsync(int idInstitucion, int idBootcamp)
        {
            return await _context.InstitucionBootcamps
                .Where(ib => ib.Id_Institucion == idInstitucion && ib.Id_Bootcamp == idBootcamp)
                .Select(ib => new InstitucionBootcampDto
                {
                    IdInstitucion = ib.Id_Institucion,
                    IdBootcamp = ib.Id_Bootcamp
                })
                .FirstOrDefaultAsync();
        }

        public async Task<InstitucionBootcampDto> CreateAsync(InstitucionBootcampDto institucionBootcampDto)
        {
            var institucionBootcamp = InstitucionBootcampMapper.MapToEntity(institucionBootcampDto);

            _context.InstitucionBootcamps.Add(institucionBootcamp);
            await _context.SaveChangesAsync();

            return new InstitucionBootcampDto
            {
                IdInstitucion = institucionBootcamp.Id_Institucion,
                IdBootcamp = institucionBootcamp.Id_Bootcamp
            };
        }

        public async Task<bool> DeleteAsync(int idInstitucion, int idBootcamp)
        {
            var institucionBootcamp = await _context.InstitucionBootcamps
                .FirstOrDefaultAsync(ib => ib.Id_Institucion == idInstitucion && ib.Id_Bootcamp == idBootcamp);

            if (institucionBootcamp == null) return false;

            _context.InstitucionBootcamps.Remove(institucionBootcamp);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}