using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Configure database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Configure Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

// Add authorization services
builder.Services.AddAuthorization();

// Add this after service registration
builder.Services.AddScoped<AdminInitializationService>();

var app = builder.Build();

// Add this right after var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var adminService = services.GetRequiredService<AdminInitializationService>();
    await adminService.InitializeAdminAccount();

    // SEED DATABASE
    var db = services.GetRequiredService<ApplicationDbContext>();
    if (!db.RoomTypes.Any())
    {
        db.RoomTypes.AddRange(
            new WebApplication1.Models.RoomType
            {
                Name = "Deluxe Room",
                ImageUrl = "/images/suite.png",
                PricePerNight = 120,
                BedType = "King",
                MaxOccupancy = 2,
                Size = 35,
                Description = "A spacious deluxe room with a king bed and modern amenities."
            },
            new WebApplication1.Models.RoomType
            {
                Name = "Standard Room",
                ImageUrl = "/images/standard.png",
                PricePerNight = 80,
                BedType = "Queen",
                MaxOccupancy = 2,
                Size = 28,
                Description = "A comfortable standard room perfect for business or leisure."
            }
        );
    }
    if (!db.Services.Any())
    {
        db.Services.AddRange(
            new WebApplication1.Models.Service
            {
                Name = "Spa",
                Category = "Wellness",
                Description = "Relaxing spa treatment.",
                ImageUrl = "/images/massage.png",
                OperatingHours = "09:00 - 21:00",
                Location = "2nd Floor",
                PricePerPerson = 50,
                MaxGuests = 2
            },
            new WebApplication1.Models.Service
            {
                Name = "Buffet Breakfast",
                Category = "Dining",
                Description = "All-you-can-eat breakfast buffet.",
                ImageUrl = "/images/platter.png",
                OperatingHours = "06:00 - 10:00",
                Location = "Ground Floor",
                PricePerPerson = 15,
                MaxGuests = 4
            }
        );
    }
    db.SaveChanges();
}

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add these middleware in this order
app.UseAuthentication();
app.UseAuthorization();

// Add session middleware
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();