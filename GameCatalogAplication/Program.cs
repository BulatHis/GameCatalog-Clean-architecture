using System.Reflection;
using System.Text;
using GameCatalogCore.IServices;
using GameCatalogCore.Services;
using GameCatalogDomain.IRepositories;
using GameCatalogDomain.IServices;
using GameCatalogInteractor;
using GameCatalogInteractor.EmailCode;
using GameCatalogInteractor.EmailCode.SecretKey;
using GameCatalogInteractor.Repositories;
using GameCatalogMsSQL.GenerateJWT;
using GameCatalogMsSQL.Hash;
using GameCatalogMsSQL.IRepositories;
using GameCatalogMsSQL.Repositories;
using GameCatalogMsSQL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);


builder.Logging.ClearProviders(); //не забыть почистить!
builder.Host.UseNLog();
builder.Services.AddLogging();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    swagger.IncludeXmlComments(xmlPath);
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "GameCatalog",
    });
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ввести 'Bearer' + token для авторизации",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddControllers();
builder.Services.AddDbContext<AddDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration["DataBaseConnection:ConnectionString"]);
    opt.UseSqlServer(b => b.MigrationsAssembly("GameCatalogMsSQL"));
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]!)),
            LifetimeValidator = (_, expires, _, _) =>
                expires - DateTime.UtcNow > new TimeSpan(0, 0, 0)
        };
    });

builder.Services.AddCors();

// Регистрация зависимостей
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IAdminReviewServices, AdminReviewServices>();
builder.Services.AddScoped<IGameServices, GameServices>();
builder.Services.AddScoped<IReviewServices, ReviewServices>();
builder.Services.AddScoped<IGenreServices, GenreServices>();
builder.Services.AddScoped<ICodeGenerator, CodeGenerator>();
builder.Services.AddScoped<IMailHandler, MailHandler>();
builder.Services.AddScoped<IGenerateToken, GenerateTokenJwt>();
builder.Services.AddScoped<ICheckSecretKey, CheckSecretKey>();
builder.Services.AddScoped<IHashPasswords, HashPasswords>();
builder.Services.AddHttpContextAccessor();


// Регистрация репозиториев
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IAdminReviewRepository, AdminReviewRepository>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("User", policy => policy.RequireRole("User"));
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
});

builder.Services.AddAuthentication();


// Создание приложения
var app = builder.Build();


app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseCors(builder => builder.WithOrigins("http://localhost:3000").AllowAnyHeader());

app.Run();