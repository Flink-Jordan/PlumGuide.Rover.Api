using PlumGuide.Rover.Models;

namespace PlumGuide.Rover.Providers
{
    public interface IPositionProvider
    {
        void UpdatePosition(Position position);
        Position GetPosition();
    }
}