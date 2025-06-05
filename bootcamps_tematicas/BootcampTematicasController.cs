using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace firstback.BootcampsTematicas
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BootcampTematicasController : ControllerBase
    {
        private readonly IBootcampTematicaService _service;

        public BootcampTematicasController(IBootcampTematicaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BootcampTematicaDto>>> GetAll()
        {
            var bootcampTematicas = await _service.GetAllAsync();
            return Ok(bootcampTematicas);
        }

        [HttpGet("{idBootcamp}/{idTematica}")]
        public async Task<ActionResult<BootcampTematicaDto>> GetById(int idBootcamp, int idTematica)
        {
            var bootcampTematica = await _service.GetByIdAsync(idBootcamp, idTematica);
            if (bootcampTematica == null) return NotFound("No se encontró la relación Bootcamp-Tematica.");
            return Ok(bootcampTematica);
        }

        [HttpPost]
        public async Task<ActionResult<BootcampTematicaDto>> Create([FromBody] BootcampTematicaDto bootcampTematicaDto)
        {
            var bootcampTematica = await _service.CreateAsync(bootcampTematicaDto);
            return CreatedAtAction(nameof(GetById), new { idBootcamp = bootcampTematica.IdBootcamp, idTematica = bootcampTematica.IdTematica }, bootcampTematica);
        }

        [HttpDelete("{idBootcamp}/{idTematica}")]
        public async Task<IActionResult> Delete(int idBootcamp, int idTematica)
        {
            var deleted = await _service.DeleteAsync(idBootcamp, idTematica);
            if (!deleted) return NotFound("No se encontró la relación Bootcamp-Tematica.");
            return NoContent();
        }
    }
}