using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using BattlegroundsHubHS.Data;
using BattlegroundsHubHS.Dtos;
using BattlegroundsHubHS.Models.Entities;
using BattlegroundsHubHS.Models.Enums;

namespace BattlegroundsHubHS.Services
{
    /// Импортер данных из JSON в базу данных
    public class DataImporter
    {
        private readonly AppDbContext _context;
        private readonly ILogger<DataImporter> _logger;

        // Маппинг cardTypeId на тип карты
        private readonly Dictionary<int, string> _cardTypeMapping = new()
        {
            { 3, "Hero" },      // Герой
            { 4, "Minion" },    // Миньон
            { 5, "Spell" },     // Заклинание
            { 40, "Reward" },   // Награда
            { 44, "Trinket" }   // Аксессуар
        };

        // Маппинг minionTypeId на племя
        private readonly Dictionary<int, MinionType> _minionTypeMapping = new()
        {
            { 11, MinionType.Undead },   // Нежить
            { 14, MinionType.Murloc },   // Мурлок
            { 15, MinionType.Demon },    // Демон
            { 17, MinionType.Mech },     // Механизм
            { 18, MinionType.Elemental },// Элементаль
            { 20, MinionType.Beast },    // Зверь
            { 23, MinionType.Pirate },   // Пират
            { 24, MinionType.Dragon },   // Дракон
            { 43, MinionType.Quilboar }, // Свинобраз
            { 92, MinionType.Naga }      // Нага
        };

