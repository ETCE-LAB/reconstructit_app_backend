using WebApplication1.Data;
using WebApplication1.Services.Implementation;
using WebApplication1.Services;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Remote SQL Database
builder.Services.AddDbContext<DBContext>(options =>
{
    options.UseLazyLoadingProxies();
    options.UseSqlServer("Server=tcp:dbserverreconstructit.database.windows.net,1433;Initial Catalog=Backend_Platform_DB;Persist Security Info=False;User ID=db_admin;Password=3a1e677c-fd30-4358-bf0c-7c6f73031c56;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
});

var tokenAuthority = "https://development-isse-identity-backend.azurewebsites.net";
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = tokenAuthority;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = false,
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddCors();
builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IClaimsService, ClaimsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
