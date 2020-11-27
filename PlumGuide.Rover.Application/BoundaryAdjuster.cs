using PlumGuide.Rover.Models;
using PlumGuide.Rover.Application.MovementStrategies;

namespace PlumGuide.Rover.Application
{
    internal class BoundaryAdjuster : IBoundaryAdjuster
    {
        public uint WrapXAround(int targetXPosition)
        {
            if (targetXPosition < PositioningConstants.DefaultXPosition)
                return PositioningConstants.MaximumXPosition;

            if (targetXPosition > PositioningConstants.MaximumXPosition)
                return PositioningConstants.DefaultXPosition;

            return (uint) targetXPosition;
        }

        public uint WrapYAround(int targetYPosition)
        {
            if (targetYPosition < PositioningConstants.DefaultYPosition)
                return PositioningConstants.MaximumYPosition;

            if (targetYPosition > PositioningConstants.MaximumYPosition)
                return PositioningConstants.DefaultYPosition;

            return (uint) targetYPosition;
        }
    }
}
