using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace firstback.Oportunidades
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OportunidadController : ControllerBase
    {
        private readonly IOportunidadService _oportunidadService;
        private readonly IMapper _mapper;

        public OportunidadController(IOportunidadService oportunidadService, IMapper mapper)
        {
            _oportunidadService = oportunidadService;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<OportunidadDTO>>> GetAll()
        {
            var oportunidades = await _oportunidadService.GetAllAsync();
            return Ok(oportunidades);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Oportunidad>> Get(int id)
        {
            var product = await _oportunidadService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] OportunidadDTO oportunidadDTO)
        {
            int id = await _oportunidadService.CreateAsync(oportunidadDTO);

            // Recupera la oportunidad creada
            var nuevaOportunidad = await _oportunidadService.GetByIdAsync(id);

            if (nuevaOportunidad == null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(Get), new { id = nuevaOportunidad.Id }, nuevaOportunidad);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] OportunidadDTO oportunidadDTO)
        {
            var existingOportunidad = await _oportunidadService.GetByIdAsync(id);
            if (existingOportunidad == null)
            {
                return NotFound();
            }

            _mapper.Map(oportunidadDTO, existingOportunidad);
            await _oportunidadService.UpdateAsync(id, oportunidadDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingOportunidad = await _oportunidadService.GetByIdAsync(id);
            if (existingOportunidad == null)
                return NotFound();
            await _oportunidadService.DeleteAsync(id);
            return NoContent();
        }
    }
}