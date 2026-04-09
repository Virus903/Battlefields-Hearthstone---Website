using System.Text.Json.Serialization;

namespace BattlegroundsHubHS.Dtos
{
    /// <summary>
    /// Battlegrounds-специфичные данные карты
    /// </summary>
    public class BattlegroundsDto
    {
        [JsonPropertyName("hero")]
        public bool IsHero { get; set; }

        [JsonPropertyName("tier")]
        public int Tier { get; set; }

        [JsonPropertyName("quest")]
        public bool IsQuest { get; set; }

        [JsonPropertyName("reward")]
        public bool IsReward { get; set; }

        [JsonPropertyName("heroPowerId")]
        public int? HeroPowerId { get; set; }

        [JsonPropertyName("image")]
        public string ImageUrl { get; set; } = "";

        [JsonPropertyName("upgradeId")]
        public int? UpgradeId { get; set; }
    }
}