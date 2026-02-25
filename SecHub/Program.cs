namespace SecHub;

using Microsoft.EntityFrameworkCore;
using SecHub.Models;
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
                o.EnableSensitiveDataLogging();

                o.UseInMemoryDatabase("Default");
            });

#if DEBUG
            builder.Services.AddHostedService<EscolaDbContextSeeder>();
#endif

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            builder.Services.AddControllers();
        }
        var app = builder.Build();
        {
            app.UseStaticFiles();

            app.MapRazorPages();

            app.MapControllers();

            app.UseWelcomePage("/");

            app.UseSwagger();

            app.UseSwaggerUI(x =>
            {
                x.EnableTryItOutByDefault();
                x.EnablePersistAuthorization();
                x.EnableDeepLinking();
            });
        }
        await app.RunAsync();
    }

    #region Random Data Seeder

    class EscolaDbContextSeeder(IServiceProvider services) : BackgroundService
    {
        static readonly string[] s_Nomes = [
            "Ana Paula", "Bruno", "Carlos Henrique", "Daniela", "Eduardo", "Fernanda", "Gabriel", "Heloísa", "Ítalo", "João Vitor",
            "Kátia", "Luiz Felipe", "Maria Eduarda", "Nathan", "Olívia", "Paulo Roberto", "Quiteria", "Rafael", "Sara", "Thiago",
            "Ursula", "Vitor Hugo", "Wagner", "Yara", "Zeca", "André Luiz", "Beatriz", "Caio", "Davi Lucas", "Elisa",
            "Fabrício", "Gisele", "Heitor", "Iara", "Júlio César", "Kevin", "Larissa", "Murilo", "Nicole", "Otávio",
            "Patrícia", "Ruan", "Sabrina", "Túlio", "Vanessa", "Willian", "Yasmin", "Aline", "Bento", "Clarice",
            "Douglas", "Emanuelly", "Felipe", "Giovanna", "Hugo", "Isabela", "Jorge", "Kamila", "Leonardo", "Marcelo",
            "Nair", "Orozimbo", "Priscila", "Renato", "Sônia", "Tatiane", "Uéliton", "Valéria", "Waldir", "Xavier",
            "Yuri", "Zuleica", "Alice", "Bernardo", "Cecília", "Dante", "Esther", "Frederico", "Glória", "Henrique",
            "Igor", "Joana", "Kauan", "Letícia", "Mateus", "Nina", "Olavo", "Pietro", "Raquel", "Samuel",
            "Talita", "Ulisses", "Valentina", "Wilson", "Yago", "Zilda", "Antônio", "Bárbara", "Cláudio", "Débora",
            "Enzo Gabriel", "Flávia", "Guilherme", "Helena", "Isaac", "Janaína", "Kaique", "Lorena", "Marcos", "Nádia",
            "Otto", "Paula", "Ricardo", "Stella", "Tomás", "Ubiratan", "Vera", "Wellington", "Xico", "Yonne",
            "Zélia", "Augusto", "Blanca", "Cristiano", "Dora", "Erick", "Fátima", "Geraldo", "Hilda"
        ];

        static readonly string[] s_Sobrenomes = [
            "Silva", "Santos", "Oliveira", "Souza", "Rodrigues", "Ferreira", "Alves", "Pereira", "Lima", "Gomes",
            "Costa", "Ribeiro", "Martins", "Carvalho", "Almeida", "Lopes", "Soares", "Fernandes", "Vieira", "Barbosa",
            "Rocha", "Dias", "Nascimento", "Cavalcanti", "Mendes", "Castelo Branco", "Espírito Santo", "Corte Real", "Vilas Boas", "Dutra",
            "Moraes", "Pinto", "Pinheiro", "Montenegro", "Figueiredo", "Teixeira", "Guimarães", "Moreira", "Machado", "Borges",
            "Batista", "Amaral", "Cardoso", "Resende", "Viana", "Fonseca", "Melo", "Aragão", "Freitas", "Sampaio",
            "Paiva", "Tavares", "Campos", "Andrade", "Azevedo", "Barros", "Correia", "Cunha", "Lira", "Marques",
            "Medeiros", "Miranda", "Monteiro", "Moura", "Nunes", "Prado", "Queiroz", "Ramalho", "Ramos", "Reis",
            "Sales", "Santana", "Siqueira", "Vasconcelos", "Veloso", "Xavier", "Assis", "Bicalho", "Braga", "Caldas",
            "Chaves", "Dantas", "Drummond", "Farias", "Galvão", "Guerra", "Jardim", "Lacerda", "Leal", "Leite",
            "Lins", "Luz", "Macedo", "Magalhães", "Maia", "Malta", "Matos", "Meireles", "Menezes", "Mesquita",
            "Neto", "Nogueira", "Novaes", "Ornellas", "Pacheco", "Padilha", "Peixoto", "Pessôa", "Porto", "Quadros",
            "Rangel", "Rego", "Rios", "Sá", "Salvador", "Sarmento", "Seixas", "Serra", "Toledo", "Torres",
            "Uchoa", "Valente", "Vargas", "Vaz", "Ventura", "Veras", "Viana", "Zamboni"
        ];

        static readonly string[] s_Estados = { "Rio de Janeiro" };

        static readonly string[] s_Municipios = {
            "Nova Iguaçu",
            "Mesquita",
            "São João de Meriti",
            "Duque de Caxias",
            "Belford Roxo",
            "Nilópolis",
            "Queimados",
            "Japeri",
            "Rio de Janeiro",
            "Magé",
            "Seropédica",
            "Itaguaí",
            "Paracambi",
            "Guapimirim"
        };

        static readonly Random s_Random = new(24022026);

        static string RandomNome()
        {
            int i = s_Random.Next(2, 4);

            var ofs = 0;

            var parts = new string[1 + i];

            parts[ofs++] = s_Nomes[s_Random.Next(0, s_Nomes.Length)];

            for (; ofs < parts.Length; ofs++)
            {
                string value;

                while (true)
                {
                    value = s_Sobrenomes[s_Random.Next(0, s_Sobrenomes.Length)];

                    if (!parts.AsSpan(ofs..).Contains(value))
                    {
                        parts[ofs] = value;
                        break;
                    }
                }
            }

            return string.Join(' ', parts).ToUpper().Trim();
        }

        static readonly char[] s_Digits = [.. "0123456789"];

        static string RandomCpf()
        {
            var text = string.Concat(s_Random.GetItems(s_Digits, 11))
                .Insert(3, ".")
                .Insert(7, ".")
                .Insert(11, "-");

            return text;
        }

        static string RandomTelefone()
        {
            var digits = string.Concat(s_Random.GetItems(s_Digits, 9));
            digits = digits.Insert(digits.Length - 4, "-");
            return "(21) " + digits;
        }

        static Responsavel RandomResponsavel(string grauParentesco)
        {
            return new Responsavel
            {
                Nome = RandomNome(),
                GrauParentesco = grauParentesco,
                Genero = Enums.PessoaGenero.Outro,
                NumCpf = RandomCpf(),
                Telefones = [
                    RandomTelefone(),
                    RandomTelefone(),
                ]
            };
        }

        protected override async Task ExecuteAsync(CancellationToken token)
        {
            await using var scope = services.CreateAsyncScope();

            using var db = scope.ServiceProvider.GetRequiredService<EscolaDbContext>();

            for (int i = 0; i < 45; i++)
            {
                var dataNasc = new DateOnly(
                    s_Random.Next(2010, 2020),
                    s_Random.Next(1, 12),
                    s_Random.Next(1, 28)
                );

                var t1 = RandomResponsavel("RESPONSÁVEL LEGAL");

                var t2 = RandomResponsavel("RESPONSÁVEL LEGAL");

                db.Responsavel.Add(t1);

                db.Responsavel.Add(t2);

                db.Aluno.Add(new Aluno
                {
                    Ativo = true,
                    Nome = RandomNome().ToUpper(),
                    NumCpf = RandomCpf(),
                    DataNascimento = dataNasc,
                    NaturalidadeEstado = s_Estados[0].ToUpper(),
                    NaturalidadeMunicipio = s_Municipios[s_Random.Next(0, s_Municipios.Length)].ToUpper(),
                    IsBrasileiro = true,
                    DataCriado = DateTime.Now,
                    Responsaveis = [
                        t1,
                        t2
                    ]
                });

                await Task.Delay(5, token);
            }


            await db.SaveChangesAsync(token);
        }
    }

    #endregion
}