using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace firstback.InstitucionesBootcamps
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InstitucionBootcampController : ControllerBase
    {
        private readonly IInstitucionBootcampService _service;

        public InstitucionBootcampController(IInstitucionBootcampService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstitucionBootcamp>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{idInstitucion}/{idBootcamp}")]
        public async Task<ActionResult<InstitucionBootcamp>> GetById(int idInstitucion, int idBootcamp)
        {
            var result = await _service.GetByIdAsync(idInstitucion, idBootcamp);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<InstitucionBootcampDto>> Create([FromBody] InstitucionBootcampDto institucionBootcampDto)
        {
            if (institucionBootcampDto == null)
                return BadRequest("Los datos no pueden ser nulos.");

            var result = await _service.CreateAsync(institucionBootcampDto);
            return CreatedAtAction(nameof(GetById), new { idInstitucion = result.IdInstitucion, idBootcamp = result.IdBootcamp }, result);
        }

        [HttpDelete("{idInstitucion}/{idBootcamp}")]
        public async Task<IActionResult> Delete(int idInstitucion, int idBootcamp)
        {
            var deleted = await _service.DeleteAsync(idInstitucion, idBootcamp);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}