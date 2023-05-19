using Library.Data;
using Library.Data.Infrastructure;
using Library.Data.Repositories;
using Library.Model.Models;
using Library.Service;
using Library.Web.App_Start;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);
string connString = builder.Configuration
                           .GetConnectionString("LibraryDbConnection");

// Add services to the container.

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//                .AddEntityFrameworkStores<LibDBContext>()
//                .AddDefaultTokenProviders();


builder.Services.AddScoped<IDbFactory, DbFactory>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IStaffReaderRepository, StaffReaderRepository>();
builder.Services.AddScoped<IStaffReaderService, StaffReaderService>();

builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<ILogInfoRepository, LogInfoRepository>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<IReaderStatusRepository, ReaderStatusRepository>();

//DbContext
builder.Services.AddDbContext<LibraryContext>(options =>
{
    options.UseSqlServer(connString);
});


var app = builder.Build();
//Bootstrapper.Run();

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
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
