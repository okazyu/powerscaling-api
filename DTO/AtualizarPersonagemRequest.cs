using PowerScaling.Enums;

namespace PowerScaling.DTO
{
    public class AtualizarPersonagemRequest
    {
        public string? Nome { get; set; }
        public int? Durabilidade { get; set; }
        public int? Velocidade { get; set; }
        public int? Força { get; set; }
        public int? Inteligencia { get; set; }
        public int? PoderDeFogo { get; set; }
        public LevelMenace? Menace { get; set; }
        public string? ImageUrl { get; set; }
    }
}
