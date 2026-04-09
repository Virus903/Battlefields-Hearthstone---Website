using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BattlegroundsHubHS.Data;
using BattlegroundsHubHS.Models.Entities;

namespace BattlegroundsHubHS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpellsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SpellsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить все заклинания
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Spell>>> GetAll()
        {
            var spells = await _context.Spells
                .OrderBy(s => s.TavernTier)
                .ThenBy(s => s.Name)
                .ToListAsync();

            return Ok(spells);
        }

        /// <summary>
        /// Получить заклинания по уровню таверны
        /// </summary>
        [HttpGet("tier/{tier}")]
        public async Task<ActionResult<IEnumerable<Spell>>> GetByTier(int tier)
        {
            if (tier < 1 || tier > 6)
                return BadRequest("Уровень таверны должен быть от 1 до 6");

            var spells = await _context.Spells
                .Where(s => s.TavernTier == tier)
                .OrderBy(s => s.Name)
                .ToListAsync();

            return Ok(spells);
        }
    }
}