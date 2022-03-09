using Actions.Api.Middlewares;
using Actions.Core.Domain.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shared.Api.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Actions.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AppSettings.Build(Configuration);

            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            ConfigureDefaultServices(services);

            ConfigureEnvironmentServices(services);
        }

        protected void ConfigureDefaultServices(IServiceCollection services)
        {
            // JWT
            var key = Encoding.UTF8.GetBytes(AppSettings.SecretKey);
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
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });

            // Injecao de dependencia
            Configs.DependencyInjection.Config(services);

            FluentValidation.ValidatorOptions.Global.LanguageManager = new Shared.Core.Domain.Impl.Validator.CustomLanguageManager();

            services.AddCors();

            services.AddControllers();

            services.AddApiVersioning(config =>
            {
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.ReportApiVersions = true;
                config.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            services.AddMvc()
                .AddJsonOptions(options => { options.JsonSerializerOptions.IgnoreNullValues = true; });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "API Actions", Version = "v1" });
                var xmlFile = "Actions.Api.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.CustomSchemaIds((type) => type.Name);

                options.CustomSchemaIds(x =>
                {
                    return x.FullName.Replace("Projects.Application.", "").Replace("ViewModels.", "").Replace("ViewModel", "");
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                        }, new List<string>()
                    }
                });
                options.OperationFilter<RemoveVersionParameterFilter>();
                options.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();

                // Permite o uso do atributo ApiExplorerSettings para apresentar o nome do controller com outro nome no Swagger
                options.TagActionsBy(api => new[] { api.GroupName });
                options.DocInclusionPredicate((name, api) => true);
                // Permite agurpar os metodos
                options.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");
                // Permite o uso de Annotations 
                // https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/master/README.md
                options.EnableAnnotations();
            });
        }

        protected virtual void ConfigureEnvironmentServices(IServiceCollection services)
        {
            services.AddDbContext<Actions.Infrastructure.Data.ActionsContext>(options =>
            {
                options.LogTo(Console.WriteLine);
                options.UseSqlServer(Configuration.GetConnectionString("ApiVIContext"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();

            app.UseCors(configure =>
                configure
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .WithExposedHeaders("x-total-count")
                    .WithExposedHeaders("content-disposition")
            );

            var supportedCultures = new[] { new CultureInfo("en-US") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseSwagger();

            app.UseSwaggerUI(s =>
            {
                //https://github.com/swagger-api/swagger-ui/blob/master/docs/usage/configuration.md
                s.SwaggerEndpoint("../swagger/v1/swagger.json", "Api VI v1");
                s.DocExpansion(DocExpansion.None);
                s.DefaultModelsExpandDepth(-1);
                s.InjectStylesheet("/swagger-ui/custom.css");
                s.InjectJavascript("/swagger-ui/custom.js", "text/javascript");

                // Esconde os botoes de submit
                if (AppSettings.HideActionButtons)
                {
                    s.SupportedSubmitMethods();
                };
            });

            app.UseAuthenticationMiddleware();

            app.UseExceptionHandlingMiddleware();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
