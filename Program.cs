using API_CRUD_MongoDB.Models;
using API_CRUD_MongoDB.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injeccion de Dependencia de nuestro Servicio
builder.Services.AddControllers(); // uso de controladores
// configuracion de DatabaseSettings
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("ProductDatabase")
);
builder.Services.AddSingleton<ProductService>(); // agregamos nuestro servicio

// Habilitar CORS
builder.Services.AddCors(opciones =>
{
    opciones.AddPolicy("nuevaPolitica", app =>
    {
        app.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("nuevaPolitica"); // CORS

app.UseAuthorization();

app.MapControllers();

app.Run();
