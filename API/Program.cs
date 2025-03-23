using System.Text;
using Core.Entities;
using Core.Interfaces;
using Infrustructure.Data;
using Infrustructure.Data.SeedData;
using Infrustructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Frontend origin
              .AllowAnyHeader()                    // Allow all headers
              .AllowAnyMethod()                    // Allow all HTTP methods
              .AllowCredentials();                 // Allow cookies and credentials
    });
});
// Configure Identity with API endpoints
builder.Services.AddIdentityApiEndpoints<AppUser>()
    .AddEntityFrameworkStores<StoreContext>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.SameSite = SameSiteMode.None; // Or SameSiteMode.None if cross-domain
        options.Cookie.SecurePolicy = CookieSecurePolicy.None; // Only over HTTPS
         options.Cookie.HttpOnly = true;
    });

// Configure CORS




builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting(); // Routing middleware must come first.
app.UseCors("AllowSpecificOrigin"); // Enable CORS for the specified origin.
app.UseAuthentication(); // Add authentication middleware before authorization.
app.UseAuthorization(); // Add authorization middleware after authentication.

app.MapControllers(); // Map regular API controllers.
app.MapGroup("api").MapIdentityApi<AppUser>(); // Map Identity API group for AppUser.

// Seed database
try
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StoreContext>();
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    Console.WriteLine(ex);
    throw;
}

app.Run();
