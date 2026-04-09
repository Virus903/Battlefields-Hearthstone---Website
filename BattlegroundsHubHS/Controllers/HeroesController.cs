using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BattlegroundsHubHS.Data;
using BattlegroundsHubHS.Models.Entities;
using BattlegroundsHubHS.Models.Enums;

namespace BattlegroundsHubHS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeroesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HeroesController> _logger;

        public HeroesController(AppDbContext context, ILogger<HeroesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Получить всех героев
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hero>>> GetAll()
        {
            var heroes = await _context.Heroes
                .OrderBy(h => h.Name)
                .ToListAsync();

            return Ok(heroes);
        }

        /// <summary>
        /// Получить героя по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Hero>> GetById(int id)
        {
            var hero = await _context.Heroes.FindAsync(id);

            if (hero == null)
                return NotFound($"Герой с ID {id} не найден");

            return Ok(hero);
        }

        /// <summary>
        /// Получить героев по рейтингу (S, A, B, C, D, F)
        /// </summary>
        [HttpGet("tier/{tier}")]
        public async Task<ActionResult<IEnumerable<Hero>>> GetByTier(HeroTier tier)
        {
            var heroes = await _context.Heroes
                .Where(h => h.Tier == tier)
                .OrderBy(h => h.Name)
                .ToListAsync();

            return Ok(heroes);
        }

        /// <summary>
        /// Поиск героев по названию
        /// </summary>
        [HttpGet("search/{query}")]
        public async Task<ActionResult<IEnumerable<Hero>>> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Введите поисковый запрос");

            var heroes = await _context.Heroes
                .Where(h => h.Name.ToLower().Contains(query.ToLower()))
                .OrderBy(h => h.Name)
                .ToListAsync();

            return Ok(heroes);
        }
    }
}