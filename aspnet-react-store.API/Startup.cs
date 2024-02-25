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

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "asp-react-store API",
                    Description = "An ASP.NET CORE Web API for online-store"
                });
            }); 

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

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                    options.DocumentTitle = "Swagger UI";
                });
            }

            // TODO: Change to Migrations
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            dbContext.SeedData().Wait();
        }
    }
}
