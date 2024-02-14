using MagicHotel_API;
using MagicHotel_API.Datos;
using MagicHotel_API.Repositorio;
using MagicHotel_API.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// AGREGAR: .AddNewtonsoftJson(); para usar el PATCH
builder.Services.AddControllers(options =>
{
	options.CacheProfiles.Add("Default30",
		new CacheProfile()
		{
			Duration = 30
		});
}).AddNewtonsoftJson();



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
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

builder.Services.AddApiVersioning(options =>
{
	options.AssumeDefaultVersionWhenUnspecified = true;
	options.DefaultApiVersion = new ApiVersion(1, 0);
	options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
	options.GroupNameFormat = "'v'VVV";
	options.SubstituteApiVersionInUrl = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


// Swagger : Archivo Añadido
builder.Services.AddSwaggerGen(options => {
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = "Ingresar Bearer [space] tuToken \r\n\r\n " +
					  "Ejemplo: Bearer 123456abcder",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Scheme = "Bearer"
	});
	options.AddSecurityRequirement(new OpenApiSecurityRequirement()
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id= "Bearer"
				},
				Scheme = "oauth2",
				Name="Bearer",
				In = ParameterLocation.Header
			},
			new List<string>()
		}
	});
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Version = "v1",
		Title = "Magic Hotel v1",
		Description = "API para Hoteles"
	});
    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2",
        Title = "Magic Hotel v2",
        Description = "API para Hoteles"
    });
});

builder.Services.AddResponseCaching();

// Archivo Añadido
var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");

builder.Services.AddAuthentication(x =>
{
	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(x => {
		x.RequireHttpsMetadata = false;
		x.SaveToken = true;
		x.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
			ValidateIssuer = false,
			ValidateAudience = false
		};
	});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "Magic_HotelV1");
		options.SwaggerEndpoint("/swagger/v2/swagger.json", "Magic_HotelV2");
	});
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
