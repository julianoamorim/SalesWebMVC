using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SalesWebMVC.Caching;
using SalesWebMVC.Filters;
using SalesWebMVC.Repositories;
using SalesWebMVC.Repositories.Interfaces;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<CacheService>();
builder.Services.AddEndpointsApiExplorer();

// Injeta o Key Vault na configuração
var keyVaultUrl = builder.Configuration["KeyVault:Url"];

builder.Configuration.AddAzureKeyVault(
    new Uri(keyVaultUrl),
    new DefaultAzureCredential()  // usa Managed Identity automaticamente
);

// Registra o Redis usando o secret do Key Vault
var redisConnection = builder.Configuration["redis-connectionstring-full"];
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConnection;
});
if (!string.IsNullOrWhiteSpace(redisConnection))
    Console.WriteLine("✅ Key Vault conectado! Redis:ConnectionString carregado com sucesso.");
else
    Console.WriteLine("❌ Falha: Redis:ConnectionString não encontrado no Key Vault.");

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SalesWeb_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("Chave é inválida ou nula.");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))                        
                    };
                });

builder.Services.AddAuthorization();


//builder.Services.AddControllers();
builder.Services.AddControllers(options => 
{
    options.Filters.Add(new ResponseWrapperFilter());
});
builder.Services.AddScoped<ILivrariaRepository, LivrariaRepository>(); //injecao de dependencia

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

//app.UseAuthentication();
//app.UseAuthorization();
app.UseHttpsRedirection();
app.Run();
