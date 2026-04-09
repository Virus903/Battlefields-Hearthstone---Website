using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BattlegroundsHubHS.Data;
using BattlegroundsHubHS.Models.Entities;

namespace BattlegroundsHubHS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RewardsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RewardsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить все награды
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reward>>> GetAll()
        {
            var rewards = await _context.Rewards
                .OrderBy(r => r.Name)
                .ToListAsync();

            return Ok(rewards);
        }

        /// <summary>
        /// Получить награду по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Reward>> GetById(int id)
        {
            var reward = await _context.Rewards.FindAsync(id);

            if (reward == null)
                return NotFound($"Награда с ID {id} не найдена");

            return Ok(reward);
        }
    }
}