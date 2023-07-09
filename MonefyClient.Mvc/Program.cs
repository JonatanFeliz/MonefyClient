using Microsoft.Extensions.Configuration;
using MonefyClient.Application.Services.Abstractions;
using MonefyClient.Application.Services.Implementations;
using Serilog;
using System.Configuration;

using var log = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.FromLogContext()
    .WriteTo.File("Logs/log.txt",
    rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Serilog.ILogger>(log);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddTransient<IMonefyAccountAppService,MonefyAccountAppService>();
builder.Services.AddTransient<IMonefyExpenseAppService, MonefyExpenseAppService>();
builder.Services.AddTransient<IMonefyIncomeAppService, MonefyIncomeAppService>();
builder.Services.AddTransient<IMonefyUserAppService, MonefyUserAppService>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(3);
});
//builder.Services.AddMvc();

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

app.UseSession();

//app.UseMvc();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}");

app.Run();


//TODO:
// - JWT
// - Multidioma
// - Conectar con la API
// - Inyeccion de dependencias (revisar)
// - Mejorar apartado visual
// - Sessions
// - Conectar API de crytos
