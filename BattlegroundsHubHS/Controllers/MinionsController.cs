using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BattlegroundsHubHS.Data;
using BattlegroundsHubHS.Models.Entities;
using BattlegroundsHubHS.Models.Enums;

namespace BattlegroundsHubHS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MinionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<MinionsController> _logger;

        public MinionsController(AppDbContext context, ILogger<MinionsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Получить всех миньонов
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Minion>>> GetAll()
        {
            var minions = await _context.Minions
                .OrderBy(m => m.TavernTier)
                .ThenBy(m => m.Name)
                .ToListAsync();

            return Ok(minions);
        }

        /// <summary>
        /// Получить миньона по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Minion>> GetById(int id)
        {
            var minion = await _context.Minions.FindAsync(id);

            if (minion == null)
                return NotFound($"Миньон с ID {id} не найден");

            return Ok(minion);
        }

        /// <summary>
        /// Получить миньонов по уровню таверны (1-7)
        /// </summary>
        [HttpGet("tier/{tier}")]
        public async Task<ActionResult<IEnumerable<Minion>>> GetByTier(int tier)
        {
            if (tier < 1 || tier > 7)
                return BadRequest("Уровень таверны должен быть от 1 до 7");

            var minions = await _context.Minions
                .Where(m => m.TavernTier == tier)
                .OrderBy(m => m.Name)
                .ToListAsync();

            return Ok(minions);
        }

        /// <summary>
        /// Получить миньонов по типу/племени
        /// </summary>
        [HttpGet("type/{type}")]
        public async Task<ActionResult<IEnumerable<Minion>>> GetByType(MinionType type)
        {
            var minions = await _context.Minions
                .Where(m => m.Type == type)
                .OrderBy(m => m.TavernTier)
                .ThenBy(m => m.Name)
                .ToListAsync();

            return Ok(minions);
        }

        /// <summary>
        /// Получить миньонов с фильтрацией по уровню и типу
        /// </summary>
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Minion>>> Filter(
            [FromQuery] int? tier,
            [FromQuery] MinionType? type)
        {
            var query = _context.Minions.AsQueryable();

            if (tier.HasValue && tier.Value >= 1 && tier.Value <= 7)
                query = query.Where(m => m.TavernTier == tier.Value);

            if (type.HasValue)
                query = query.Where(m => m.Type == type.Value);

            var minions = await query
                .OrderBy(m => m.TavernTier)
                .ThenBy(m => m.Name)
                .ToListAsync();

            return Ok(minions);
        }

        /// <summary>
        /// Поиск миньонов по названию
        /// </summary>
        [HttpGet("search/{query}")]
        public async Task<ActionResult<IEnumerable<Minion>>> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Введите поисковый запрос");

            var minions = await _context.Minions
                .Where(m => m.Name.ToLower().Contains(query.ToLower()))
                .OrderBy(m => m.TavernTier)
                .ThenBy(m => m.Name)
                .ToListAsync();

            return Ok(minions);
        }
    }
}