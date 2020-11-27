using System.Collections.Generic;
using PlumGuide.Rover.Models;

namespace PlumGuide.Rover.Application
{
    public interface IMovementHandler
    {
        Position HandleMovementCommands(IReadOnlyList<MovementCommand> movementCommands);
    }
}