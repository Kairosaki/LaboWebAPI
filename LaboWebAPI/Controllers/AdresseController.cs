using LaboWebAPI.DTO.AdresseDTO;
using LaboWebAPI.Services.AdresseServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaboWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdresseController : ControllerBase
    {
        private readonly IAdresseService _adresseService;

        public AdresseController(IAdresseService adresseService)
        {
            _adresseService = adresseService;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<AdresseIndexDTO>))]
        public ActionResult<AdresseIndexDTO> Index([FromQuery] AdresseSearchDTO dto)
        {
            return Ok(_adresseService.Lire(dto));
        }

        [HttpGet("{id}")]
        [Produces(typeof(AdresseIndexDTO))]
        public ActionResult<AdresseIndexDTO> Find([FromRoute] long id)
        {
            AdresseIndexDTO? adresse = _adresseService.LireUn(id);

            if (adresse == null)
            {
                return NotFound();
            }
            return Ok(adresse);
        }

        [HttpPatch]
        [Produces(typeof(bool))]
        public IActionResult Update(long id, [FromBody] AdresseEditDTO dto)
        {
            return Ok(_adresseService.Modifier(id, dto));
        }
    }
}
