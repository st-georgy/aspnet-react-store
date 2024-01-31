using aspnet_react_store.Application.Services;
using aspnet_react_store.DataAccess;
using aspnet_react_store.DataAccess.Entities.Enums;
using aspnet_react_store.DataAccess.Repositories;
using aspnet_react_store.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace aspnet_react_store.Server
{
    public class Startup(IConfiguration configuration)
    {

        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(Configuration.GetConnectionString(nameof(StoreDbContext)));
            dataSourceBuilder.MapEnum<AccountTypeEnum>();
            dataSourceBuilder.MapEnum<OrderStatusEnum>();
            var dataSource = dataSourceBuilder.Build();

            services.AddDbContext<StoreDbContext>(options => options.UseNpgsql(dataSource));

            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
        }

        public void Configure(IApplicationBuilder app, StoreDbContext dbContext)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }
    }
}
