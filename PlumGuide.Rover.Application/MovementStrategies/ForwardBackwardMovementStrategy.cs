using System;
using PlumGuide.Rover.Models;
using PlumGuide.Rover.Providers;

namespace PlumGuide.Rover.Application.MovementStrategies
{
    public class ForwardBackwardMovementStrategy : IMovementStrategy
    {
        private readonly IObstacleProvider _obstacleProvider;
        private readonly IPositionProvider _positionProvider;
        private readonly IBoundaryAdjuster _boundaryAdjuster;

        private readonly int _defaultSpacesToMove = 1;

        public ForwardBackwardMovementStrategy(MovementCommand command, IObstacleProvider obstacleProvider,
            IBoundaryAdjuster boundaryAdjuster, IPositionProvider positionProvider)
        {
            _obstacleProvider = obstacleProvider ?? throw new ArgumentNullException(nameof(obstacleProvider));
            _boundaryAdjuster = boundaryAdjuster ?? throw new ArgumentNullException(nameof(boundaryAdjuster));
            _positionProvider = positionProvider ?? throw new ArgumentNullException(nameof(positionProvider));

            _defaultSpacesToMove = command switch
            {
                MovementCommand.Forward => 1,
                MovementCommand.Backward => -1,
                _ => throw new InvalidOperationException(
                    $"Could not set default spaces to move for command type: {command}.")
            };
        }

        public Position Move(Position currentPosition)
        {
            if (currentPosition == null)
                throw new ArgumentNullException(nameof(currentPosition));

            Position targetPosition = currentPosition?.CompassDirection switch
            {
                CompassDirection.North => new Position(currentPosition.CompassDirection, currentPosition.XPosition,
                    _boundaryAdjuster.WrapYAround((int) currentPosition.YPosition + _defaultSpacesToMove)),
                CompassDirection.South => new Position(currentPosition.CompassDirection, currentPosition.XPosition,
                    _boundaryAdjuster.WrapYAround((int) currentPosition.YPosition - _defaultSpacesToMove)),
                CompassDirection.East => new Position(currentPosition.CompassDirection,
                    _boundaryAdjuster.WrapXAround((int) currentPosition.XPosition + _defaultSpacesToMove),
                    currentPosition.YPosition),
                CompassDirection.West => new Position(currentPosition.CompassDirection,
                    _boundaryAdjuster.WrapXAround((int) currentPosition.XPosition - _defaultSpacesToMove),
                    currentPosition.YPosition),
                _ => null
            };

            if (targetPosition == null)
                throw new InvalidOperationException(
                    $"Could not generate a valid target position based on direction {currentPosition.CompassDirection}.");

            var obstacleBlockingMovement =
                _obstacleProvider.GetObstacle(targetPosition.XPosition, targetPosition.YPosition);

            if (obstacleBlockingMovement != null)
                throw new ObstacleEncounteredException(obstacleBlockingMovement);

            _positionProvider.UpdatePosition(targetPosition);
            return targetPosition;
        }
    }
}