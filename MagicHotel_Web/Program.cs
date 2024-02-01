using MagicHotel_Web;
using MagicHotel_Web.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Agrego servicios de AutoMapper para usar en Proyecto WEB.
builder.Services.AddAutoMapper(typeof(MappingConfig));

// Agrego servicios para las Interfaces.
builder.Services.AddHttpClient<IHotelService, HotelService>();
builder.Services.AddScoped<IHotelService, HotelService>();

builder.Services.AddHttpClient<INumeroHotelService, NumeroHotelService>();
builder.Services.AddScoped<INumeroHotelService, NumeroHotelService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
