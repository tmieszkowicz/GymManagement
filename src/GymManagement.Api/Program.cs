using GymManagement.Application;
using GymManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    // .AddMediator(ServiceLifetime.Scoped, typeof(GymManagement.Application.DependencyInjection))
    .AddApplication()
    .AddInfrastructure();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
