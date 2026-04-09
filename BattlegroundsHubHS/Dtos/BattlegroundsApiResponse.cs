using System.Text.Json.Serialization;

namespace BattlegroundsHubHS.Dtos
{
    /// <summary>
    /// Корневой объект ответа от API Blizzard
    /// </summary>
    public class BattlegroundsApiResponse
    {
        [JsonPropertyName("cards")]
        public List<CardDto> Cards { get; set; } = new();

        [JsonPropertyName("cardCount")]
        public int CardCount { get; set; }
    }
}