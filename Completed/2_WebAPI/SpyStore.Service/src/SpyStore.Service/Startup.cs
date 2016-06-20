using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SpyStore.DAL.EF;
using SpyStore.DAL.EF.Initializers;
using SpyStore.DAL.Repos;
using SpyStore.DAL.Repos.Interfaces;

namespace SpyStore.Service
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
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            // http://docs.asp.net/en/latest/security/cors.html
            //services.AddCors(options => {
            //    options.AddPolicy("AllowAll", builder =>
            //    {
            //        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials();
            //    });
            //});
            services.AddDbContext<SpyStoreContext>(options => options.UseSqlServer(Configuration["Data:ConnectionString"]));
            //https://docs.asp.net/en/latest/fundamentals/dependency-injection.html
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<ICustomerRepo, CustomerRepo>();
            services.AddScoped<IShoppingCartRepo, ShoppingCartRepo>();
            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddScoped<IOrderDetailRepo, OrderDetailRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //app.UseIISPlatformHandler();

            app.UseStaticFiles();

            //app.UseCors("AllowAll");  // has to go before UseMvc for to show Headers in Response as well as preflight.

            app.UseMvc();
            StoreDataInitializer.InitializeData(app.ApplicationServices);
        }
    }
}
