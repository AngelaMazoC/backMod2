namespace firstback.InstitutionsOpportunity
{
    public class InstitutionOpportunityMapper
    {
        public static InstitutionOpportunity MapToEntity(InstitutionOpportunityDTO dto)
        {
            return new InstitutionOpportunity
            {
                Id_Institution = dto.IdInstitution,
                Id_Opportunity = dto.IdOpportunity
            };
        }
    }
}