using BlazingPizza.Server.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace BlazingPizza.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PizzaStoreContext>(options => 
            {
                //options.UseSqlServer(Configuration.GetConnectionString("PizzaStore"));
                options.UseSqlite(Configuration.GetConnectionString("PizzaStore"));
            });
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddDatabaseDeveloperPageExceptionFilter();     //for can see entityframework errors
            //identityserver4 setup
            services.AddDefaultIdentity<PizzaStoreUser>(options =>
                options.SignIn.RequireConfirmedAccount = true
            ).AddEntityFrameworkStores<PizzaStoreContext>();
            services.AddIdentityServer()
                .AddApiAuthorization<
                    PizzaStoreUser,             //add user setup
                    PizzaStoreContext           //add database management
                >();
            services.AddAuthentication()
                .AddIdentityServerJwt();        //setup authentication scheme for /identity

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();            //notification if we need to add migrations
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            //add identity server
            app.UseIdentityServer();
            //app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
