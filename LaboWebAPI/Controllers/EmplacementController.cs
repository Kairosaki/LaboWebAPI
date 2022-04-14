using LaboWebAPI.DTO.EmplacementDTO;
using LaboWebAPI.Services.EmplacementServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaboWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmplacementController : ControllerBase
    {
        private readonly IEmplacementService _emplacementService;

        public EmplacementController(IEmplacementService emplacementService)
        {
            _emplacementService = emplacementService;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<EmplacementIndexDTO>))]
        public ActionResult<EmplacementIndexDTO> Index([FromQuery] EmplacementSearchDTO dto)
        {
            return Ok(_emplacementService.Lire(dto));
        }

        [HttpGet("{id}")]
        [Produces(typeof(EmplacementIndexDTO))]
        public ActionResult<EmplacementIndexDTO> Find([FromRoute] long id)
        {
            EmplacementIndexDTO? emplacement = _emplacementService.LireUn(id);

            if (emplacement == null)
            {
                return NotFound();
            }
            return Ok(emplacement);
        }

        [HttpGet("libres")]
        [Produces(typeof(IEnumerable<EmplacementIndexDTO>))]
        public ActionResult<EmplacementIndexDTO> FindLibres()
        {
            return Ok(_emplacementService.GetAllLibres());
        }

        [HttpPost]
        [Produces(typeof(int))]
        public IActionResult Create([FromBody] EmplacementAddDTO dto)
        {
            long id = _emplacementService.Ajouter(dto);
            if (id == -1)
            {
                return BadRequest();
            }
            return Ok(id);
        }

        [HttpPatch("{id}")]
        [Produces(typeof(bool))]
        public IActionResult Update([FromRoute] long id, [FromBody] EmplacementEditDTO dto)
        {
            try
            {
                if (!_emplacementService.Modifier(id, dto))
                {
                    return NotFound();
                }
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("{id}")]
        [Produces(typeof(bool))]
        public IActionResult UpdatePlace([FromRoute] long id)
        {
            if (!_emplacementService.ModifierPlace(id))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut()]
        [Produces(typeof(bool))]
        public IActionResult SetAllPlacesToTrue()
        {
            if (!_emplacementService.ModifierAllPlaces())
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
