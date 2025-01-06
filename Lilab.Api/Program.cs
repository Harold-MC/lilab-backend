using Lilab.Api.Contracts;
using Lilab.Api.Installers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var apiInstallers = typeof(MvcInstaller).Assembly.ExportedTypes.Where(x =>
        typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
    .Select(Activator.CreateInstance)
    .Cast<IInstaller>().ToList();

apiInstallers.ForEach(installer => installer.InstallServices(builder.Services, builder.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();

app.UseCors("Any");

app.UseAuthorization();

app.MapControllers();

app.Run();