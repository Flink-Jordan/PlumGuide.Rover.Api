namespace PlumGuide.Rover.Application.MovementStrategies
{
    public interface IBoundaryAdjuster
    {
        uint WrapXAround(int targetXPosition);
        uint WrapYAround(int targetYPosition);
    }
}