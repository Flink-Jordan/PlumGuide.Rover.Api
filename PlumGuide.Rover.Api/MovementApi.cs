using System;
using System.Collections.Generic;
using PlumGuid.Rover.Models;
using PlumGuide.Rover.Application;

namespace PlumGuide.Rover.Api
{
    public class MovementApi
    {
        private readonly IMovementHandler _movementHandler;

        public MovementApi(IMovementHandler movementHandler)
        {
            _movementHandler = movementHandler ?? throw new ArgumentNullException(nameof(movementHandler));
        }

        public Position Move(IReadOnlyList<MovementCommand> movementCommands)
        {
            return _movementHandler.HandleMovementCommands(movementCommands);
        }
    }
}
