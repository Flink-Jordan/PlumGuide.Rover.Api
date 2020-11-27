using System;
using PlumGuide.Rover.Models;
using PlumGuide.Rover.Providers;

namespace PlumGuide.Rover.Application.MovementStrategies
{
    internal class RightMovementStrategy : IMovementStrategy
    {
        private readonly IPositionProvider _positionProvider;

        public RightMovementStrategy(IPositionProvider positionProvider)
        {
            _positionProvider = positionProvider ?? throw new ArgumentNullException(nameof(positionProvider));
        }

        public Position Move(Position currentPosition)
        {
            var targetPosition =  currentPosition.CompassDirection switch
            {
                CompassDirection.North => new Position(CompassDirection.East, currentPosition.XPosition,
                    currentPosition.YPosition),
                CompassDirection.East => new Position(CompassDirection.South, currentPosition.XPosition,
                    currentPosition.YPosition),
                CompassDirection.South => new Position(CompassDirection.West, currentPosition.XPosition,
                    currentPosition.YPosition),
                CompassDirection.West => new Position(CompassDirection.North, currentPosition.XPosition,
                    currentPosition.YPosition),
                _ => throw new InvalidOperationException(
                    $"Could not set a target direction for direction {currentPosition.CompassDirection}")
            };

            _positionProvider.UpdatePosition(targetPosition);

            return targetPosition;
        }
    }
}
