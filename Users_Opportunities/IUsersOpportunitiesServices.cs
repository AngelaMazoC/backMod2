namespace firstback.UsersOpportunities
{
    public interface IUsersOpportunitiesServices
    {
        Task<IEnumerable<UsersOpportunitiesDTO>>GetAllAsync();
        Task<UsersOpportunitiesDTO?> GetByIdAsync(int idUser, int idOpportunity);
        Task<UsersOpportunitiesDTO>CreateAsync(UsersOpportunitiesDTO usersOpportunitiesDTO);
        Task<bool> DeleteAsync(int idUser, int idOpportunity);
    }
}