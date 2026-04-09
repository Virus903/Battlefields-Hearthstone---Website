using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BattlegroundsHubHS.Data;

namespace BattlegroundsHubHS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StatsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить общую статистику по картам
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<object>> GetStats()
        {
            var heroesCount = await _context.Heroes.CountAsync();
            var minionsCount = await _context.Minions.CountAsync();
            var spellsCount = await _context.Spells.CountAsync();
            var questsCount = await _context.Quests.CountAsync();
            var rewardsCount = await _context.Rewards.CountAsync();
            var anomaliesCount = await _context.Anomalies.CountAsync();
            var accessoriesCount = await _context.Accessories.CountAsync();
            var chronomaliesCount = await _context.Chronomalies.CountAsync();
            var chronoSpellsCount = await _context.ChronoSpells.CountAsync();

            // Статистика по уровням таверны для миньонов
            var minionsByTier = new Dictionary<int, int>();
            for (int i = 1; i <= 7; i++)
            {
                var count = await _context.Minions.CountAsync(m => m.TavernTier == i);
                minionsByTier[i] = count;
            }

            // Статистика по типам миньонов
            var minionsByType = await _context.Minions
                .GroupBy(m => m.Type)
                .Select(g => new { Type = g.Key.ToString(), Count = g.Count() })
                .ToListAsync();

            return Ok(new
            {
                Heroes = heroesCount,
                Minions = minionsCount,
                Spells = spellsCount,
                Quests = questsCount,
                Rewards = rewardsCount,
                Anomalies = anomaliesCount,
                Accessories = accessoriesCount,
                Chronomalies = chronomaliesCount,
                ChronoSpells = chronoSpellsCount,
                Total = heroesCount + minionsCount + spellsCount + questsCount +
                        rewardsCount + anomaliesCount + accessoriesCount +
                        chronomaliesCount + chronoSpellsCount,
                MinionsByTier = minionsByTier,
                MinionsByType = minionsByType
            });
        }

        /// <summary>
        /// Получить статистику по героям (по рейтингам)
        /// </summary>
        [HttpGet("heroes")]
        public async Task<ActionResult<object>> GetHeroesStats()
        {
            var heroesByTier = await _context.Heroes
                .GroupBy(h => h.Tier)
                .Select(g => new { Tier = g.Key.ToString(), Count = g.Count() })
                .ToListAsync();

            return Ok(new
            {
                TotalHeroes = await _context.Heroes.CountAsync(),
                HeroesByTier = heroesByTier
            });
        }
    }
}