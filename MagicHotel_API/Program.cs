using MagicHotel_API;
using MagicHotel_API.Datos;
using MagicHotel_API.Repositorio;
using MagicHotel_API.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// AGREGAR: .AddNewtonsoftJson(); para usar el PATCH
builder.Services.AddControllers().AddNewtonsoftJson();

// Conectar la cadena de conexion con DbContext
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Agrego el servicio de AutoMapper para usarlo
builder.Services.AddAutoMapper(typeof(MappingConfig));

// Agrego el servicio para usarlo en el Controlador
builder.Services.AddScoped<IHotelRepositorio, HotelRepositorio>();
builder.Services.AddScoped<INumeroHotelRepositorio, NumeroHotelRepositorio>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
