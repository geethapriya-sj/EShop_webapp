using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add Ocelot configuration based on the environment
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("ocelot-dev.json");
}
else
{
    builder.Configuration.AddJsonFile("ocelot.json");
}

// Register Ocelot services
builder.Services.AddOcelot();

var app = builder.Build();

// Use Ocelot middleware
app.UseRouting();
app.UseOcelot().Wait();

app.MapGet("/", () => "Hello Ocelot!");

app.Run();
