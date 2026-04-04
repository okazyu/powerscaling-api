namespace PowerScaling.DTO
{
    public class ComparacaoResponse
    {
        public string NomePersonagemA { get; set; } = string.Empty;
        public decimal MediaPersonagemA { get; set; }
        public decimal MultiplicadorPersonagemA { get; set; }
        public decimal PontuacaoFinalPersonagemA { get; set; }

        public string NomePersonagemB { get; set; } = string.Empty;
        public decimal MediaPersonagemB { get; set; }
        public decimal MultiplicadorPersonagemB { get; set; }
        public decimal PontuacaoFinalPersonagemB { get; set; }

        public string? ImageUrlPersonagemA { get; set; }
        public string? ImageUrlPersonagemB { get; set; }

        public string Resultado { get; set; } = string.Empty;
        public string? Vencedor { get; set; }
    }
}
