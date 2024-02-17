using Microsoft.Extensions.FileProviders;
using aspnet_react_store.Persistence;
using aspnet_react_store.Infrastructure;
using aspnet_react_store.API.Extensions;

namespace aspnet_react_store.Server
{
    public class Startup(IConfiguration configuration)
    {

        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHttpContextAccessor();

            services.Configure<JwtOptions>(Configuration.GetSection(nameof(JwtOptions)));

            services.AddApiProviders();
            services.AddApiDbContext(Configuration);
            services.AddApiAuthentication(Configuration);
            services.AddApiEntityServices();
        }

        public void Configure(IApplicationBuilder app, StoreDbContext dbContext, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always,
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Images")),
                RequestPath = "/Images"
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }
    }
}
