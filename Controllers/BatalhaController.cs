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
            var personagemA = await _context.Personagens
                .FirstOrDefaultAsync(c => c.Id == request.CharacterAId);

            var personagemB = await _context.Personagens
                .FirstOrDefaultAsync(c => c.Id == request.CharacterBId);

            if (personagemA is null || personagemB is null) return NotFound("Um ou ambos personagens não foram encontrados");

            var result = _batalhaService.Compare(
                personagemA,
                request.MenaceA,
                personagemB,
                request.MenaceB
            );

            return Ok(result);
        }
    }
}
