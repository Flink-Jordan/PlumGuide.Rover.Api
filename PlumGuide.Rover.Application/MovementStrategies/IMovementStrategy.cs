using PlumGuide.Rover.Models;

namespace PlumGuide.Rover.Application.MovementStrategies
{
    public interface IMovementStrategy
    {
        Position Move(Position currentPosition);
    }
}