using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleCarApi.Services;

namespace SimpleCarApi.TestHost
{
	public class StartupStub : Startup 
	{
		public StartupStub(IConfiguration configuration) : base (configuration)
		{	 
		}

		public override void ConfigureRepositories(IServiceCollection services)
		{
			services.AddSingleton<ICarService, CarServiceStub>();
		}
		 

	}
}
