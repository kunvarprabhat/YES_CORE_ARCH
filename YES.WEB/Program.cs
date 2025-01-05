using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using System;
using YES.Domain.Auth;
using YES.Domain.EntityClasses;
using YES.Extensions;
using YES.Infrastructure;
using YES.Infrastructure.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
builder.Services
               .AddDatabase(builder.Configuration)
               .AddConfigration(builder.Configuration)
               .AddRepositories()
               .AddServices()
               .AddSessionWithOptions();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    //options.SignIn.RequireConfirmedAccount = true;
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
})
  .AddEntityFrameworkStores<AppDbContext>();
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperProfile());
});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddDbContextPool<AppDbContext>(
     options => options.UseSqlServer(builder.Configuration.GetConnectionString("YESConnection")
  ));
FileUploadSettings settings = new();
builder.Configuration.GetSection("FileUploadSettings").Bind(settings);
settings.UploadFolderPath = builder.Environment.WebRootPath + @"\" + settings.UploadFolder;
builder.Services.AddSingleton<FileUploadSettings>(settings);

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "Yesscc";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict; // Adjust as needed
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.MaxAge = TimeSpan.FromMinutes(60);// Set the cookie expiration time

        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied"; // Customize the access denied path
        options.LogoutPath = "/Account/Logout"; // Customize the logout path

        // Configure other options as needed
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
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
