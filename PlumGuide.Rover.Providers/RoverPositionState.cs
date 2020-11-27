using System;
using System.Collections.Generic;
using System.Linq;
using PlumGuide.Rover.Models;

namespace PlumGuide.Rover.Providers
{
    internal sealed class RoverPositionState : IRoverPositionState
    {
        private Position _currentPosition = new Position(CompassDirection.North, PositioningConstants.DefaultXPosition,
            PositioningConstants.DefaultYPosition);

        private readonly IList<Obstacle> _obstacles = new List<Obstacle>();

        public void AddObstacle(Obstacle obstacle)
        {
            if (obstacle == null) throw new ArgumentNullException(nameof(obstacle));
            _obstacles.Add(obstacle);
        }

        public IReadOnlyList<Obstacle> GetObstacles()
        {
            return _obstacles.ToList();
        }

        public void RemoveObstacle(Obstacle obstacle)
        {
            if (obstacle == null) throw new ArgumentNullException(nameof(obstacle));
            _obstacles.Remove(obstacle);
        }

        public void SetPosition(Position position)
        {
            _currentPosition = position ?? throw new ArgumentNullException(nameof(position));
        }

        public Position GetPosition()
        {
            return _currentPosition;
        }
    }
}