using System;
using System.Collections.Generic;

namespace PlumGuide.Rover.Api
{
    public class RoverPosition
    {
        public uint XPosition { get; set; }
        public uint YPosition { get; set; }

        public RoverPosition(uint xPosition, uint yPosition)
        {
            XPosition = xPosition;
            YPosition = yPosition;
        }
    }
}