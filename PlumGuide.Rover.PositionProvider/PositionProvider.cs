using System;
using PlumGuide.Rover.Models;

namespace PlumGuide.Rover.Providers
{
    internal class PositionProvider : IPositionProvider
    {
        private readonly IRoverPositionState _roverPositionState;

        public PositionProvider(IRoverPositionState roverPositionState)
        {
            _roverPositionState = roverPositionState ?? throw new ArgumentNullException(nameof(roverPositionState));
        }

        public Position GetPosition()
        {
            return _roverPositionState.GetPosition();
        }

        public void UpdatePosition(Position position)
        {
            if (position == null) throw new ArgumentNullException(nameof(position));
            _roverPositionState.SetPosition(position);
        }
    }
}
