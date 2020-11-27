using System;
using PlumGuide.Rover.Models;
using PlumGuide.Rover.Providers;

namespace PlumGuide.Rover.Application.MovementStrategies
{
    internal class MovementStrategyFactory : IMovementStrategyFactory
    {
        private readonly IObstacleProvider _obstacleProvider;
        private readonly IBoundaryAdjuster _boundaryAdjuster;
        private readonly IPositionProvider _positionProvider;

        public MovementStrategyFactory(IObstacleProvider obstacleProvider, IBoundaryAdjuster boundaryAdjuster, IPositionProvider positionProvider)
        {
            _obstacleProvider = obstacleProvider ?? throw new ArgumentNullException(nameof(obstacleProvider));
            _boundaryAdjuster = boundaryAdjuster ?? throw new ArgumentNullException(nameof(boundaryAdjuster));
            _positionProvider = positionProvider ?? throw new ArgumentNullException(nameof(positionProvider));
        }

        public IMovementStrategy GetMovementStrategy(MovementCommand movementCommand)
        {
            switch (movementCommand)
            {
                case MovementCommand.Forward:
                case MovementCommand.Backward:
                    return new ForwardBackwardMovementStrategy(movementCommand, _obstacleProvider, _boundaryAdjuster, _positionProvider);
                case MovementCommand.Left:
                    return new LeftMovementStrategy(_positionProvider);
                case MovementCommand.Right:
                    return new RightMovementStrategy(_positionProvider);
                default:
                    throw new InvalidOperationException($"Movement Command: {movementCommand} is not supported.");
            }
        }
    }
}
