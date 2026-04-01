namespace PowerScaling.DTO
{
    public class CriarPersonagemRequest
    {
        public string Nome { get; set; } = string.Empty;
        public int Durabilidade { get; set; }
        public int Velocidade { get; set; }
        public int Força { get; set; }
        public int Inteligencia { get; set; }
        public int PoderDeFogo { get; set; }
        public string? ImageUrl { get; set; }
    }
}
