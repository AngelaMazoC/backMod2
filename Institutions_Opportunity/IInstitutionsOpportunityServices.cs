namespace firstback.InstitutionsOpportunity
{
    public interface IInstitutionsOpportunityService
    {
        Task<IEnumerable<InstitutionOpportunityDTO>> getAllAsync();
        Task<InstitutionOpportunityDTO?> getByIdAsync(int idInstitucion, int idOpportunity);
        Task<InstitutionOpportunityDTO> createAsync(InstitutionOpportunityDTO institucionOpportunityDTO);
        Task<bool> deleteAsync(int idInstitucion, int idOpportunity);
    }
}