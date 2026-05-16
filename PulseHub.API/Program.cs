// PulseHub.API/Program.cs
using PulseHub.Infrastructure; // ou o namespace correto onde AddInfrastructure está definido

var builder = WebApplication.CreateBuilder(args);

// =============================================
// SERVICES — Injeção de Dependência
// =============================================

// Application Layer
builder.Services.AddApplication();

// Infrastructure Layer (banco, repositórios, etc.)
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
// API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "PulseHub API",
        Version = "v1",
        Description = "Backend da plataforma PulseHub — Marketplace Social em Tempo Real"
    });
});

// =============================================
// PIPELINE
// =============================================

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PulseHub v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();