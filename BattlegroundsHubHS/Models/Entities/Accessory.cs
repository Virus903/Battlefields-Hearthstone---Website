namespace BattlegroundsHubHS.Models.Entities
{
    /// Аксессуар — декоративный предмет (косметика)
    public class Accessory
    {
        public int Id { get; set; }                          // Уникальный ID
        public int DbfId { get; set; }                      // ID из Blizzard
        public string Name { get; set; } = "";               // Название
        public string ImageUrl { get; set; } = "";           // Ссылка на картинку
        public string Effect { get; set; } = "";             // Визуальный эффект
    }
}