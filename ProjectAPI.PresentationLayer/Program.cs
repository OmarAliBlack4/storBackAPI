
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjectAPI.DataAccessLayer.Data.Context;
using ProjectAPI.DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using ProjectAPI.BusinessLogicLayer.Services.Implementations;
using ProjectAPI.BusinessLogicLayer.Services.Interfaces;
using ProjectAPI.DataAccessLayer.Data.Seeders;
using ProjectAPI.DataAccessLayer.UnitOfWorks;
using StackExchange.Redis;
using ProjectAPI.DataAccessLayer.Repositories;
using ProjectAPI.BusinessLogicLayer.MappingProfiles;

namespace ProjectAPI.PresentationLayer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", builder =>
                {
                    builder.WithOrigins("http://localhost:4200",
                                        "https://localhost:4200")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
            });

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<IAuthonticationService, AuthonticationService>();
            builder.Services.AddScoped<IDataSeeder, DataSeeder>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddScoped<IOrderService,OrderService>();
            builder.Services.AddAutoMapper(typeof(BasketMappingProfile));
            builder.Services.AddSingleton<IConnectionMultiplexer>(
                _ => ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")!));
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddIdentity<User, IdentityRole>(option =>
            {
                option.Password.RequireDigit = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequiredLength = 8;
                option.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<StoreContext>()
            .AddDefaultTokenProviders();

            //Add JWT Authentication
            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
                    ValidAudience = builder.Configuration["JwtOptions:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:SecretKey"]))
                };
            });

            //Add Policies
            builder.Services.AddAuthorization(c =>
            {
                c.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                c.AddPolicy("User", policy => policy.RequireRole("User"));
            });

            //Add JWT Authorization to Swagger UI
            #region Add JWT Authorization to Swagger UI
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please Enter Bearer Token ",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                new List<string>(){}
            }
                });
            });
            #endregion

            //Add DB Context Services
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            //seed Data
            using (var scope = app.Services.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
                await seeder.InitializeIdentityAsync();
            }


            // Enable CORS
            app.UseCors("AllowFrontend");


            // Use Satasuc Files wwwroot
            app.UseStaticFiles();

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
        }
    }
}
