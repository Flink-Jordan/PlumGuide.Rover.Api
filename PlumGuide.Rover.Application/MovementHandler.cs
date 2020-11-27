using System;
using System.Collections.Generic;
using PlumGuide.Rover.Models;
using PlumGuide.Rover.Providers;
using PlumGuide.Rover.Application.MovementStrategies;

namespace PlumGuide.Rover.Application
{
    public class MovementHandler : IMovementHandler
    {
        private readonly IPositionProvider _positionProvider;
        private readonly IMovementStrategyFactory _movementStrategyFactory;

        public MovementHandler(IPositionProvider positionProvider, IMovementStrategyFactory movementStrategyFactory)
        {
            _positionProvider = positionProvider ?? throw new ArgumentNullException(nameof(positionProvider));
            _movementStrategyFactory = movementStrategyFactory ?? throw new ArgumentNullException(nameof(movementStrategyFactory));
        }

        public Position HandleMovementCommands(IReadOnlyList<MovementCommand> movementCommands)
        {
            if (movementCommands == null || movementCommands.Count == 0)
                throw new ArgumentNullException(nameof(movementCommands));

            var currentPosition = _positionProvider.GetPosition();

            if (currentPosition == null)
                throw new InvalidOperationException("Current position was null.");

            Position position = currentPosition;
            foreach (var movementCommand in movementCommands)
            {
                var movementStrategy = _movementStrategyFactory.GetMovementStrategy(movementCommand);
                position = movementStrategy.Move(position); 
            }

            return position;
        }
    }
}
