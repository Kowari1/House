using House.Data.Repositories;
using House.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Windows;
using House.ViewModels;
using House.Converters;

namespace House
{
    /// <summary>
    /// Главный класс приложения WPF.
    /// Отвечает за конфигурацию сервисов и запуск приложения.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Конфигурация приложения.
        /// </summary>
        public static IConfiguration Configuration { get; private set; }

        /// <summary>
        /// Провайдер сервисов.
        /// </summary>
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// Метод, выполняемый при запуске приложения.
        /// </summary>
        /// <param name="e">Аргументы запуска.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ConfigureConfiguration();

            ConfigureServices();
        }

        /// <summary>
        /// Настраивает конфигурацию приложения (чтение appsettings.json).
        /// </summary>
        private void ConfigureConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
        }

        /// <summary>
        /// Настраивает DI-контейнер и регистрирует сервисы.
        /// </summary>
        private void ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddDbContextFactory<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")),
                ServiceLifetime.Transient);

            services.AddScoped<IEquipmentRepository, EquipmentRepository>();
            services.AddTransient<EquipmentPageViewModel>();
            services.AddSingleton<EquipmentStatusConverter>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
