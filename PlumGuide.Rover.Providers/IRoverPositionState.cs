using System.Collections.Generic;
using PlumGuide.Rover.Models;

namespace PlumGuide.Rover.Providers
{
    public interface IRoverPositionState
    {
        public void SetPosition(Position position);
        public Position GetPosition();
        void RemoveObstacle(Obstacle obstacle);
        void AddObstacle(Obstacle obstacle);
        IReadOnlyList<Obstacle> GetObstacles();
    }
}