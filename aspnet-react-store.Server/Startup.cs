using Microsoft.EntityFrameworkCore;

namespace aspnet_react_store.Server {
    public class Startup(IConfiguration configuration) {

        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();

            services.AddDbContext<StoreDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("PostgresConnection"))
            );
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
