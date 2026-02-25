namespace SecHub;

using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System.Globalization;

public partial class Program
{
    static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(theme: SystemConsoleTheme.Colored)
            .MinimumLevel.Verbose()
            .CreateBootstrapLogger();

        var defaultCulture = new CultureInfo("pt-BR");
        defaultCulture.NumberFormat.CurrencyDecimalSeparator = ".";
        defaultCulture.NumberFormat.CurrencyGroupSeparator = ".";
        defaultCulture.NumberFormat.NumberDecimalSeparator = ".";
        defaultCulture.NumberFormat.NumberGroupSeparator = ".";
        defaultCulture.NumberFormat.PercentDecimalSeparator = ".";
        defaultCulture.NumberFormat.PercentGroupSeparator = ".";

        CultureInfo.CurrentCulture = defaultCulture;
        CultureInfo.CurrentUICulture = defaultCulture;
        CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
        CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

        await StartAsync(args);
    }

    static async Task StartAsync(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        {
            builder.Services.AddRazorPages();

            builder.Logging.ClearProviders()
                .AddSerilog();

            builder.Services.AddDbContext<EscolaDbContext>(o =>
            {
                o.UseInMemoryDatabase("Default");
            });

#if DEBUG
            builder.Services.AddHostedService<EscolaDbContextSeeder>();
#endif

        }
        var app = builder.Build();
        {
            app.UseStaticFiles();

            app.MapRazorPages();

            app.MapControllers();
        }
        await app.RunAsync();
    }
}