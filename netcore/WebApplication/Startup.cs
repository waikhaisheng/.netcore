using Database.Users;
using Database.Users.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Models.Common;
using Services.UserServices;
using Services.UserServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication.Filters;
using WebApplication.Middlewares;

namespace WebApplication
{
    /// <summary>
    /// Creater: System
    /// Created: 20211219
    /// UpdatedBy: Wai Khai Sheng
    /// Updated: 20211227
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        private readonly string _httpClientLocal;
        /// <summary>
        /// Creater: Wai Khai Sheng
        /// Created: 20211227
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        private readonly string _baseAddress;
        /// <summary>
        /// Creater: System
        /// Created: 20211219
        /// UpdatedBy: Wai Khai Sheng
        /// Updated: 20211227
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _httpClientLocal = Configuration["ProjectHttpClient:DemowebapplicationTest:Name"];
            _baseAddress = Configuration["ProjectHttpClient:DemowebapplicationTest:BaseAddress"];
        }
        /// <summary>
        /// Creater: System
        /// Created: 20211219
        /// UpdatedBy: 
        /// Updated: 
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Creater: System
        /// Created: 20211219
        /// UpdatedBy: Wai Khai Sheng
        /// Updated: 20211227
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
            services.AddHttpClient(_httpClientLocal, c =>
            {
                c.BaseAddress = new Uri(_baseAddress);
                c.DefaultRequestHeaders.Add("Accept", "application/json");
                c.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                c.Timeout = TimeSpan.FromSeconds(15);

            }).ConfigurePrimaryHttpMessageHandler(x => new HttpClientHandler() 
            { 
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate 
            }).SetHandlerLifetime(TimeSpan.FromMinutes(10));
            #region JWT
            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            #endregion
            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserDbContext, UserDbContext>(x => new UserDbContext(Configuration.GetConnectionString("DefaultConnection")));
        }

        /// <summary>
        /// Creater: System
        /// Created: 20211219
        /// UpdatedBy: Wai Khai Sheng
        /// Updated: 20211219
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => 
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication v1");
                    c.RoutePrefix = "swagger";
                    c.DefaultModelsExpandDepth(-1);
                });

            }
            app.UseCors(opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseErrorHandlerMiddleware();
            app.UseMiddleware<JwtMiddleware>();//JWT

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
