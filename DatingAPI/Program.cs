using DatingAPI.Data;
using DatingAPI.Extensions;
using DatingAPI.Middleware;
using DatingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = new ConfigurationBuilder()
		.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
		.AddJsonFile("appsettings.json").Build();

builder.Services.AddApplicationServices(configuration);
builder.Services.AddIdentityServices(configuration);
var app = builder.Build();
builder.Services.AddCors();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseDefaultFiles(new DefaultFilesOptions()
{
	DefaultFileNames = new List<string>() { "index.html" }
});
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(builder =>builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
	var context = services.GetRequiredService<DatingAppContext>();
	await context.Database.MigrateAsync();
	await SeedData.SeedUsers(context);
}
catch(Exception ex)
{
	var logger = services.GetService<ILogger<Program>>();
	logger.LogError(ex, "An error occurred during migratiom");
}

app.Run();