        public DataImporter(AppDbContext context, ILogger<DataImporter> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// 
        /// Основной метод импорта
        /// 
        public async Task ImportAsync(string jsonFilePath)
        {
            _logger.LogInformation("Начинаем импорт данных из {FilePath}", jsonFilePath);

            // Читаем JSON файл
            var json = await File.ReadAllTextAsync(jsonFilePath);
            var response = JsonSerializer.Deserialize<BattlegroundsApiResponse>(json);

            if (response?.Cards == null)
            {
                _logger.LogError("Не удалось десериализовать JSON");
                return;
            }

            _logger.LogInformation("Найдено {Count} карт", response.Cards.Count);

            // Очищаем таблицы перед импортом (опционально)
            await ClearTablesAsync();

            // Импортируем каждую карту
            foreach (var card in response.Cards)
            {
                await ImportCardAsync(card);
            }

            await _context.SaveChangesAsync();
            _logger.LogInformation("Импорт завершён!");
        }

        /// <summary>
        /// Очистка таблиц перед импортом
        /// </summary>
        private async Task ClearTablesAsync()
        {
            _logger.LogInformation("Очищаем таблицы...");

            _context.Heroes.RemoveRange(_context.Heroes);
            _context.Minions.RemoveRange(_context.Minions);
            _context.Spells.RemoveRange(_context.Spells);
            _context.Quests.RemoveRange(_context.Quests);
            _context.Rewards.RemoveRange(_context.Rewards);
            _context.Anomalies.RemoveRange(_context.Anomalies);
            _context.Accessories.RemoveRange(_context.Accessories);
            _context.Chronomalies.RemoveRange(_context.Chronomalies);
            _context.ChronoSpells.RemoveRange(_context.ChronoSpells);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Импорт отдельной карты
        /// </summary>
        private async Task ImportCardAsync(CardDto card)
        {
            // Определяем тип карты по cardTypeId и battlegrounds-флагам
            if (card.Battlegrounds?.IsHero == true || card.CardTypeId == 3)
            {
                await ImportHeroAsync(card);
            }
            else if (card.Battlegrounds?.IsQuest == true)
            {
                await ImportQuestAsync(card);
            }
            else if (card.Battlegrounds?.IsReward == true || card.CardTypeId == 40)
            {
                await ImportRewardAsync(card);
            }
            else if (card.CardTypeId == 44) // Аксессуар
            {
                await ImportAccessoryAsync(card);
            }
            else if (card.CardTypeId == 5) // Заклинание
            {
                // Проверяем, не хрономалия ли это
                if (card.Battlegrounds?.Tier == 3 || card.Battlegrounds?.Tier == 5)
                {
                    await ImportChronoSpellAsync(card);
                }
                else
                {
                    await ImportSpellAsync(card);
                }
            }
            else if (card.CardTypeId == 4) // Миньон
            {
                // Проверяем, не хрономалия ли это
                if (card.Battlegrounds?.Tier == 3 || card.Battlegrounds?.Tier == 5)
                {
                    await ImportChronomalyAsync(card);
                }
                else
                {
                    await ImportMinionAsync(card);
                }
            }
        }

        /// <summary>
        /// Импорт героя
        /// </summary>
        private async Task ImportHeroAsync(CardDto card)
        {
            var hero = new Hero
            {
                DbfId = card.Id,
                Name = card.Name ?? "",
                ImageUrl = card.Battlegrounds?.ImageUrl ?? card.ImageUrl ?? "",
                HeroPower = "",
                HeroPowerDescription = card.Text ?? "",
                Armor = card.Armor,
                Tier = HeroTier.C  // По умолчанию C, потом можно обновить вручную
            };

            await _context.Heroes.AddAsync(hero);
            _logger.LogDebug("Добавлен герой: {Name}", hero.Name);
        }

        /// <summary>
        /// Импорт миньона
        /// </summary>
        private async Task ImportMinionAsync(CardDto card)
        {
            var minion = new Minion
            {
                DbfId = card.Id,
                Name = card.Name ?? "",
                ImageUrl = card.Battlegrounds?.ImageUrl ?? card.ImageUrl ?? "",
                TavernTier = card.Battlegrounds?.Tier ?? 1,
                Type = GetMinionType(card.MinionTypeId),
                Attack = card.Attack,
                Health = card.Health,
                Effect = card.Text ?? "",
                Rarity = CardRarity.Common
            };

            await _context.Minions.AddAsync(minion);
            _logger.LogDebug("Добавлен миньон {Tier} уровня: {Name}", minion.TavernTier, minion.Name);
        }

        /// <summary>
        /// Импорт заклинания
        /// </summary>
        private async Task ImportSpellAsync(CardDto card)
        {
            var spell = new Spell
            {
                DbfId = card.Id,
                Name = card.Name ?? "",
                ImageUrl = card.Battlegrounds?.ImageUrl ?? card.ImageUrl ?? "",
                TavernTier = card.Battlegrounds?.Tier ?? 1,
                Effect = card.Text ?? "",
                Cost = 1,  // По умолчанию, можно будет уточнить
                Rarity = CardRarity.Common
            };

            await _context.Spells.AddAsync(spell);
            _logger.LogDebug("Добавлено заклинание {Tier} уровня: {Name}", spell.TavernTier, spell.Name);
        }

        /// <summary>
        /// Импорт задания
        /// </summary>
        private async Task ImportQuestAsync(CardDto card)
        {
            var quest = new Quest
            {
                DbfId = card.Id,
                Name = card.Name ?? "",
                ImageUrl = card.Battlegrounds?.ImageUrl ?? card.ImageUrl ?? "",
                Requirement = card.Text ?? "",
                RewardDescription = ""
            };

            await _context.Quests.AddAsync(quest);
            _logger.LogDebug("Добавлено задание: {Name}", quest.Name);
        }

        /// <summary>
        /// Импорт награды
        /// </summary>
        private async Task ImportRewardAsync(CardDto card)
        {
            var reward = new Reward
            {
                DbfId = card.Id,
                Name = card.Name ?? "",
                ImageUrl = card.Battlegrounds?.ImageUrl ?? card.ImageUrl ?? "",
                Effect = card.Text ?? "",
                Rarity = CardRarity.Common
            };

            await _context.Rewards.AddAsync(reward);
            _logger.LogDebug("Добавлена награда: {Name}", reward.Name);
        }

        /// <summary>
        /// Импорт аксессуара
        /// </summary>
        private async Task ImportAccessoryAsync(CardDto card)
        {
            var accessory = new Accessory
            {
                DbfId = card.Id,
                Name = card.Name ?? "",
                ImageUrl = card.Battlegrounds?.ImageUrl ?? card.ImageUrl ?? "",
                Effect = card.Text ?? ""
            };

            await _context.Accessories.AddAsync(accessory);
            _logger.LogDebug("Добавлен аксессуар: {Name}", accessory.Name);
        }

        /// <summary>
        /// Импорт хрономалии (миньон)
        /// </summary>
        private async Task ImportChronomalyAsync(CardDto card)
        {
            var chronomaly = new Chronomaly
            {
                DbfId = card.Id,
                Name = card.Name ?? "",
                ImageUrl = card.Battlegrounds?.ImageUrl ?? card.ImageUrl ?? "",
                TavernTier = card.Battlegrounds?.Tier ?? 3,
                Type = GetMinionType(card.MinionTypeId),
                Effect = card.Text ?? "",
                IsSpell = false,
                Cost = 0,
                Attack = card.Attack,
                Health = card.Health,
                Rarity = CardRarity.Epic
            };

            await _context.Chronomalies.AddAsync(chronomaly);
            _logger.LogDebug("Добавлена хрономалия-миньон {Tier} уровня: {Name}", chronomaly.TavernTier, chronomaly.Name);
        }

        /// <summary>
        /// Импорт хрономального заклинания
        /// </summary>
        private async Task ImportChronoSpellAsync(CardDto card)
        {
            var chronoSpell = new ChronoSpell
            {
                DbfId = card.Id,
                Name = card.Name ?? "",
                ImageUrl = card.Battlegrounds?.ImageUrl ?? card.ImageUrl ?? "",
                TavernTier = card.Battlegrounds?.Tier ?? 3,
                Type = GetMinionType(card.MinionTypeId),
                Effect = card.Text ?? "",
                Cost = 1,
                Rarity = CardRarity.Epic
            };

            await _context.ChronoSpells.AddAsync(chronoSpell);
            _logger.LogDebug("Добавлено хрономальное заклинание {Tier} уровня: {Name}", chronoSpell.TavernTier, chronoSpell.Name);
        }

        /// <summary>
        /// Получение типа миньона по ID из API
        /// </summary>
        private MinionType GetMinionType(int? minionTypeId)
        {
            if (minionTypeId == null || !_minionTypeMapping.ContainsKey(minionTypeId.Value))
                return MinionType.Neutral;

            return _minionTypeMapping[minionTypeId.Value];
        }
    }
}