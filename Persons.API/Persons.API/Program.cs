using Persons.API;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();
Log.Information("Starting Person API application");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, logConfig) => logConfig
    .WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration));

var app = builder
    .ConfigureServices()
    .ConfigureApiPipeline();

app.UseSerilogRequestLogging();
app.Run();


public partial class Program { }