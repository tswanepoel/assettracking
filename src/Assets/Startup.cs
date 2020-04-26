using Assets.Entities;
using Assets.Models.Mapping;
using Assets.Models.Validation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Assets
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
            services.AddDbContext<AssetsDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AssetsDb")));
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            
            services.AddOData();

            services.AddControllersWithViews(options =>
                {
                    options.InputFormatters.RemoveType(typeof(ODataInputFormatter));
                    options.OutputFormatters.RemoveType(typeof(ODataOutputFormatter));
                })
                .AddFluentValidation(c => c.RegisterValidatorsFromAssembly(typeof(ComputerValidator).Assembly))
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters { NameClaimType = "sub" };
                    options.Authority = "https://assettracking.eu.auth0.com/";
                    options.Audience = "https://assettracking.azurewebsites.net/api";
                });

            services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Asset Log API", Version = "v1" });
                    options.OperationFilter<ODataQueryOptionsFilter>();

                    options.SchemaGeneratorOptions.CustomTypeMappings.Add(typeof(ODataQueryOptions), () => new OpenApiSchema());
                    options.SchemaGeneratorOptions.CustomTypeMappings.Add(typeof(ODataQueryOptions<>), () => new OpenApiSchema());
                })
                .AddSwaggerGenNewtonsoftSupport();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(c => c.RootPath = "ClientApp/dist");
            
            var mapperConfig = new TypeAdapterConfig();
            mapperConfig.Scan(typeof(TenantRegister).Assembly);

            var mapper = new Mapper(mapperConfig);
            services.AddSingleton<IMapper>(mapper);

            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddTransient<HrefBuilder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseSwagger();
            app.UseReDoc(c => c.SpecUrl("../swagger/v1/swagger.json"));
            
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(c =>
            {
                c.EnableDependencyInjection();
                c.Select().Expand().Filter().OrderBy().MaxTop(null);

                c.MapControllers();
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
