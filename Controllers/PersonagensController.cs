using Microsoft.AspNetCore.Mvc;
using PowerScaling.Data;
using PowerScaling.DTO;
using PowerScaling.Entities;
using System.Data.Entity;
using System.Reflection.PortableExecutable;

namespace PowerScaling.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonagensController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PersonagensController(AppDbContext contexto)
        {
            _context = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var personagens = await _context.Personagens.ToListAsync();
            return Ok(personagens);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CriarPersonagemRequest request)
        {
            var personagem = new Personagens
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome,
                Resistencia = request.Durabilidade,
                Velocidade = request.Velocidade,
                Força = request.Força,
                Intelecto = request.Inteligencia,
                PoderDeFogo = request.PoderDeFogo,
                ImageUrl = request.ImageUrl,
            };

            _context.Personagens.Add(personagem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = personagem.Id }, personagem);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var personagem = await _context.Personagens.FindAsync(id);

            if (personagem is null) return NotFound();

            return Ok(personagem);
        }
    }
}
