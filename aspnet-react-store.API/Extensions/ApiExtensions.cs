using Npgsql;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using aspnet_react_store.Application.Services;
using aspnet_react_store.Domain.Abstractions.Auth;
using aspnet_react_store.Domain.Abstractions.Repositories;
using aspnet_react_store.Domain.Abstractions.Services;
using aspnet_react_store.Infrastructure;
using aspnet_react_store.Persistence;
using aspnet_react_store.Persistence.Entities.Enums;
using aspnet_react_store.Persistence.Mapping;
using aspnet_react_store.Persistence.Repositories;

namespace aspnet_react_store.API.Extensions
{
    public static class ApiExtensions
    {
        public static void AddApiDbContext(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(configuration.GetConnectionString(nameof(StoreDbContext)));
            dataSourceBuilder.MapEnum<UserRoleEnum>();
            dataSourceBuilder.MapEnum<OrderStatusEnum>();
            var dataSource = dataSourceBuilder.Build();

            services.AddDbContext<StoreDbContext>(options => options.UseNpgsql(dataSource));
        }

        public static void AddApiEntityServices(this IServiceCollection services)
        {
            services.AddScoped<ICartsService, CartsService>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IImagesService, ImagesService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IUserInfosService, UserInfosService>();

            services.AddScoped<ICartsRepository, CartsRepository>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IImagesRepository, ImagesRepository>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUserInfosRepository, UserInfosRepository>();

            services.AddAutoMapper(typeof(AutoMapperProfile));
        }

        public static void AddApiProviders(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IPasswordHashProvider, PasswordHashProvider>();

            services.AddSingleton<IImageUrlProvider>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext!.Request;
                var baseUrl = configuration["ASPNETCORE_URLS"]?.Split(';').Select(url => new Uri(url)).FirstOrDefault()?.ToString();

                if (string.IsNullOrWhiteSpace(baseUrl))
                    throw new Exception("Configuration file is incorrect (Check launchsettings.json)");

                return new ImageUrlProvider(baseUrl);
            });
        }

        public static void AddApiAuthentication(
            this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
                    };

                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["tasty-cookies"];
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            var roleClaim = context.Principal?.FindFirst(ClaimTypes.Role);
                            if (roleClaim != null && context.Principal?.Identity is ClaimsIdentity identity)
                                identity.AddClaim(new Claim("role", roleClaim.Value));
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization();
        }
    }
}
