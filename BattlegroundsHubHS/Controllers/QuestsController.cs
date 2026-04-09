using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BattlegroundsHubHS.Data;
using BattlegroundsHubHS.Models.Entities;

namespace BattlegroundsHubHS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuestsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить все задания
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quest>>> GetAll()
        {
            var quests = await _context.Quests
                .Include(q => q.Reward)
                .OrderBy(q => q.Name)
                .ToListAsync();

            return Ok(quests);
        }

        /// <summary>
        /// Получить задание по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Quest>> GetById(int id)
        {
            var quest = await _context.Quests
                .Include(q => q.Reward)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (quest == null)
                return NotFound($"Задание с ID {id} не найдено");

            return Ok(quest);
        }
    }
}