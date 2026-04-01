using PowerScaling.Enums;

namespace PowerScaling.Entities
{
    public class Personagens
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Resistencia { get; set; }
        public int Velocidade { get; set; }
        public int Força { get; set; }
        public int Intelecto { get; set; }
        public int PoderDeFogo { get; set; }
        public LevelMenace Menace { get; set; }
        public string? ImageUrl { get; set; }
    }
}
