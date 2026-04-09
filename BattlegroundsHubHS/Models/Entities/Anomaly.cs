namespace BattlegroundsHubHS.Models.Entities
{
    /// Аномалия — глобальное изменение правил игры
    public class Anomaly
    {
        public int Id { get; set; }                          // Уникальный ID
        public int DbfId { get; set; }                      // ID из Blizzard
        public string Name { get; set; } = "";               // Название
        public string ImageUrl { get; set; } = "";           // Ссылка на картинку
        public string Effect { get; set; } = "";             // Эффект аномалии
    }
}