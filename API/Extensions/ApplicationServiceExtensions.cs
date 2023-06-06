using API.Interfaces;
using API.Models;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<DatingDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowOrigin",
                    builder =>
                    {
                        builder.AllowAnyHeader()
                                            .AllowAnyMethod().AllowCredentials().SetIsOriginAllowed((hosts) => true);
                    });
            });
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
