
using Mec_Api_Fundmentals.Core;
using Mec_Api_Fundmentals.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mec_Api_Fundmentals
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			builder.Services.AddDbContext<EcommerceDbContext>
				(options => options.UseSqlServer(
					builder.Configuration.GetConnectionString("DefaultConnection")));
			builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddSwaggerGen();

			var app = builder.Build();
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
