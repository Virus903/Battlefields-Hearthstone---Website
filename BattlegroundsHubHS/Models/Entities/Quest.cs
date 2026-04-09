namespace BattlegroundsHubHS.Models.Entities
{
    /// Задание — задача, которую нужно выполнить за игру
    public class Quest
    {
        public int Id { get; set; }                          // Уникальный ID
        public int DbfId { get; set; }                      // ID из Blizzard
        public string Name { get; set; } = "";               // Название
        public string ImageUrl { get; set; } = "";           // Ссылка на картинку
        public string Requirement { get; set; } = "";        // Что нужно сделать
        public string RewardDescription { get; set; } = "";  // Описание награды
        public int? RewardId { get; set; }                   // Ссылка на награду
        public Reward? Reward { get; set; }
    }

}