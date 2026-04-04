using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerScaling.Data;
using PowerScaling.DTO;
using PowerScaling.Entities;

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
            Console.WriteLine($"Create - Context hash: {_context.GetHashCode()}");

            var personagem = new Personagens
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome,
                Resistencia = request.Durabilidade,
                Velocidade = request.Velocidade,
                Força = request.Força,
                Intelecto = request.Inteligencia,
                PoderDeFogo = request.PoderDeFogo,
                Menace = request.Menace,
                ImageUrl = request.ImageUrl
            };

            try
            {
                _context.Personagens.Add(personagem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

            return Ok(personagem);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            Console.WriteLine($"GetById - Context hash: {_context.GetHashCode()}");

            var personagem = await _context.Personagens.FindAsync(id);

            if (personagem is null) return NotFound();

            return Ok(personagem);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePersonagem(Guid id)
        {
            var personagem = await _context.Personagens.FindAsync(id);

            if (personagem is not null)
            {
                _context.Personagens.Remove(personagem);
                await _context.SaveChangesAsync();
            }
            else return NotFound();

            return Ok();
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> UpdatePersonagem(Guid id, AtualizarPersonagemRequest atualizarPersonagem)
        {
            var personagem = await _context.Personagens.FindAsync(id);

            if (personagem is null) return NotFound();

            if (atualizarPersonagem.Nome is not null) { personagem.Nome = atualizarPersonagem.Nome; }

            ApplyIfNotNull(atualizarPersonagem.Durabilidade, v => personagem.Resistencia = v);
            ApplyIfNotNull(atualizarPersonagem.Força, v => personagem.Força = v);
            ApplyIfNotNull(atualizarPersonagem.Inteligencia, v => personagem.Intelecto= v);
            ApplyIfNotNull(atualizarPersonagem.Velocidade, v => personagem.Velocidade = v);
            ApplyIfNotNull(atualizarPersonagem.PoderDeFogo, v => personagem.PoderDeFogo = v);

            ApplyIfValid(atualizarPersonagem.Durabilidade, v => v >= 0 && v <= 100, v => personagem.Resistencia = v);
            ApplyIfValid(atualizarPersonagem.Força, v => v >= 0 && v <= 100, v => personagem.Força = v);
            ApplyIfValid(atualizarPersonagem.Inteligencia, v => v >= 0 && v <= 100, v => personagem.Intelecto = v);
            ApplyIfValid(atualizarPersonagem.Velocidade, v => v >= 0 && v <= 100, v => personagem.Velocidade = v);
            ApplyIfValid(atualizarPersonagem.PoderDeFogo, v => v >= 0 && v <= 100, v => personagem.PoderDeFogo = v);

            await _context.SaveChangesAsync();

            return Ok(personagem);
        }

        private void ApplyIfNotNull<T>(T? value, Action<T> updateAction) where T : struct
        {
            if (value.HasValue) { updateAction(value.Value); }
        }


        private void ApplyIfValid<T>(
            T? value,
            Func<T, bool> isValid,
            Action<T> updateAction) where T : struct
        {
            if (value.HasValue && isValid(value.Value)) { updateAction(value.Value); }
        }
    }
}
