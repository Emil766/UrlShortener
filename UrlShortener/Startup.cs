using Autofac;
using AutoMapper;
using ILive.ParkCloud.API.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Repository.Interfaces.Interfaces;
using Repository.MongoDB;
using Repository.MongoDB.Configuration;
using Repository.MongoDB.Implementation;
using Serilog;
using Services.Configuration;
using Services.Implementations;
using Services.Interfaces;
using System.IO;

namespace UrlShortener
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
            services.AddControllers();

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            services.AddSingleton<ILogger>(logger);

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "UrlShortener.API", Version = "v1", });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "UrlShortener.API.xml");
                opt.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "UrlShortener.API V1");
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var mapper = new MapperConfiguration(x => x.AddProfile<MappingProfile.MappingProfile>()).CreateMapper();

            builder.RegisterInstance(mapper).As<IMapper>().SingleInstance();

            var mongoUrl = new MongoUrl(Configuration.GetValue<string>("MongoUrl"));
            var client = new MongoClient(mongoUrl);
            var mongoDatabase = client.GetDatabase(Configuration.GetValue<string>("MongoDBName"));
            builder.RegisterInstance(mongoDatabase).As<IMongoDatabase>().SingleInstance();
            builder.RegisterInstance(client).As<IMongoClient>().SingleInstance();

            var mongoCollections = Configuration.GetSection("MongoCollections").Get<MongoCollections>();
            builder.RegisterInstance(mongoCollections);

            MongoCollectionsInitializer.InitializeCollections(mongoDatabase, mongoCollections);

            var appBaseUrl = Configuration.GetSection("AppBaseUrl").Get<string>();
            var shortenerServiceConfig = new ShortenerServiceConfig { SelfBaseUrl = appBaseUrl };
            builder.RegisterInstance(shortenerServiceConfig);

            builder.RegisterType<ShortUrlRepository>().As<IShortUrlRepository>();

            builder.RegisterType<ShortenerService>().As<IShortenerService>();
        }
    }
}
