using PlumGuide.Rover.Models;

namespace PlumGuide.Rover.WebApi
{
    public class PositionViewModel
    {
        public string Direction { get; set; }
        public uint XPosition { get; set; }
        public uint YPosition { get; set; }

        public PositionViewModel(Position position)
        {
            Direction = position.CompassDirection.ToString();
            XPosition = position.XPosition;
            YPosition = position.YPosition;
        }
    }
}
