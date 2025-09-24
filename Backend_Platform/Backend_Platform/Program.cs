using WebApplication1.Data;
using WebApplication1.Services.Implementation;
using WebApplication1.Services;
using Microsoft.EntityFrameworkCore;
using Backend_Platform.Services;
using Backend_Platform.Services.Implementation;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Remote SQL Database
builder.Services.AddDbContext<DBContext>(options =>
{
    options.UseLazyLoadingProxies();
    options.UseSqlServer(Environment.GetEnvironmentVariable("CUSTOMCONNSTR_reconstructitDatabase"));
});
// Identity Server
var tokenAuthority = "https://dev.backend.isse-identity.com";
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

// Add Services build in this project
builder.Services.AddSingleton<IClaimsService, ClaimsService>();
builder.Services.AddSingleton<IMediaService, MediaService>();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
