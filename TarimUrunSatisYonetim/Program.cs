using Application.Abraction;
using Application.Concrete;
using Application.Dtos;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Persistence;
using Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddScoped<AppUser>();
builder.Services.AddIdentity<AppUser, AppRoles>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AppDbContext>() // Bu kýsmý doðru DbContext ile deðiþtirin
    .AddDefaultTokenProviders();
builder.Services.AddScoped<DataSeeder>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<ApiResponse>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// DataSeeder'ý çalýþtýrmak için veritabaný baðlantýsýnýn oluþturulmasýnýn ardýndan Seed iþlemini tetikleyin
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    await seeder.SeedAsync();  // await'i kullanýrken doðru baðlamda olunmalý
}

app.Run();
