using EFCoreCommerceDemo.Example2.Infrastructure;
using EFCoreCommerceDemo.Example2.Middlewares;
using EFCoreCommerceDemo.Example2.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EFCoreCommerceDemo.Example2
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContextPool<CommerceDbContext>(builder =>
            {
                if (_env.IsDevelopment())
                    builder.EnableSensitiveDataLogging(true);

                var connStr = this.Configuration.GetConnectionString("db");
                builder.UseSqlServer(connStr);
            });

            services.AddSingleton<ICurrencyConverter, FakeCurrencyConverter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionsMiddleware>(); 

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
