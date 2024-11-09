using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Shop.Models;
using Web_Shop.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30);
	options.Cookie.IsEssential = true;
}); // use session
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

//Connection db
builder.Services.AddDbContext<DataContext>(
	opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AppUseModel, IdentityRole>()
	.AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
	// Password settings.
	options.Password.RequireDigit = true;
	options.Password.RequireLowercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.Password.RequiredLength = 6;

	// User settings.
	options.User.RequireUniqueEmail = true;
});

var app = builder.Build();

app.UseStatusCodePagesWithRedirects("/Home/Error?statuscode={0}");
app.UseSession();

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

app.UseAuthentication(); // Xac thuc truoc
app.UseAuthorization(); // Xac thuc quyen

app.MapControllerRoute(
	name: "areas",
	pattern: "{area:exists}/{controller=Product}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "category",
	pattern: "/category/{Slug?}",
	defaults: new { controller = "Category", action = "Index" });

app.MapControllerRoute(
	name: "brand",
	pattern: "/brand/{Slug?}",
	defaults: new { controller = "Brand", action = "Index" });

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");


//Seeding data 
var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
var env = app.Environment;
SeedData.SeedingData(context, env);

app.Run();