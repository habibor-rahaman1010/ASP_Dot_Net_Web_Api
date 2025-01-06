using CountryApi.DataAccessLayer;
using CountryApi.MapsterProfile;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using Serilog.Events;

public class Program()
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Application Bootstrap Logger Configuration, it log information before running application...
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
        string? tableName = "ApplicationLogs";

        //Log write into database
        Log.Logger = new LoggerConfiguration().MinimumLevel
            .Debug().WriteTo.MSSqlServer(
                  connectionString: connection,
                  sinkOptions: new MSSqlServerSinkOptions { TableName = tableName, AutoCreateSqlTable = false })
            .ReadFrom.Configuration(configuration).CreateBootstrapLogger();

        try
        {

            Log.Information("Application Starting...");

            //Log write into database
            var hostBuilder = builder.Host.UseSerilog((ctx, lc) =>
                lc.MinimumLevel.Debug().WriteTo.MSSqlServer(
                    connectionString: connection,
                    sinkOptions: new MSSqlServerSinkOptions { TableName = tableName, AutoCreateSqlTable = false })
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(builder.Configuration)
            );

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Database connectiong here...
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")));

            //Mapster Registration here...
            TypeAdapterConfig.GlobalSettings.Apply(new MappingProfile());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();


            app.Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            Console.WriteLine("Application Crush!");
        }
    }
}
