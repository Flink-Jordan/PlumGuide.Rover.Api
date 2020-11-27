using PlumGuide.Rover.Models;

namespace PlumGuide.Rover.Application.MovementStrategies
{
    public interface IMovementStrategyFactory
    {
        IMovementStrategy GetMovementStrategy(MovementCommand movementCommand);
    }
}