using PowerScaling.Enums;

namespace PowerScaling.DTO
{
    public class ComparacaoRequest
    {
        public Guid CharacterAId { get; set; }
        public Guid CharacterBId { get; set; }
        public LevelMenace MenaceA { get; set; }
        public LevelMenace MenaceB { get; set; }
    }
}
