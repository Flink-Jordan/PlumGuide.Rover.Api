using Microsoft.Extensions.DependencyInjection;
using PlumGuide.Rover.Providers;
using PlumGuide.Rover.Application.MovementStrategies;

namespace PlumGuide.Rover.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRoverApplication(this IServiceCollection services)
        {
            services.AddRoverProviders();

            services.AddTransient<IMovementStrategyFactory, MovementStrategyFactory>();
            services.AddTransient<IBoundaryAdjuster, BoundaryAdjuster>();
            services.AddTransient<IMovementHandler, MovementHandler>();
        }
    }
}
