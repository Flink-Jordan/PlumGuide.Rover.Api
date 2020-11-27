using Microsoft.Extensions.DependencyInjection;

namespace PlumGuide.Rover.Providers
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRoverProviders(this IServiceCollection services)
        {
            services.AddTransient<IObstacleProvider, ObstacleProvider>();
            services.AddTransient<IPositionProvider, Providers.PositionProvider>();
            services.AddSingleton<IRoverPositionState, RoverPositionState>();
        }
    }
}
