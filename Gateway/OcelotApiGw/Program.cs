using Ocelot.DependencyInjection;
using Ocelot.Middleware;

string corpspolicy = "clientPolicy";
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile($"Ocelot.{builder.Environment.EnvironmentName}.json", true, true);
builder.Services.AddOcelot();
builder.Services.AddCors(option =>
{
    option.AddPolicy(name: corpspolicy, cbuilder =>
    {
        cbuilder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseCors(corpspolicy);
app.UseOcelot();
app.Run();
