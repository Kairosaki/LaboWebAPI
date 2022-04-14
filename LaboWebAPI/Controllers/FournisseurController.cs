using LaboWebAPI.DTO.FournisseurDTO;
using LaboWebAPI.Services.FournisseurServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaboWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FournisseurController : ControllerBase
    {
        private readonly IFournisseurService _fournisseurService;

        public FournisseurController(IFournisseurService fournisseurService)
        {
            _fournisseurService = fournisseurService;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<FournisseurIndexDTO>))]
        public ActionResult<FournisseurIndexDTO> Index([FromQuery] FournisseurSearchDTO dto)
        {
            return Ok(_fournisseurService.Lire(dto));
        }

        [HttpGet("{id}")]
        [Produces(typeof(FournisseurIndexDTO))]
        public ActionResult<FournisseurIndexDTO> Find([FromRoute] long id)
        {
            FournisseurIndexDTO? fournisseur = _fournisseurService.LireUn(id);

            if (fournisseur == null)
            {
                return NotFound();
            }
            return Ok(fournisseur);
        }

        [HttpPost]
        [Produces(typeof(int))]
        public IActionResult Create([FromBody] FournisseurAddDTO dto)
        {
            long id = _fournisseurService.Ajouter(dto);
            if (id == -1)
            {
                return BadRequest();
            }
            return Ok(id);
        }

        [HttpPatch]
        [Produces(typeof(bool))]
        public IActionResult Update(long id, [FromBody] FournisseurEditDTO dto)
        {
            return Ok(_fournisseurService.Modifier(id, dto));
        }

        [HttpDelete]
        [Produces(typeof(bool))]
        public IActionResult Delete(long id)
        {
            if (!_fournisseurService.Supprimer(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
