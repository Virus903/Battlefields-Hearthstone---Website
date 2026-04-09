using BattlegroundsHubHS.Models.Enums;

namespace BattlegroundsHubHS.Models.Entities
{
    /// Хрономалия — особая карта (уровень таверны 3 или 5)
    public class Chronomaly
    {
        public int Id { get; set; }                          // Уникальный ID
        public int DbfId { get; set; }                      // ID из Blizzard
        public string Name { get; set; } = "";               // Название
        public string ImageUrl { get; set; } = "";           // Ссылка на картинку
        public int TavernTier { get; set; }                  // 3 или 5
        public MinionType Type { get; set; }                 // Племя
        public string Effect { get; set; } = "";             // Эффект
        public bool IsSpell { get; set; }                    // true=заклинание, false=миньон
        public int Cost { get; set; }                        // Стоимость (если заклинание)
        public int Attack { get; set; }                      // Атака (если миньон)
        public int Health { get; set; }                      // Здоровье (если миньон)
        public CardRarity Rarity { get; set; }               // Редкость
    }
}