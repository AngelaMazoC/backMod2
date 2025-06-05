using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace firstback.UsersOpportunities
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersOpportunitiesController : ControllerBase
    {
        private readonly IUsersOpportunitiesServices _service;

        public UsersOpportunitiesController(IUsersOpportunitiesServices service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{userId}/{opportunityId}")]
        public async Task<IActionResult> GetById(int userId, int opportunityId)
        {
            try
            {
                var result = await _service.GetByIdAsync(userId, opportunityId);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserOpportunity([FromBody] UsersOpportunitiesDTO usersOpportunityDTO)
        {
            if (usersOpportunityDTO == null)
                return BadRequest("Los datos no pueden ser nulos.");

            try
            {
                var result = await _service.CreateAsync(usersOpportunityDTO);
                return CreatedAtAction(nameof(GetById), 
                    new { userId = result.IdUser, opportunityId = result.IdOpportunity }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{userId}/{opportunityId}")]
        public async Task<IActionResult> Delete(int userId, int opportunityId)
        {
            try
            {
                var deleted = await _service.DeleteAsync(userId, opportunityId);
                if (!deleted) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}