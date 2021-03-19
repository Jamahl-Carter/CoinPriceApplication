using System;
using System.Linq;
using System.Net;
using System.Threading;
using CoinPrice.Business.Configuration;
using CoinPrice.Common.Configuration;
using CoinPrice.Data.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace CoinPrice.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoinPriceChecker.Api", Version = "v1" });
            });

            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var invalidProp = context.ModelState.First(prop => prop.Value.Errors.Count > 0);

                    return new JsonResult(new
                    {
                        Message = $"{invalidProp.Key}: {invalidProp.Value.Errors.FirstOrDefault()?.ErrorMessage}"
                    });
                };
            });

            // Configure service dependencies
            services.AddBusinessDependencies();
            services.AddCommonDependencies(Configuration);
            services.AddDataDependencies();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            // Configure CORS
            app.UseCors(options =>
            {
                string supportedOrigins = Configuration.GetValue<string>("CORS");

                options.WithOrigins(supportedOrigins).AllowAnyMethod();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "CoinPriceChecker.Api");
                c.RoutePrefix = string.Empty;
            });
            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = context =>
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    return context.Response.WriteAsync(JsonConvert.SerializeObject(new {Error = "An internal error has occured."}));
                }
            });

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
