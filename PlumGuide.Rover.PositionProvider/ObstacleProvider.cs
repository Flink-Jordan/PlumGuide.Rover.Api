using System;
using System.Linq;
using PlumGuide.Rover.Models;

namespace PlumGuide.Rover.Providers
{
    internal class ObstacleProvider : IObstacleProvider
    {
        private readonly IRoverPositionState _roverPositionState;

        public ObstacleProvider(IRoverPositionState roverPositionState)
        {
            _roverPositionState = roverPositionState ?? throw new ArgumentNullException(nameof(roverPositionState));
        }

        public void AddObstacle(Obstacle obstacle)
        {
            if (obstacle == null) throw new ArgumentNullException(nameof(obstacle));
            _roverPositionState.AddObstacle(obstacle);
        }

        public void RemoveObstacle(Obstacle obstacle)
        {
            if (obstacle == null) throw new ArgumentNullException(nameof(obstacle));
            _roverPositionState.RemoveObstacle(obstacle);
        }

        public Obstacle GetObstacle(uint xPosition, uint yPosition)
        {
            return _roverPositionState.GetObstacles()
                .SingleOrDefault(e => 
                    e.YPosition == yPosition && e.XPosition == xPosition);
        }
    }
}
