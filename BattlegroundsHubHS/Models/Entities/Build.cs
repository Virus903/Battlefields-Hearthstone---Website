using BattlegroundsHubHS.Models.Enums;

namespace BattlegroundsHubHS.Models.Entities
{
    /// Сборка — готовая стратегия/композиция
    public class Build
    {
        public int Id { get; set; }                          // Уникальный ID
        public string Name { get; set; } = "";               // Название (например, "Демоны")
        public string Description { get; set; } = "";        // Описание
        public string Strategy { get; set; } = "";           // Пошаговая стратегия
        public string ScreenshotUrl { get; set; } = "";      // Ссылка на скриншот
        public string Strengths { get; set; } = "";          // Сильные стороны
        public string Weaknesses { get; set; } = "";         // Слабые стороны
        public HeroTier Tier { get; set; }                   // Рейтинг сборки
        public MinionType? MainType { get; set; }            // Основное племя сборки
    }
}