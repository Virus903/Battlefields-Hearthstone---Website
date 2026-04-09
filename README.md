# 🏆 Battlegrounds Hub

**Battlegrounds Hub** — это веб-приложение-справочник по режиму «Поля сражений» Hearthstone. Примерно как Википедия, только тут будет прям всё по этому режиму (одиночному и дуо).  
Проект предоставляет актуальную информацию о героях, миньонах, заклинаниях, аномалиях, аксессуарах и хрономалиях (то есть полную информацию об этом режиме).  
А также более углубленную информацию как правильно играть в этот режим с полезными советами сборками и комбинациями.

---

### 🎯 Реализовано

- ✅ **Полная база данных** — 800+ карт (герои, миньоны, заклинания, аномалии, аксессуары, хрономалии)
- ✅ **REST API** — 11 эндпоинтов для получения данных
- ✅ **Импорт из официального API Blizzard** — автоматическое наполнение БД
- ✅ **Swagger документация** — интерактивное тестирование API
- ✅ **Фронтенд** — базовая галерея карт с фильтрацией и поиском
- ✅ **SQLite** — легковесная база данных без необходимости установки сервера

### 🔜 В разработке

- 🔲 **Сборки (Builds)** — готовые композиции и стратегии
- 🔲 **Советы (Tips)** — гайды для новичков и опытных игроков
- 🔲 **Комбинации карт** — поиск связок от 2 до 7 карт
- 🔲 **Режимы игры** — гайды для соло и дуэтов
- 🔲 **Полная вики** — стратегии по героям и мета-отчёты

---

## 🛠️ Технологии

| Компонент | Технология |
|-----------|------------|
| **Бэкенд** | ASP.NET Core 8.0 |
| **База данных** | SQLite + Entity Framework Core |
| **API** | REST + Swagger (OpenAPI) |
| **Фронтенд** | HTML5, CSS3, JavaScript (Vanilla) |
| **Импорт данных** | System.Text.Json |
| **Контроль версий** | Git + GitHub |

---

## 📁 Структура проекта

- **BattlegroundsHubHS/**
  - **Controllers/** - API контроллеры
    - HeroesController.cs
    - MinionsController.cs
    - SpellsController.cs
    - QuestsController.cs
    - RewardsController.cs
    - AnomaliesController.cs
    - AccessoriesController.cs
    - ChronomaliesController.cs
    - ChronoSpellsController.cs
    - StatsController.cs
    - ImportController.cs
  - **Data/** - Контекст базы данных
    - AppDbContext.cs
  - **Dtos/** - DTO для импорта из API
    - BattlegroundsApiResponse.cs
    - CardDto.cs
    - BattlegroundsDto.cs
  - **Models/** - Сущности базы данных
    - **Entities/** - Основные модели
      - Hero.cs
      - Minion.cs
      - Spell.cs
      - Quest.cs
      - Reward.cs
      - Anomaly.cs
      - Accessory.cs
      - Chronomaly.cs
      - ChronoSpell.cs
      - Build.cs
      - Tip.cs
    - **Enums/** - Перечисления
      - HeroTier.cs
      - MinionType.cs
      - CardRarity.cs
  - **Services/** - Сервисы
    - DataImporter.cs (импорт из JSON)
  - **wwwroot/** - Статические файлы
    - index.html (фронтенд приложения)
  - Program.cs (точка входа)
  - appsettings.json (конфигурация)
  - battlegrounds.db (база данных SQLite)

---

## 🚀 Установка и запуск

### Требования

- Visual Studio 2022/2026
- .NET 8.0 SDK
- Git

### Инструкция

1. Клонировать репозиторий

   git clone https://github.com/Virus903/Battlegrounds-HS-Hub.git
   cd Battlegrounds-HS-Hub

2. Открыть проект в Visual Studio

   Дважды кликнуть по BattlegroundsHubHS.sln

3. Восстановить пакеты NuGet

   dotnet restore

4. Применить миграции (создать БД)

   Способ 1 — через терминал:
   dotnet ef database update

   Способ 2 — через консоль диспетчера пакетов Visual Studio:
   Update-Database

5. Запустить проект

   Способ 1 — через Visual Studio: нажать F5
   Способ 2 — через терминал: dotnet run

6. Открыть в браузере

   - API документация: https://localhost:7272/swagger
   - Главная страница: https://localhost:7272/index.html
   - Или просто: https://localhost:7272/

   Важно: эти ссылки надо открывать после открытия API документации.

---

## 📡 API эндпоинты

| Метод | Эндпоинт | Описание |
|-------|----------|----------|
| GET | /api/Heroes | Получить всех героев |
| GET | /api/Heroes/{id} | Получить героя по ID |
| GET | /api/Heroes/tier/{tier} | Фильтр героев по рейтингу |
| GET | /api/Heroes/search/{query} | Поиск героев по названию |
| GET | /api/Minions | Получить всех миньонов |
| GET | /api/Minions/tier/{tier} | Фильтр по уровню таверны |
| GET | /api/Minions/type/{type} | Фильтр по типу миньона |
| GET | /api/Minions/filter | Комбинированный фильтр |
| GET | /api/Minions/search/{query} | Поиск миньонов |
| GET | /api/Spells | Получить все заклинания |
| GET | /api/Spells/tier/{tier} | Фильтр заклинаний |
| GET | /api/Quests | Получить все задания |
| GET | /api/Rewards | Получить все награды |
| GET | /api/Anomalies | Получить все аномалии |
| GET | /api/Accessories | Получить все аксессуары |
| GET | /api/Chronomalies | Получить все хрономалии |
| GET | /api/Chronomalies/tier/{tier} | Фильтр хрономалий |
| GET | /api/ChronoSpells | Получить хрон. заклинания |
| GET | /api/Stats | Общая статистика по картам |
| POST | /api/Import/run | Импорт данных из JSON |

---

## 📥 Импорт данных

Данные импортируются из официального API Blizzard:

1. API возвращает JSON со всеми картами Battlegrounds
2. DataImporter парсит JSON и распределяет карты по таблицам
3. Импорт запускается через POST запрос на /api/Import/run

Файл с данными: Data/battlegrounds_cards.json


