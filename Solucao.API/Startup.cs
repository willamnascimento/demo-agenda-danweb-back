using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Solucao.API.Configurations;
using Solucao.API.Services;
using Solucao.Application.Data;
using Solucao.Application.Utils;

namespace Solucao.API
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
            services.AddCors();

            services.AddControllers().AddNewtonsoftJson(options =>
                  options.SerializerSettings.ReferenceLoopHandling =
                  Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Solucao.API", Version = "v1" });
            });

            // .NET Native DI Abstraction
            services.AddDependencyInjectionConfiguration();

            // Auto Mapper
            services.AddAutoMapperConfiguration();

            var key = Encoding.ASCII.GetBytes("c29sdWNhby1sYXNlcg==");
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

            services.AddHttpClient();

            var server = Environment.GetEnvironmentVariable("DbServer");
            var port = Environment.GetEnvironmentVariable("DbPort");
            var user = Environment.GetEnvironmentVariable("DbUser");
            var password = Environment.GetEnvironmentVariable("Password");
            var database = Environment.GetEnvironmentVariable("Database");
            var development = Environment.GetEnvironmentVariable("Development");
            string connectionString = string.Empty;

            if (development == "True")
                connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SolucaoDB;";
            else
                connectionString = $"Server={server}, {port};Initial Catalog={database};User ID={user};Password={password}";

            Console.WriteLine("************" + connectionString + "************");
            services.AddDbContext<SolucaoContext>(options =>
                options.UseSqlServer(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, ILogger<Startup> logger)
        {
            DatabaseManagementService.MigrationInitialisation(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Solucao.API v1"));
            }

            loggerFactory.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
            {
                LogLevel = LogLevel.Information
            }));

            app.UseHttpsRedirection();

            // Make sure you call this before calling app.UseMvc()
            //app.UseCors(
            //    options => options.WithOrigins("http://solucao-laser-dev.s3-website-us-east-1.amazonaws.com").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()
            //);
            app.UseCors(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            


            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == (int)System.Net.HttpStatusCode.Unauthorized)
                {
                    logger.LogWarning($"Unauthorized request - {context.Request.Method} - {context.Request.Path}");
                }
            });

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
