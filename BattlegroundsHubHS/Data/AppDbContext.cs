using Microsoft.EntityFrameworkCore;
using BattlegroundsHubHS.Models.Entities;

namespace BattlegroundsHubHS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Таблицы базы данных
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Minion> Minions { get; set; }
        public DbSet<Spell> Spells { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<Anomaly> Anomalies { get; set; }
        public DbSet<Accessory> Accessories { get; set; }
        public DbSet<Chronomaly> Chronomalies { get; set; }
        public DbSet<ChronoSpell> ChronoSpells { get; set; }
        public DbSet<Build> Builds { get; set; }
        public DbSet<Tip> Tips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка связи Quest -> Reward (необязательная связь)
            modelBuilder.Entity<Quest>()
                .HasOne(q => q.Reward)
                .WithMany()
                .HasForeignKey(q => q.RewardId)
                .OnDelete(DeleteBehavior.SetNull);

            // Индексы для быстрого поиска
            modelBuilder.Entity<Minion>()
                .HasIndex(m => m.TavernTier);

            modelBuilder.Entity<Minion>()
                .HasIndex(m => m.Type);

            modelBuilder.Entity<Hero>()
                .HasIndex(h => h.Tier);

            modelBuilder.Entity<Spell>()
                .HasIndex(s => s.TavernTier);
        }
    }
}