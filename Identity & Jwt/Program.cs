using Identity___Jwt.context;
using Identity___Jwt.IdentitModel;
using Identity___Jwt.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser,ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<DefaultUser>();
builder.Services.AddScoped<DefaultRoles>();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    // 4. Resolve the DefaultUser service
    var seeder = scope.ServiceProvider.GetRequiredService<DefaultUser>();
    var seede = scope.ServiceProvider.GetRequiredService<DefaultRoles>();


    // 5. Call the seeding method to create the default user
    await seeder.seedDefaultUserAsync();
    await seede.SeedDefaultRolesAsync();

}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
app.MapControllers();

app.Run();
