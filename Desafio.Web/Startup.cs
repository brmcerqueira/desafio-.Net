﻿using Desafio.Business;
using Desafio.Persistence;
using LightInject;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Desafio.Web
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private IServiceContainer container;

        public Startup(IHostingEnvironment env)
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DaoContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc().AddControllersAsServices();
        }

        // Use this method to add services directly to LightInject
        // Important: This method must exist in order to replace the default provider.
        public void ConfigureContainer(IServiceContainer container)
        {
            this.container = container;
            container.Register(f => f.GetInstance<IStringLocalizerFactory>().Create("Shared", "Desafio.Web"));
            container.RegisterFrom<BusinessRoot>();
            container.RegisterFrom<PersistenceRoot>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US")
            });

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvcWithDefaultRoute();
            container.AdjustValidationLanguageManager();
        }
    }
}
