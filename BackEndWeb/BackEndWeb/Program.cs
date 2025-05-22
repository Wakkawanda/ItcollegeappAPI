using System.Text;
using BackEndWeb.DataBaseService.DBContext;
using BackEndWeb.DataBaseService.Service;
using BackEndWeb.LDAPService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace BackEndWeb;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Проверка на подгрузку конфигурации
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        
        // Добавляем CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder =>
                {
                    builder.WithOrigins("http://localhost:3001", "http://localhost:3000", "http://fe-3.students.it-college.ru")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });

        builder.Services.AddControllers();
        
        builder.Services.AddEndpointsApiExplorer();
        
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Name", Version = "v1" });
            
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Введите токен в формате 'Bearer {ваш токен}'",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
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
        
        // JWT
        var jwtSettings = builder.Configuration.GetSection("Jwt");
        var secretKey = jwtSettings["SecretKey"];

        if (string.IsNullOrEmpty(secretKey))
            throw new ArgumentException("Secret key cannot be null or empty.", nameof(secretKey));
        
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false; // Только для разработки!
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,          // Проверка издателя токена
                    ValidateAudience = true,        // Проверка получателя токена
                    ValidAudience = jwtSettings["ValidAudience"],
                    ValidIssuer = jwtSettings["ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)), // Ключ для подписи
                    ValidateLifetime = true,          // Проверка времени жизни токена
                    ClockSkew = TimeSpan.Zero
                };
            });

        builder.Services.AddAuthorization(); // Добавление авторизации 
        
        // ORM
        builder.Services.AddDbContext<Itcollegeapp>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
        
        builder.Services.AddScoped<CompanyService>();
        builder.Services.AddScoped<EventService>();
        builder.Services.AddScoped<EventassignmentsService>();
        builder.Services.AddScoped<ReporteventService>();
        builder.Services.AddScoped<ResulteventService>();
        builder.Services.AddScoped<TypeEventService>();
        
        // LDAP
        builder.Services.Configure<LdapSettings>(builder.Configuration.GetSection("LDAP"));
        builder.Services.AddScoped<LdapService>();
        
        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name v1");
            });
        }
        
        // Прослушиваем все адресса. todo Расскомитить при билде !!!!!
        app.Urls.Add("http://0.0.0.0:3000"); // Замените 5288 на ваш порт
        app.Urls.Add("https://0.0.0.0:3001"); // Замените 7190 на ваш HTTPS порт
        
        //app.UseHttpsRedirection();

        // Включаем CORS
        app.UseCors("AllowSpecificOrigin");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}