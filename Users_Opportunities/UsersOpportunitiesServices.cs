using BackendProject.Data;
using Microsoft.EntityFrameworkCore;

namespace firstback.UsersOpportunities
{
    public class UsersOpportunitiesServices : IUsersOpportunitiesServices
    {
        private readonly ApplicationDbContext _context;

        public UsersOpportunitiesServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsersOpportunitiesDTO>> GetAllAsync()
        {
            return await _context.UsersOpportunities
            .Select(uo => new UsersOpportunitiesDTO
            {
                IdUser = uo.Id_User,
                IdOpportunity = uo.Id_Opportunity
            })
            .ToListAsync();
        }

        public async Task<UsersOpportunitiesDTO?> GetByIdAsync(int idUser, int idOpportunity)
        {
            return await _context.UsersOpportunities
            .Where(uo => uo.Id_User == idUser && uo.Id_Opportunity == idOpportunity)
            .Select(uo => new UsersOpportunitiesDTO
            {
                IdUser = uo.Id_User,
                IdOpportunity = uo.Id_Opportunity
            })
            .FirstOrDefaultAsync();
        }

        public async Task<UsersOpportunitiesDTO> CreateAsync(UsersOpportunitiesDTO usersOpportunitiesDTO)
        {
            var userOpportunities = UsersOpportunitiesMapper.MapToEntity(usersOpportunitiesDTO);

            _context.UsersOpportunities.Add(userOpportunities);
            await _context.SaveChangesAsync();

            return usersOpportunitiesDTO;
        }

        public async Task<bool> DeleteAsync(int userId, int opportunityId)
        {
            var userOpportunities = await _context.UsersOpportunities
                .FirstOrDefaultAsync(uo => uo.Id_User == userId && uo.Id_Opportunity == opportunityId);

            if (userOpportunities == null) return false;

            _context.UsersOpportunities.Remove(userOpportunities);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}