using DatingAPI.Data;
using DatingAPI.Helpers;
using DatingAPI.Interfaces;
using DatingAPI.Models;
using DatingAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DatingAPI.Extensions
{
	public static class ApplicationServiceExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddDbContext<DatingAppContext>(options
	=> options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
			services.AddScoped<IPhotoService, PhotoService>();
			return services;
		}
	}
}
