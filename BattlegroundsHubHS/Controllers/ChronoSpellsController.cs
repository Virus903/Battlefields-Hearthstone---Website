using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BattlegroundsHubHS.Data;
using BattlegroundsHubHS.Models.Entities;
using BattlegroundsHubHS.Models.Enums;

namespace BattlegroundsHubHS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChronoSpellsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ChronoSpellsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить все хрономальные заклинания
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChronoSpell>>> GetAll()
        {
            var chronoSpells = await _context.ChronoSpells
                .OrderBy(c => c.TavernTier)
                .ThenBy(c => c.Name)
                .ToListAsync();

            return Ok(chronoSpells);
        }

        /// <summary>
        /// Получить хрономальные заклинания по уровню таверны
        /// </summary>
        [HttpGet("tier/{tier}")]
        public async Task<ActionResult<IEnumerable<ChronoSpell>>> GetByTier(int tier)
        {
            if (tier != 3 && tier != 5)
                return BadRequest("Хрономальные заклинания бывают только 3-го или 5-го уровня");

            var chronoSpells = await _context.ChronoSpells
                .Where(c => c.TavernTier == tier)
                .OrderBy(c => c.Name)
                .ToListAsync();

            return Ok(chronoSpells);
        }
    }
}