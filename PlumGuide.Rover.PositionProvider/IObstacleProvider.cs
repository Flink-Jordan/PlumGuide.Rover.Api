using PlumGuide.Rover.Models;

namespace PlumGuide.Rover.Providers
{
    public interface IObstacleProvider
    {
        void AddObstacle(Obstacle obstacle);
        void RemoveObstacle(Obstacle obstacle);
        Obstacle GetObstacle(uint xPosition, uint yPosition);
    }
}