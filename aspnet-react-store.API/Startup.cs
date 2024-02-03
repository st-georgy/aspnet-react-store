using aspnet_react_store.Application.Services;
using aspnet_react_store.Core.Abstractions;
using aspnet_react_store.DataAccess;
using aspnet_react_store.DataAccess.Entities.Enums;
using aspnet_react_store.DataAccess.Repositories;
using aspnet_react_store.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Npgsql;

namespace aspnet_react_store.Server
{
    public class Startup(IConfiguration configuration)
    {

        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHttpContextAccessor();

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(Configuration.GetConnectionString(nameof(StoreDbContext)));
            dataSourceBuilder.MapEnum<AccountTypeEnum>();
            dataSourceBuilder.MapEnum<OrderStatusEnum>();
            var dataSource = dataSourceBuilder.Build();

            services.AddDbContext<StoreDbContext>(options => options.UseNpgsql(dataSource));

            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IImagesService, ImagesService>();

            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IImagesRepository, ImagesRepository>();

            services.AddSingleton<IImageUrlProvider>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext!.Request;
                var baseUrl = Configuration["ASPNETCORE_URLS"]?.Split(';').Select(url => new Uri(url)).FirstOrDefault()?.ToString();

                if (string.IsNullOrWhiteSpace(baseUrl))
                    throw new Exception("Configuration file is incorrect (Check launchsettings.json)");

                return new ImageUrlProvider(baseUrl);
            });
        }

        public void Configure(IApplicationBuilder app, StoreDbContext dbContext, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

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
