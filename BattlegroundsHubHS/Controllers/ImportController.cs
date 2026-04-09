using Microsoft.AspNetCore.Mvc;
using BattlegroundsHubHS.Services;

namespace BattlegroundsHubHS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImportController : ControllerBase
    {
        private readonly DataImporter _importer;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ImportController> _logger;

        public ImportController(
            DataImporter importer,
            IWebHostEnvironment env,
            ILogger<ImportController> logger)
        {
            _importer = importer;
            _env = env;
            _logger = logger;
        }

        /// <summary>
        /// Запуск импорта данных из JSON файла
        /// </summary>
        [HttpPost("run")]
        public async Task<IActionResult> RunImport()
        {
            try
            {
                var jsonPath = Path.Combine(_env.ContentRootPath, "Data", "battlegrounds_cards.json");

                if (!System.IO.File.Exists(jsonPath))
                {
                    return BadRequest($"Файл не найден: {jsonPath}");
                }

                await _importer.ImportAsync(jsonPath);
                return Ok("Импорт успешно завершён!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при импорте");
                return StatusCode(500, $"Ошибка: {ex.Message}");
            }
        }
    }
}