namespace firstback.UsersOpportunities
{
    public class UsersOpportunitiesMapper
    {
        public static UsersOpportunities MapToEntity(UsersOpportunitiesDTO dto)
        {
            return new UsersOpportunities
            {
                Id_User = dto.IdUser,
                Id_Opportunity = dto.IdOpportunity
            };
        }
    }
}
