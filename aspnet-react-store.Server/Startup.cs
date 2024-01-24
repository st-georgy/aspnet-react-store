using aspnet_react_store.Server.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace aspnet_react_store.Server {
    public class Startup(IConfiguration configuration) {

        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(Configuration.GetConnectionString("PostgresConnection"));
            dataSourceBuilder.MapEnum<AccountTypeEnum>();
            dataSourceBuilder.MapEnum<OrderStatusEnum>();
            var dataSource = dataSourceBuilder.Build();

            services.AddDbContext<StoreDbContext>(options => options.UseNpgsql(dataSource));
        }

        public void Configure(IApplicationBuilder app, StoreDbContext dbContext) {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }
    }
}
