using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace firstback.InstitutionsOpportunity
{
    [ApiController]
    [Route("Api/[Controller]")]
    [Authorize]
    public class InstitutionsOpportunityController : ControllerBase
    {
        private readonly IInstitutionsOpportunityService _service;

        public InstitutionsOpportunityController(IInstitutionsOpportunityService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> getAll()
        {
            try
            {
                var result = await _service.getAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{institutionId}/{opportunityId}")]
        public async Task<ActionResult> getById(int institutionId, int opportunityId)
        {
            try
            {
                var result = await _service.getByIdAsync(institutionId, opportunityId);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> createInstitutionOpportunity([FromBody] InstitutionOpportunityDTO institutionOpportunityDTO)
        {
            if (institutionOpportunityDTO == null)
                return BadRequest("Los datos no pueden ser nulos.");

            try
            {
                var result = await _service.createAsync(institutionOpportunityDTO);
                return CreatedAtAction(nameof(getById),
                    new { institutionId = result.IdInstitution, opportunityId = result.IdOpportunity }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{institutionId}/{opportunityId}")]
        public async Task<ActionResult> Delete(int institutionId, int opportunityId)
        {
            try
            {
                var result = await _service.deleteAsync(institutionId, opportunityId);
                if (!result) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}