using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BattlegroundsHubHS.Data;
using BattlegroundsHubHS.Models.Entities;

namespace BattlegroundsHubHS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccessoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AccessoriesController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить все аксессуары
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Accessory>>> GetAll()
        {
            var accessories = await _context.Accessories
                .OrderBy(a => a.Name)
                .ToListAsync();

            return Ok(accessories);
        }

        /// <summary>
        /// Получить аксессуар по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Accessory>> GetById(int id)
        {
            var accessory = await _context.Accessories.FindAsync(id);

            if (accessory == null)
                return NotFound($"Аксессуар с ID {id} не найден");

            return Ok(accessory);
        }
    }
}