using System;
using PlumGuide.Rover.Models;

namespace PlumGuide.Rover.Application.MovementStrategies
{
    public class ObstacleEncounteredException : Exception
    {
        public ObstacleEncounteredException(Obstacle obstacle)
        :base($"Obstacle encountered at x: {obstacle.XPosition} y: {obstacle.YPosition} with description: {obstacle.Description}")
        {
        }
    }
}
