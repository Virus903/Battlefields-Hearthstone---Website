using BattlegroundsHubHS.Models.Enums;

namespace BattlegroundsHubHS.Models.Entities
{
    /// Герой — игровой персонаж со своей силой героя
    public class Hero
    {
        public int Id { get; set; }                          // Уникальный ID в БД
        public int DbfId { get; set; }                      // ID из базы Blizzard (для картинки)
        public string Name { get; set; } = "";               // Имя героя
        public string ImageUrl { get; set; } = "";           // Ссылка на картинку
        public string HeroPower { get; set; } = "";          // Название силы героя
        public string HeroPowerDescription { get; set; } = ""; // Описание силы героя
        public int Armor { get; set; }                       // Броня героя
        public HeroTier Tier { get; set; }                   // Рейтинг S/A/B/C/D/F
    }
}