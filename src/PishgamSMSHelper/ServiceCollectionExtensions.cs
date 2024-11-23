using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PishgamSMSHelper;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddPishgamSMS(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<PishgamSMSConfig>(configuration.GetSection(PishgamSMSConfig.SectionName));
		services.AddScoped<ISmsService, PishgamSmsService>();
		services.AddHttpClient();
		return services;
	}
}
