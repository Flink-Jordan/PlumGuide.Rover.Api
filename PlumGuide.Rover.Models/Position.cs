namespace PlumGuide.Rover.Models
{
    public class Position
    {
        public CompassDirection CompassDirection { get; set; }
        public uint XPosition { get; set; }
        public uint YPosition { get; set; }

        public Position(CompassDirection compassDirection, uint xPosition, uint yPosition)
        {
            CompassDirection = compassDirection;
            XPosition = xPosition;
            YPosition = yPosition;
        }
    }
}
