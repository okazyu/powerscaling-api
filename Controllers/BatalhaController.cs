using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerScaling.Data;
using PowerScaling.Service;
using PowerScaling.DTO;

namespace PowerScaling.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BatalhaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly BatalhaService _batalhaService;

        public BatalhaController(AppDbContext context, BatalhaService batalhaService)
        {
            _context = context;
            _batalhaService = batalhaService;
        }

        [HttpPost("compare")]
        public async Task<IActionResult> Compare([FromBody] ComparacaoRequest request)
        {
            var personagemA = await _context.Personagens.FindAsync(request.PersonagemAId);
            var personagemB = await _context.Personagens.FindAsync(request.PersonagemBId);

            if (personagemA is null || personagemB is null) return NotFound("Os personagens não foram encontrados.");
            
            var resultado = await _batalhaService.Compare(personagemA, personagemB);
            return Ok(resultado);
        }

        [HttpGet("random-compare")]
        public async Task<IActionResult> ComparaAleatorio() 
        {
            var sorteados = await _batalhaService.SortearPar();

            if (sorteados.Count < 2)
                return BadRequest("Não tem personagens o suficiente");

            return Ok(sorteados);
        }
    }
}
