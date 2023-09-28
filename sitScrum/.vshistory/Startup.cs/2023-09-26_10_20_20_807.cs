using clbBusiness;
using clbBusinessInterface;
using clbDataAccess;
using clbDataAccessInterface;
using IMSAUtil.GUID;
using IMSAUtilInterface.GUID;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using sitScrum.Middleware;
using sitScrum.Repositories;
using sitScrum.util;
using System;

namespace sitScrum
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

            // Para habilitar el uso de variables de session
            services.AddDistributedMemoryCache();
            var vSessionTimeOut = this.Configuration.GetSection("AppSettings:SessionTimeOut").Value;
            vSessionTimeOut ??= "25";

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(double.Parse(vSessionTimeOut));
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // se agrega el razorRuntimeCompilation para los cambios de paginas razor no haya que parar el servidor
            services.AddControllersWithViews(config =>
            {
                config.Filters.Add(typeof(UnhandledErrorMiddleware));
                config.Filters.Add(typeof(SessionValidateFilter));
            })
                   .AddRazorRuntimeCompilation();
            
            services.AddSingleton<IGuidGenerator, GuidGenerator>();
            services.AddTransient<IAuthRepository, AuthRespository>();

            //Proyecto SCRUM
            services.AddTransient<IScrum_TareasBO, Scrum_TareasBO>();
            services.AddTransient<IScrum_TareasRepository, Scrum_TareasRepository>();

            // Injeccion dependencia de httpContext
            services.AddHttpContextAccessor();

            //Habilitar la autenticacion
            var vAuthenticationTimeOut = this.Configuration.GetSection("AppSettings:AuthenticationTimeOut").Value;
            vAuthenticationTimeOut ??= "20";
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(o =>
            {
                o.LoginPath = new PathString("/user/login");
                o.ExpireTimeSpan = TimeSpan.FromMinutes(double.Parse(vAuthenticationTimeOut));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                Console.WriteLine("Modo develop");
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                Console.WriteLine("modo release");
            }

            app.UseStaticFiles();

            app.UseRouting();

            // Habilitar uso de las variables de session
            app.UseSession();

            //Habilitar la autenticacion y autorizacion
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
