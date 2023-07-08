using Library.Data;
using Library.Data.Infrastructure;
using Library.Data.Repositories;
using Library.Data.Settings;
using Library.Service;
using Library.Web.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
string connString = builder.Configuration
						   .GetConnectionString("LibraryDbConnection")
						   ?? throw new InvalidOperationException("Connection string 'LibraryDbConnection' not found."); ;

// Add services to the container.

builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();
builder.Services.RegisterMaps();

//DbContext
builder.Services.AddDbContext<LibraryContext>(options =>
{
	options.UseSqlServer(connString);
});

//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//                .AddEntityFrameworkStores<LibDBContext>()
//                .AddDefaultTokenProviders();
//builder.Services.Configure<AppSettings>(startup.cstartup.Configurationon.GetSection(nameof(AppSettings)));

//JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTConfig:Key"]))
    };
});



builder.Services.AddSingleton<IAppSettings>(sp => sp.GetRequiredService<IOptions<AppSettings>>().Value);


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<DbContext, LibraryContext>();
builder.Services.AddScoped<IDbFactory, DbFactory>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IStaffReaderRepository, StaffReaderRepository>();
builder.Services.AddScoped<IStaffReaderService, StaffReaderService>();

builder.Services.AddScoped<ILogInfoRepository, LogInfoRepository>();
builder.Services.AddScoped<ILogService, LogService>();

builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<IPositionService, PositionService>();

builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped<IRoleUserRepository, RoleUserRepository>();
builder.Services.AddScoped<IRoleUserService, RoleUserService>();

builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<IPositionService, PositionService>();

builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
builder.Services.AddScoped<IPublisherService, PublisherService>();

builder.Services.AddScoped<ICardRepository, CardRepository>();

builder.Services.AddScoped<IReaderStatusRepository, ReaderStatusRepository>();

builder.Services.AddScoped<ITabRepository, TabRepository>();
builder.Services.AddScoped<ITabService, TabService>();

builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
builder.Services.AddScoped<ILanguageService, LanguageService>();

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IAuthorService, AuthorService>();

builder.Services.AddScoped<IStorageRepository, StorageRepository>();
builder.Services.AddScoped<IStorageService, StorageService>();

builder.Services.AddTransient<IEmailRepository, EmailRepository>();
builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddScoped<ITokenService, TokenService>();


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
