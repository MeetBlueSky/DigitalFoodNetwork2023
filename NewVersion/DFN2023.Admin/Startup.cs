using AutoMapper;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using DFN2023.Admin.Helpers;
using DFN2023.Business;
using DFN2023.Contracts;
using DFN2023.Entities.Mappings;
using DFN2023.Infrastructure.Context;
using DFN2023.Infrastructure.Repositories;
using DFN2023.Infrastructure.UnitOfWork;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DFN2023.Admin
{
    public class Startup
    {
        public string DefaultDb { get; set; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            DefaultDb = Configuration["AppSettings:DbType"];
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            ConfigureProductionServices(services);

        }
        public void ConfigureTestingServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(c => c.UseSqlServer(Configuration.GetConnectionString("DevConnection")));

            ConfigureServices(services);
        }
        public void ConfigureProductionServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(c =>
            {
                try
                {
                    c.UseSqlServer(Configuration.GetConnectionString("DevConnection"));
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                }
            });

            ConfigureServices(services);
        }




        public void ConfigureServices(IServiceCollection services)
        {
            if (DefaultDb == EnumProjectDbType.Sql)
            {
                services.AddDbContext<SqlContext>(c => { c.UseSqlServer(Configuration.GetConnectionString("DevConnection"), b => b.MigrationsAssembly("DFN2023.Infrastructure")); });
                UpdateDatabase(services);
            }


            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAdminService, AdminService>();
            IMapper mapper = new MapperConfiguration(mc => { mc.AddProfile(new ModelMapping()); }).CreateMapper();
            services.AddSingleton(mapper);

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSession();


            services.AddLocalization(opts => {
                opts.ResourcesPath = "Resources";
            });

            services.AddMvc().AddRazorRuntimeCompilation()
              .AddNewtonsoftJson(options =>
              {
                  options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                  options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
              });



            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromHours(int.TryParse(Configuration["SessionTimeOut"], out int result) ? result : 24);
            //});

            services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();



            services.Configure<RequestLocalizationOptions>(opts => {
                var supportedCultures = new List<CultureInfo> {
                    new CultureInfo("en"),
                    new CultureInfo("tr"),
                    new CultureInfo("fr"),
                  };

                opts.DefaultRequestCulture = new RequestCulture("tr");
                // Formatting numbers, dates, etc.
                opts.SupportedCultures = supportedCultures;
                // UI strings that we have localized.
                opts.SupportedUICultures = supportedCultures;

                opts.RequestCultureProviders.Insert(0,
                    new RouteDataRequestCultureProvider { Options = opts });
            });
            services.AddControllersWithViews();
        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSession();

            //var suportCulture = new[] { "tr", "en"  };
            //var localizatonOptions = new RequestLocalizationOptions().SetDefaultCulture(suportCulture[0])
            //    .AddSupportedCultures(suportCulture)
            //    .AddSupportedUICultures(suportCulture);

            //app.UseRequestLocalization(localizatonOptions);

            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            //app.UseCookiePolicy();

            /*  app.UseEndpoints(endpoints =>
              {
                  endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

              }); */

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "culture-route", pattern: "{culture}/{controller}/{action}/{id?}", constraints: new { culture = "[a-z]{2}" });
                endpoints.MapControllerRoute(name: "culture-route2", pattern: "{culture}/{controller}/{action}/{ad?}/{id?}", constraints: new { culture = "[a-z]{2}" });
                endpoints.MapControllerRoute(name: "default", "{culture=en}/{controller=Home}/{action=Index}/{id?}", constraints: new { culture = "[a-z]{2}" });
            });



        }




        //Proje çalýþtýrýlýðýnda otomatik migration yapýlmasýný saðlar.
        private void UpdateDatabase(IServiceCollection services)
        {
            var _sp = services.BuildServiceProvider();
            if (DefaultDb == EnumProjectDbType.Sql)
            {
                using (var context = _sp.GetService<SqlContext>())
                {
                    try
                    {
                        var pending = context.Database.GetPendingMigrations();
                        context.Database.Migrate();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
    }
}

