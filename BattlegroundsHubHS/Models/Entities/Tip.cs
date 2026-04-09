namespace BattlegroundsHubHS.Models.Entities
{
    /// Совет — помощь игрокам
    public class Tip
    {
        public int Id { get; set; }                          // Уникальный ID
        public string Title { get; set; } = "";              // Заголовок
        public string Content { get; set; } = "";            // Текст совета
        public string Category { get; set; } = "";           // "Новичкам", "Стратегия", "Мета"
        public int Priority { get; set; } = 1;               // Важность (1-10)
        public string ImageUrl { get; set; } = "";           // Иллюстрация
    }
}