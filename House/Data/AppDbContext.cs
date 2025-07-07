using House.Model;
using Microsoft.EntityFrameworkCore;

namespace House.Data
{
    /// <summary>
    /// Контекст базы данных приложения.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Таблица пользователей в базе данных.
        /// </summary>
        public DbSet<Equipment> Equipments { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр контекста базы данных.
        /// </summary>
        /// <param name="options">Параметры конфигурации контекста.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Конфигурирует модель данных при создании базы данных.
        /// </summary>
        /// <param name="modelBuilder">Используемый для настройки сущностей.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipment>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();
        }
    }
}
