using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BattlegroundsHubHS.Data;
using BattlegroundsHubHS.Models.Entities;

namespace BattlegroundsHubHS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnomaliesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnomaliesController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить все аномалии
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Anomaly>>> GetAll()
        {
            var anomalies = await _context.Anomalies
                .OrderBy(a => a.Name)
                .ToListAsync();

            return Ok(anomalies);
        }

        /// <summary>
        /// Получить аномалию по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Anomaly>> GetById(int id)
        {
            var anomaly = await _context.Anomalies.FindAsync(id);

            if (anomaly == null)
                return NotFound($"Аномалия с ID {id} не найдена");

            return Ok(anomaly);
        }
    }
}