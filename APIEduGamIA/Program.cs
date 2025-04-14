using Domain.Interfaces;
using Infrastructure.CrossCutting.Redis;
using Infrastructure.Data.Context;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.Repositories.Interfaces;
using Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Service;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:8080");

var ambiente = builder.Environment.IsDevelopment() ? "Developer" : "Production";
var connectionStrings = builder.Configuration.GetSection("ConnectionStrings").GetSection(ambiente);


var defaultconnectionString = connectionStrings["DefaultConnection"];
var redisConnectionString = connectionStrings["Redis"];

////Configurar Banco de dados/ Redis
builder.Services.AddSingleton<IDatabaseConnectionFactory>(sp => new DatabaseConnectionFactory(defaultconnectionString));
builder.Services.AddSingleton<IRedisService>(new RedisService(redisConnectionString));

//// Configurar os serviços
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<ICourseService, CourseService>();

////Configurar os repositoris
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<ICoursesRepository,CoursesRepository>();



builder.Logging.ClearProviders();
builder.Logging.AddConsole();



// Configurar Autenticação JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "yourissuer",
            ValidAudience = "youraudience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("yoursecretkey"))
        };
    });

// Configurar Swagger e ativar anotações
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations(); // Habilita suporte para anotações de Swagger
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Education Gaming IA",
        Version = "v1",
        Description = @"
Somente nos podemos te propocionar uma aprendizado mais rapido
                       "
    });

    // Adicionar suporte para autenticação JWT no Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Copie 'token'",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();
//app.UseWebSockets();

app.UseStaticFiles();


//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sandbox"));
//}

app.UseAuthentication(); // Habilitar autenticação
app.UseAuthorization();  // Habilitar autorização

app.MapControllers();

app.Run();
