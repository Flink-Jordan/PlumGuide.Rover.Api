using Microsoft.Extensions.DependencyInjection;
using PlumGuide.Rover.Application;

namespace PlumGuide.Rover.Api
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRoverApi(this IServiceCollection services)
        {
            services.AddRoverApplication();
        }
    }
}
