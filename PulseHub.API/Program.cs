// PulseHub.API/Program.cs

using PulseHub.Application;
using PulseHub.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// =============================================
// SERVICES — Injeção de Dependência
// =============================================

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// =============================================
// PIPELINE
// =============================================

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
