using BackendProject.Data;
using Microsoft.EntityFrameworkCore;

namespace firstback.InstitutionsOpportunity
{
    public class InstitutionsOpportunityService : IInstitutionsOpportunityService
    {
        private readonly ApplicationDbContext _context;

        public InstitutionsOpportunityService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InstitutionOpportunityDTO>> getAllAsync()
        {
            return await _context.InstitutionOpportunities
            .Select(io => new InstitutionOpportunityDTO
            {
                IdInstitution = io.Id_Institution,
                IdOpportunity = io.Id_Opportunity
            })
            .ToListAsync();
        }

        public async Task<InstitutionOpportunityDTO?> getByIdAsync(int IdInstitution, int IdOpportunity)
        {
            return await _context.InstitutionOpportunities
            .Where(io => io.Id_Institution == IdInstitution && io.Id_Opportunity == IdOpportunity)
            .Select(io => new InstitutionOpportunityDTO
            {
                IdInstitution = io.Id_Institution,
                IdOpportunity = io.Id_Opportunity
            })
            .FirstOrDefaultAsync();
        }

        public async Task<InstitutionOpportunityDTO> createAsync(InstitutionOpportunityDTO institutionOpportunityDTO)
        {
            var institutionOpportunity = InstitutionOpportunityMapper.MapToEntity(institutionOpportunityDTO);

            _context.InstitutionOpportunities.Add(institutionOpportunity);
            await _context.SaveChangesAsync();

            return institutionOpportunityDTO;
        }

        public async Task<bool> deleteAsync(int institutionId, int opportunityId)
        {
            var institutionOpportunity = await _context.InstitutionOpportunities
                .FirstOrDefaultAsync(io => io.Id_Institution == institutionId && io.Id_Opportunity == opportunityId);

            if (institutionOpportunity == null) return false;

            _context.InstitutionOpportunities.Remove(institutionOpportunity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}