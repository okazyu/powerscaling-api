using PowerScaling.DTO;
using PowerScaling.Entities;
using PowerScaling.Enums;
using System.Reflection.PortableExecutable;

namespace PowerScaling.Service
{
    public class BatalhaService
    {
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

        private decimal CalcularPontuacaoFinal(Personagens personagens, LevelMenace menace)
        {
            return CalcularMedia(personagens) * MultiplicadorMenace(menace);
        }

        public ComparacaoResponse Compare(Personagens personagemA, LevelMenace menaceA, Personagens personagemB, LevelMenace menaceB)
        {
            var mediaA = CalcularMedia(personagemA);
            var mediaB = CalcularMedia(personagemB);

            var multiplicadorA = MultiplicadorMenace(menaceA);
            var multiplicadorB = MultiplicadorMenace(menaceB);

            var pontuacaoA = CalcularPontuacaoFinal(personagemA, menaceA);
            var pontuacaoB = CalcularPontuacaoFinal(personagemB, menaceB);

            string resultado;
            string? vencedor = null;

            if (pontuacaoA > pontuacaoB) { resultado = "A venceu"; vencedor = personagemA.Nome; }
            else if (pontuacaoB > pontuacaoA) { resultado = "B venceu"; vencedor = personagemB.Nome; }
            else { resultado = "Empate"; }

            return new ComparacaoResponse
            {
                NomePersonagemA = personagemA.Nome,
                MediaPersonagemA = mediaA,
                MultiplicadorPersonagemA = multiplicadorA,
                PontuacaoFinalPersonagemA = pontuacaoA,

                NomePersonagemB = personagemB.Nome,
                MediaPersonagemB = mediaB,
                MultiplicadorPersonagemB = multiplicadorB,
                PontuacaoFinalPersonagemB = pontuacaoB,

                Resultado = resultado,
                Vencedor = vencedor
            };
        }
    }
}
