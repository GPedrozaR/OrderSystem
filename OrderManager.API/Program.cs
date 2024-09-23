using OrderManager.Application;
using OrderManager.Infrastructure;

namespace OrderManager.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services
				.AddControllers()
				.AddNewtonsoftJson(options =>
				{
					options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
					options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
				});

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddInfrastructure(builder.Configuration);

			builder.Services.AddApplication();

			var app = builder.Build();

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
