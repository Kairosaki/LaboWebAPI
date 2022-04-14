using LaboWebAPI.DTO.BouteilleDTO;
using LaboWebAPI.Services.BouteilleServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaboWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BouteilleController : ControllerBase
    {
        private readonly IBouteilleService _bouteilleService;

        public BouteilleController(IBouteilleService bouteilleService)
        {
            _bouteilleService = bouteilleService;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<BouteilleIndexDTO>))]
        public ActionResult<BouteilleIndexDTO> Index([FromQuery] BouteilleSearchDTO dto)
        {
            return Ok(_bouteilleService.Lire(dto));
        }

        [HttpGet("{id}")]
        [Produces(typeof(BouteilleIndexDTO))]
        public ActionResult<BouteilleIndexDTO> Find([FromRoute] long id)
        {
            BouteilleIndexDTO? bouteille = _bouteilleService.LireUn(id);

            if (bouteille == null)
            {
                return NotFound();
            }
            return Ok(bouteille);
        }

        [HttpPost]
        [Produces(typeof(int))]
        public IActionResult Create([FromBody] BouteilleAddDTO dto)
        {
            long id = _bouteilleService.Ajouter(dto);
            if (id == -1)
            {
                return BadRequest();
            }
            return Ok(id);
        }

        [HttpPatch]
        [Produces(typeof(bool))]
        public IActionResult Update(long id, [FromBody] BouteilleEditDTO dto)
        {
            return Ok(_bouteilleService.Modifier(id, dto));
        }

        [HttpDelete]
        [Produces(typeof(bool))]
        public IActionResult Delete(long id)
        {
            if (!_bouteilleService.Supprimer(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
