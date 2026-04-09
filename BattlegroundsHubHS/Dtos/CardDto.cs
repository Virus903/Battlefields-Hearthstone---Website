using System.Text.Json.Serialization;

namespace BattlegroundsHubHS.Dtos
{
    /// <summary>
    /// DTO для карты из API Blizzard
    /// </summary>
    public class CardDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }  // Это dbfId!

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("text")]
        public string Text { get; set; } = "";

        [JsonPropertyName("image")]
        public string ImageUrl { get; set; } = "";

        [JsonPropertyName("attack")]
        public int Attack { get; set; }

        [JsonPropertyName("health")]
        public int Health { get; set; }

        [JsonPropertyName("armor")]
        public int Armor { get; set; }

        [JsonPropertyName("cardTypeId")]
        public int CardTypeId { get; set; }

        [JsonPropertyName("minionTypeId")]
        public int? MinionTypeId { get; set; }

        [JsonPropertyName("battlegrounds")]
        public BattlegroundsDto? Battlegrounds { get; set; }
    }
}