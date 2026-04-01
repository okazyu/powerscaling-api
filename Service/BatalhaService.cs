using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PowerScaling.Data;
using PowerScaling.DTO;
using PowerScaling.Entities;
using PowerScaling.Enums;

namespace PowerScaling.Service
{
    public class BatalhaService
    {
        private readonly AppDbContext _context;

        public BatalhaService(AppDbContext context)
        {
            _context = context;
        }

        public decimal CalcularMedia(Personagens personagens) 
        {
            return (personagens.Resistencia +
                    personagens.Velocidade +
                    personagens.Força +
                    personagens.Intelecto +
                    personagens.PoderDeFogo) / 5m;

        }

        public decimal MultiplicadorMenace(LevelMenace menace)
        {
            return menace switch
            {
                LevelMenace.Covarde => 0.50m,
                LevelMenace.Pacifista => 0.75m,
                LevelMenace.Normal => 1.0m,
                LevelMenace.Agressivo => 1.25m,
                LevelMenace.Homicida => 1.5m,
                _ => 1.0m
            };
        }

        private decimal CalcularPontuacaoFinal(Personagens personagens)
        {
            return CalcularMedia(personagens) * MultiplicadorMenace(personagens.Menace);
        }

        public async Task<ComparacaoResponse> Compare()
        {
            var sorteados = await SortearPar();

            if (sorteados.Count < 2)
                throw new InvalidOperationException("Não tem boneco o suficiente");

            var personagemA = sorteados[0];
            var personagemB = sorteados[1];

            var pontuacaoA = CalcularPontuacaoFinal(personagemA);
            var pontuacaoB = CalcularPontuacaoFinal(personagemB);

            var menaceA = MultiplicadorMenace(personagemA.Menace);
            var menaceB = MultiplicadorMenace(personagemB.Menace);

            var mediaA = CalcularMedia(personagemA);
            var mediaB = CalcularMedia(personagemB);

            var vencedor = pontuacaoA > pontuacaoB ? personagemA.Nome : pontuacaoB > pontuacaoA ? personagemB.Nome : "Draw";

            return new ComparacaoResponse
            {
                NomePersonagemA = personagemA.Nome,
                NomePersonagemB = personagemB.Nome,
                MediaPersonagemA = mediaA,
                MediaPersonagemB = mediaB,
                MultiplicadorPersonagemA = menaceA,
                MultiplicadorPersonagemB = menaceB,
                PontuacaoFinalPersonagemA = pontuacaoA,
                PontuacaoFinalPersonagemB = pontuacaoB,
                Vencedor = vencedor,
            };
            
        }

        public async Task<List<Personagens>> SortearPar()
        {
            var personagens = await _context.Personagens.ToListAsync();

            if (personagens.Count < 2)
                return new List<Personagens>();

            var random = new Random();

            return personagens
                .OrderBy(x => random.Next())
                .Take(2)
                .ToList();
        }
    }
}
