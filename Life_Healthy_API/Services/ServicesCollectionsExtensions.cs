using AutoMapper;
using Dapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Life_Healthy_API;
using Life_Healthy_API.Business;
using Life_Healthy_API.Data.Repository;
using Life_Healthy_API.Domain.Models.Request;
using Life_Healthy_API.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SendGrid.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using static Life_Healthy_API.Filters.ValidateModelFilter;

namespace Life.ServicesCollectionsExtensions
{
    public static class ServicesCollectionsExtensions
    {
        public static IServiceCollection DapperService(this IServiceCollection services)
        {
            services.AddScoped<UsuarioRepository>();
            services.AddScoped<WeightRepository>();
            services.AddScoped<FoodRepository>();

            DefaultTypeMap.MatchNamesWithUnderscores = true;

            return services;
        }

        public static IServiceCollection BusinessService(this IServiceCollection services)
        {
            services.AddTransient<UsuarioBL>();
            services.AddTransient<WeightBL>();
            services.AddTransient<FoodBL>();

            return services;
        }

        public static IServiceCollection RepositoryService(this IServiceCollection services)
        {
            services.AddAutoMapper(new Action<IMapperConfigurationExpression>(x => { }), typeof(Startup));
            services.AddAutoMapper(typeof(Startup));

            return services;
        }

        public static IServiceCollection SwaggerService(this IServiceCollection services)
        {
            services.AddMvcCore().AddApiExplorer();
            services.AddResponseCompression();
            #region :: Swagger ::
            services.AddSwaggerGen(conf =>
            {
                conf.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Life Healthy API",
                    Version = "v1",
                    Description = "Life Healthy API"
                });

                // Comentarios Swagger
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                conf.IncludeXmlComments(xmlPath);
            });

            return services;
            #endregion
        }

        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddMvc(options => { options.Filters.Add(typeof(ValidateModelAttibute)); }).AddFluentValidation();

            services.AddScoped<IValidator<UsuarioRequest>, UsuarioValidator>(); // Depois ver quando é chamado o validator
            services.AddScoped<IValidator<UserLoginRequest>, UserLoginValidator>(); // Depois ver quando é chamado o validator

            return services;
        }

        public static IServiceCollection JWTConfiguration(this IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();

            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }

        public static IServiceCollection SendGrid(this IServiceCollection services)
        {
            services.AddSendGrid(options =>
            {
                options.ApiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            });

            return services;
        }

    }
}
