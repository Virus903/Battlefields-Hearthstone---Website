# 🏆 Battlegrounds Hub

**Battlegrounds Hub** — это веб-приложение-справочник по режиму «Поля сражений» Hearthstone. Примерно как Википедия,только тут будет прям всё по этому режиму(одиночному и дуо)
Проект предоставляет актуальную информацию о героях, миньонах, заклинаниях, аномалиях, аксессуарах и хрономалиях(то есть полную информацию об этом режиме).
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

BattlegroundsHubHS/
├── Controllers/ # API контроллеры
│ ├── HeroesController.cs
│ ├── MinionsController.cs
│ ├── SpellsController.cs
│ ├── QuestsController.cs
│ ├── RewardsController.cs
│ ├── AnomaliesController.cs
│ ├── AccessoriesController.cs
│ ├── ChronomaliesController.cs
│ ├── ChronoSpellsController.cs
│ ├── StatsController.cs
│ └── ImportController.cs
│
├── Data/ # Контекст базы данных
│ └── AppDbContext.cs
│
├── Dtos/ # DTO для импорта
│ ├── BattlegroundsApiResponse.cs
│ ├── CardDto.cs
│ └── BattlegroundsDto.cs
│
├── Models/ # Сущности БД
│ ├── Entities/ # Основные модели
│ │ ├── Hero.cs
│ │ ├── Minion.cs
│ │ ├── Spell.cs
│ │ ├── Quest.cs
│ │ ├── Reward.cs
│ │ ├── Anomaly.cs
│ │ ├── Accessory.cs
│ │ ├── Chronomaly.cs
│ │ ├── ChronoSpell.cs
│ │ ├── Build.cs
│ │ └── Tip.cs
│ └── Enums/ # Перечисления
│ ├── HeroTier.cs
│ ├── MinionType.cs
│ └── CardRarity.cs
│
├── Services/ # Сервисы
│ └── DataImporter.cs # Импорт из JSON
│
├── wwwroot/ # Статические файлы
│ └── index.html # Фронтенд приложения
│
├── Program.cs # Точка входа
├── appsettings.json # Конфигурация
└── battlegrounds.db # База данных SQLite

## 🚀 Установка и запуск

### Требования

- [Visual Studio 2022/2026](https://visualstudio.microsoft.com/)
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Git](https://git-scm.com/)

### Инструкция

1. **Клонировать репозиторий**
git clone https://github.com/Virus903/Battlegrounds-HS-Hub.git
cd Battlegrounds-HS-Hub

2. **Открыть проект в Visual Studio**
Дважды кликнуть по BattlegroundsHubHS.sln

3.  **Восстановить пакеты NuGet**
dotnet restore

4. **Применить миграции (создать БД)**
(1)dotnet ef database update

(2)Или в консоли диспетчера пакетов Visual Studio:
powershell
Update-Database

5. **Запустить проект**
(1)Нажать F5 в Visual Studio

(2)Или выполнить в терминале:
dotnet run

6. **Открыть в браузере**

(1) При запуске откроется API документация: https://localhost:7272/swagger

(2) Главная страница: https://localhost:7272/index.html
https://localhost:7272/ - ссылка на сам сайт,чтоб посмотреть html страницу
Эти ссылки надо открывать после открытися API документации.


   
