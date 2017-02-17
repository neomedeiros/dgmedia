using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using dgmedia.DataAccess.Action;
using dgmedia.DataAccess;
using MongoDB.Driver;
using dgmedia.DataAccess.Connection;

namespace dgmedia
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                //builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mongoDBConnection = new MongoDBConnection()
            {
                MongoClientSettings = GetMongoDBSettings(Configuration),
                DatabaseName = Configuration["AppSettings:MongoDBName"]
            };

            services.AddSingleton<IMongoDBConnection>(mongoDBConnection);
            services.AddSingleton<IActionRepository, ActionRepository>();

            services.AddMvc();
        }

        public MongoClientSettings GetMongoDBSettings(IConfigurationRoot configuration)
        {
            var host = Configuration["AppSettings:MongoDBConnection:Host"];
            var port = int.Parse(Configuration["AppSettings:MongoDBConnection:Port"]);
            var useSsl = bool.Parse(Configuration["AppSettings:MongoDBConnection:UseSsl"]);
            var databaseAuth = Configuration["AppSettings:MongoDBConnection:DatabaseAuth"];
            var username = Configuration["AppSettings:MongoDBConnection:Username"];
            var password = Configuration["AppSettings:MongoDBConnection:Password"];

            var settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(host, port);
            settings.Credentials = new MongoCredential[] { MongoCredential.CreateCredential(databaseAuth, username, password) };
            settings.UseSsl = useSsl;

            return settings;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
