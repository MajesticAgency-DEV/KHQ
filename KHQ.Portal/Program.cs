using KHQ.Domain.Entities;
using KHQ.Portal.Service;
using KHQ.Repo.Data;
using KHQ.Repo.Repositories;
using KHQ.Repo.UOW;
using KHQ.Srv.Caching;
using KHQ.Srv.Mapper;
using KHQ.Srv.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Globalization;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<KHQDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Configure password options
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;

    // Disable email confirmation requirement
    options.SignIn.RequireConfirmedEmail = false;
})
.AddEntityFrameworkStores<KHQDBContext>()
.AddDefaultTokenProviders();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
    options.Cookie.HttpOnly = true; // Secure cookie
    options.Cookie.IsEssential = true; // Required for GDPR compliance
});

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
});
builder.Services.AddAutoMapper(typeof(BaseHomeProfile));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IBaseHomeService, BaseHomeSrv>();
builder.Services.AddScoped<IBrandSrv, BrandSrv>();
builder.Services.AddScoped<ICategorySrv, CategorySrv>();
builder.Services.AddScoped<IContactUsSrv, ContactUsSrv>();
builder.Services.AddScoped<IEConInnerService, EConInnerService>();
builder.Services.AddScoped<IFaqService, FaqService>();
builder.Services.AddScoped<IH_AboutUsService, H_AboutUsService>();
builder.Services.AddScoped<ISocialMediaSrv, SocialMediaSrv>();
builder.Services.AddScoped<IProductSrv, ProductSrv>();
builder.Services.AddScoped<ISubProductSrv, SubProductSrv>();
builder.Services.AddScoped<ISliderSrv, SliderSrv>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IAboutUsSrv, AboutUsSrv>();
builder.Services.AddScoped<IStainsService, StainsService>();
builder.Services.AddScoped<IStainDetailsSrv, StainDetailsSrv>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBrouchuresSrv, BrouchuresSrv>();
builder.Services.AddScoped<IEmailsSrv, EmailsSrv>();
builder.Services.AddScoped<IWhyChooseUsSrv, WhyChooseUsSrv>();
builder.Services.AddSingleton<ICacheService, CacheService>();



// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();


var sharedImagesPath = Path.Combine(builder.Environment.ContentRootPath, "..", "SharedImages");

if (!Directory.Exists(sharedImagesPath))
{
    Directory.CreateDirectory(sharedImagesPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(sharedImagesPath),
    RequestPath = "/SharedImages"
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
