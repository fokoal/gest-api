using AutoMapper;
using gesteco.api.Configuration;
using gesteco.api.Services;
using gesteco.api.Services.Implementations;
using gesteco.api.src.gesteco.WebApi.Database.Data;
using gesteco.api.src.gesteco.WebApi.Domain.Forms;
using gesteco.api.src.gesteco.WebApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;

namespace gesteco.api {
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
            services.Configure<CorsOptions>(Configuration.GetSection(CorsOptions.Cors));
            services.AddSingleton<CorsOptions>(provider => provider.GetRequiredService<IOptions<CorsOptions>>().Value);

            services.Configure<AzureAdOptions>(Configuration.GetSection(AzureAdOptions.AzureAd));
            services.AddSingleton<AzureAdOptions>(provider => provider.GetRequiredService<IOptions<AzureAdOptions>>().Value);

            services.Configure<SendMailOptions>(Configuration.GetSection(SendMailOptions.MailSend));
            services.AddSingleton<SendMailOptions>(provider => provider.GetRequiredService<IOptions<SendMailOptions>>().Value);

            services.AddDbContext<GestecoContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddHostedService<InitialiazeQuotaService>();


            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
            .AddAzureADBearer(Opt => Configuration.Bind("AzureAd", Opt));

            services.Configure<JwtBearerOptions>(AzureADDefaults.BearerAuthenticationScheme, opt =>
            {
                opt.Authority  +=  "/v2.0";
                opt.Audience = "api://9c5bdd79-a2a1-4c02-91a7-38a31ab07181";
                
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = false
                };
            });
             

            services.AddControllers();
            /// Injection des interface
            services.AddAutoMapper(typeof(Startup));

            /// Permet serialiser les object lazy load charger
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddScoped<ICommonService, CommonService>();

            // configuring swagger
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Title", Version = "v1" });
            });

            services.AddScoped<AzureSqlDatabaseTokenProvider>();
            services.AddApplicationInsightsTelemetry();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustomCors();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
                opt.DefaultModelsExpandDepth(-1); 
            });

            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
