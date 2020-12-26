using DotnetCoreRESTAPI.Repositories;
using DotnetCoreRESTAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using DotnetCoreRESTAPI.Settings;
using System;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Text.Json;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;

namespace DotnetcoreRESTAPI
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
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            var setting = Configuration.GetSection("MongoDBSettings").Get<MongoDBSetting>();
            services.AddSingleton<IMongoClient>(ServiceProvider => new MongoClient(setting.ConnectionString));
            services.AddSingleton<IRepository, MongoDBRepository>();

            // services.AddSingleton<IRepository, InMemoryRepository>();

            services.AddControllers(option =>
            {
                option.SuppressAsyncSuffixInActionNames = false;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DotnetcoreRESTAPI", Version = "v1" });
            });

            services.AddHealthChecks().AddMongoDb(
                setting.ConnectionString,
                name: "mongodb",
                timeout: TimeSpan.FromSeconds(2),
                tags: new[] { "running" }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DotnetcoreRESTAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("health/living", new HealthCheckOptions { Predicate = (_) => false });
                endpoints.MapHealthChecks("/health/running", new HealthCheckOptions
                {
                    Predicate = (check) => check.Tags.Contains("running"),
                    ResponseWriter = async (context, report) =>
                    {
                        var result = JsonSerializer.Serialize(
                            report.Entries.Select(
                                entity =>
                                new
                                {
                                    status = entity.Value.Status.ToString(),
                                    code = context.Response.StatusCode,
                                    name = entity.Key,
                                    exception = entity.Value.Exception == null ? "none" : entity.Value.Exception.Message,
                                    duration = entity.Value.Duration.ToString()
                                }
                            )
                        );
                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        await context.Response.WriteAsync(result);
                    }
                });
            });
        }
    }
}
