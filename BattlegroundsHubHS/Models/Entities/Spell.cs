using BattlegroundsHubHS.Models.Enums;

namespace BattlegroundsHubHS.Models.Entities
{
    /// Заклинание — карта, дающая временный эффект
    public class Spell
    {
        public int Id { get; set; }                          // Уникальный ID
        public int DbfId { get; set; }                      // ID из Blizzard
        public string Name { get; set; } = "";               // Название
        public string ImageUrl { get; set; } = "";           // Ссылка на картинку
        public int TavernTier { get; set; }                  // Уровень таверны (1-6)
        public string Effect { get; set; } = "";             // Эффект заклинания
        public int Cost { get; set; }                        // Стоимость в золоте
        public CardRarity Rarity { get; set; }               // Редкость
    }
}