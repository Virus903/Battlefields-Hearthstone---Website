using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BattlegroundsHubHS.Data;
using BattlegroundsHubHS.Models.Entities;
using BattlegroundsHubHS.Models.Enums;

namespace BattlegroundsHubHS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChronomaliesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ChronomaliesController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить все хрономалии (миньоны)
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chronomaly>>> GetAll()
        {
            var chronomalies = await _context.Chronomalies
                .OrderBy(c => c.TavernTier)
                .ThenBy(c => c.Name)
                .ToListAsync();

            return Ok(chronomalies);
        }

        /// <summary>
        /// Получить хрономалии по уровню таверны (3 или 5)
        /// </summary>
        [HttpGet("tier/{tier}")]
        public async Task<ActionResult<IEnumerable<Chronomaly>>> GetByTier(int tier)
        {
            if (tier != 3 && tier != 5)
                return BadRequest("Хрономалии бывают только 3-го или 5-го уровня");

            var chronomalies = await _context.Chronomalies
                .Where(c => c.TavernTier == tier)
                .OrderBy(c => c.Name)
                .ToListAsync();

            return Ok(chronomalies);
        }

        /// <summary>
        /// Получить хрономалии по типу/племени
        /// </summary>
        [HttpGet("type/{type}")]
        public async Task<ActionResult<IEnumerable<Chronomaly>>> GetByType(MinionType type)
        {
            var chronomalies = await _context.Chronomalies
                .Where(c => c.Type == type)
                .OrderBy(c => c.TavernTier)
                .ThenBy(c => c.Name)
                .ToListAsync();

            return Ok(chronomalies);
        }
    }
}