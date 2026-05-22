using MiApp.Infrastructure;
using MiApp.Application;
using MiApp.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Agregar controllers
builder.Services.AddControllers();

// Agregar Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar Application Layer (FluentValidation, MediatR, Behaviors)
builder.Services.AddApplicationServices();

// Registrar Infrastructure (DbContext, etc.)
builder.Services.AddInfrastructure(builder.Configuration);

// Registrar Exception Handler Global y ProblemDetails
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Configurar CORS para permitir conexión desde el frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Crear la base de datos automáticamente al iniciar (solo para desarrollo)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MiApp.Infrastructure.Persistence.Contexts.ApplicationDbContext>();
    db.Database.EnsureCreated();
}

// Configurar Exception Handler Global en el pipeline
app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.MapControllers();

app.Run();
