using Microsoft.EntityFrameworkCore;
using Serilog;
using Vacations_DAL;
using Vacations_DomainModel.Models.Identity;

namespace Vacations
{
    public class Startup
    {
        internal static void AddServices(WebApplicationBuilder builder)
        {
     //       AddSerilog(builder);

            RegisterDAL(builder);

            RegisterIdentity(builder);
        }

        private static void RegisterIdentity(WebApplicationBuilder builder)
        {
            builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<DbContext>();
        }

        public static void RegisterDAL(WebApplicationBuilder builder)
        {
            var services = builder.Services;

            //          var connectionString = builder.Configuration.GetConnectionString("Default");
            var connectionString = "host=localhost;port=5432;database=Vocations;Username=ulia;Password=103125";
           services.AddTransient<DbContextOptions<VacationsDbContext>>(provider =>
            {
                var builder = new DbContextOptionsBuilder<VacationsDbContext>();
                builder.UseNpgsql(connectionString);
                return builder.Options;
            });

            services.AddScoped<DbContext, VacationsDbContext>();

            services.AddScoped<IUnitOfWork>(prov =>
            {
                var context = prov.GetRequiredService<DbContext>();
                return new UnitOfWork(context);
            });
        }

    /*    internal static void AddSerilog(WebApplicationBuilder builder)
        {
            var serilogConfig = builder.Configuration.GetSection(nameof(SerilogConfig)).Get<SerilogConfig>();
            var logFilePath = Path.Combine(serilogConfig?.LoggingDir ?? "./", "log.txt");

            var loggerConfig = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Month,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}");

            if (builder.Environment.IsDevelopment())
            {
                loggerConfig = loggerConfig.MinimumLevel.Debug();
            }
            else
            {
                loggerConfig = loggerConfig.MinimumLevel.Warning();
            }

            var logger = loggerConfig.CreateLogger();

            builder.Services.AddSingleton<ILogger>(logger);
        }*/
    }
}
