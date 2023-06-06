using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using API;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}


//using API.Data;
//using Microsoft.EntityFrameworkCore;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//builder. Services.AddDbContext<DataContext>(options =>
//                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddCors(options =>
//            {
//                options.AddPolicy(
//                    name: "AllowOrigin",
//                    builder =>
//                    {
//                        builder.AllowAnyOrigin()
//                                .AllowAnyMethod()
//                                .AllowAnyHeader();
//                    });
//            });

//var app = builder.Build();
//app.UseCors("AllowOrigin");
//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{

//}

//app.MapControllers();

//app.Run();
