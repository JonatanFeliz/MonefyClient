using Microsoft.Extensions.Configuration;
using MonefyClient.Application.Services.Abstractions;
using MonefyClient.Application.Services.Implementations;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddTransient<IMonefyAccountAppService,MonefyAccountAppService>();
builder.Services.AddTransient<IMonefyExpenseAppService, MonefyExpenseAppService>();
builder.Services.AddTransient<IMonefyIncomeAppService, MonefyIncomeAppService>();
builder.Services.AddAutoMapper(typeof(Program));

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
