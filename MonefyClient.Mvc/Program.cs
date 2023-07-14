using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MonefyClient.Application.Services.Abstractions;
using MonefyClient.Application.Services.Implementations;
using MonefyClient.Mvc.Validations;
using MonefyClient.ViewModels.InputViewModels;
using Serilog;
using System.Configuration;
using System.Globalization;

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
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<InputUserViewModel>, UserViewModelValidator>();
builder.Services.AddScoped<IValidator<InputUserLoginViewModel>, UserLoginViewModelValidator>();
builder.Services.AddScoped<IValidator<InputExpenseViewModel>, ExpenseViewModelValidator>();
builder.Services.AddScoped<IValidator<InputIncomeViewModel>, IncomeViewModelValidator>();
builder.Services.AddScoped<IValidator<InputAccountViewModel>, AccountViewModelValidator>();
builder.Services.AddScoped<IValidator<InputExpenseCategoryViewModel>, ExpenseCategoryViewModelValidator>();
builder.Services.AddScoped<IValidator<InputIncomeCategoryViewModel>, IncomeCategoryViewModelValidator>();
builder.Services.AddHttpClient();
builder.Services.AddMvc();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddTransient<IMonefyAccountAppService, ApplicationService>();
//builder.Services.AddTransient<IMonefyExpenseAppService, ApplicationService>();
//builder.Services.AddTransient<IMonefyExpenseCategoryAppService, ApplicationService>();
//builder.Services.AddTransient<IMonefyIncomeCategoryAppService, ApplicationService>();
//builder.Services.AddTransient<IMonefyIncomeAppService, ApplicationService>();
builder.Services.AddTransient<IMonefyAppService, ApplicationService>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(3);
});

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

app.UseRequestLocalization();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();


//TODO (Obligatorio):
//- Falta un endpoint que recoja un income/expense category por id
//- Mirar el tema de la categoria al crear un expense/income, mirar si debe llevar id o no
//- 
//- 