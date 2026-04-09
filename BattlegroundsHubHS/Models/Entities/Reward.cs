using BattlegroundsHubHS.Models.Enums;

namespace BattlegroundsHubHS.Models.Entities
{
    /// Награда — получается за выполнение задания
    public class Reward
    {
        public int Id { get; set; }                          // Уникальный ID
        public int DbfId { get; set; }                      // ID из Blizzard
        public string Name { get; set; } = "";               // Название
        public string ImageUrl { get; set; } = "";           // Ссылка на картинку
        public string Effect { get; set; } = "";             // Эффект награды
        public CardRarity Rarity { get; set; }               // Редкость
    }
}