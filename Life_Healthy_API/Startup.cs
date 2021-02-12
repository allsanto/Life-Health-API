using AutoMapper;
using Dapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Life_Healthy_API.Business;
using Life_Healthy_API.Data.Repository;
using Life_Healthy_API.Domain.Models.Request;
using Life_Healthy_API.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using static Life_Healthy_API.Filters.ValidateModelFilter;

namespace Life_Healthy_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            #region :: Swagger ::
            services.AddSwaggerGen(conf =>
            {
                conf.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Life Healthy API",
                    Version = "v1",
                    Description = "Life Healthy API",
                    Contact = new OpenApiContact
                    {
                        Name = "Signa",
                        Url = new Uri("http://signainfo.com.br")
                    }
                });

                conf.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Autenticação baseada em Json Web Token (JWT)",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });

                // Comentarios Swagger
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //conf.IncludeXmlComments(xmlPath);
            });
            #endregion

            #region :: Dapper ::
            // Toda vez que for instaciado um determinado scopo o ADDSCOPED instacia a classe para o scopo
            services.AddScoped<UsuarioRepository>();
            DefaultTypeMap.MatchNamesWithUnderscores = true; // Solução endeter colunas com caracteres
            #endregion

            #region :: Automapper ::
            services.AddAutoMapper(new Action<IMapperConfigurationExpression>(x => { }), typeof(Startup));

            services.AddAutoMapper(typeof(Startup));
            #endregion

            services.AddMvc();
            services.AddControllers();

            #region :: Business ::
            services.AddTransient<UsuarioBL>();
            #endregion

            #region :: FluentValidation ::
            services.AddMvc(options => { options.Filters.Add(typeof(ValidateModelAttibute)); }).AddFluentValidation();

            services.AddScoped<IValidator<UsuarioRequest>, UsuarioValidator>(); // Depois ver quando é chamado o validator
            #endregion

            #region :: JWT Configuration ::
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
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region :: Jwt Configuration ::
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();
            #endregion

            #region :: Swagger ::
            app.UseSwagger();
            app.UseSwaggerUI(conf =>
            {
                conf.SwaggerEndpoint("/swagger/v1/swagger.json", "Life_Healthy_API");
            });
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
