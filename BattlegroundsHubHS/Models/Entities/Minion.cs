using BattlegroundsHubHS.Models.Enums;

namespace BattlegroundsHubHS.Models.Entities
{
    /// Миньон — существо, которое покупают в таверне
    public class Minion
    {
        public int Id { get; set; }                          // Уникальный ID
        public int DbfId { get; set; }                      // ID из Blizzard
        public string Name { get; set; } = "";               // Название миньона
        public string ImageUrl { get; set; } = "";           // Ссылка на картинку
        public int TavernTier { get; set; }                  // Уровень таверны (1-7)
        public MinionType Type { get; set; }                 // Племя (Демон, Мех и т.д.)
        public int Attack { get; set; }                      // Атака
        public int Health { get; set; }                      // Здоровье
        public string Effect { get; set; } = "";             // Текст способности
        public CardRarity Rarity { get; set; }               // Редкость
    }
}