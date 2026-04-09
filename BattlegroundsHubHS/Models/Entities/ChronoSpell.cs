using BattlegroundsHubHS.Models.Enums;

namespace BattlegroundsHubHS.Models.Entities
{
    /// Хрономальное заклинание — только заклинания из хрономалий
    public class ChronoSpell
    {
        public int Id { get; set; }                          // Уникальный ID
        public int DbfId { get; set; }                      // ID из Blizzard
        public string Name { get; set; } = "";               // Название
        public string ImageUrl { get; set; } = "";           // Ссылка на картинку
        public int TavernTier { get; set; }                  // 3 или 5
        public MinionType Type { get; set; }                 // Племя
        public string Effect { get; set; } = "";             // Эффект
        public int Cost { get; set; }                        // Стоимость
        public CardRarity Rarity { get; set; }               // Редкость
    }
}