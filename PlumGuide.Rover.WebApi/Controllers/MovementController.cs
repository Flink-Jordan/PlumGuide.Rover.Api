using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using PlumGuide.Rover.Models;
using PlumGuide.Rover.Application;
using PlumGuide.Rover.Application.MovementStrategies;

namespace PlumGuide.Rover.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovementController : ControllerBase
    {
        private readonly IMovementHandler _movementHandler;
        private readonly IReadOnlyList<char> _validCommands = new List<char> {'F','B','L','R'};

        public MovementController(IMovementHandler movementHandler)
        {
            _movementHandler = movementHandler ?? throw new ArgumentNullException(nameof(movementHandler));
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public ActionResult<Position> Post(IReadOnlyList<char> commands)
        {
            if (commands.Any(c => !_validCommands.Contains(c)))
            {
                return BadRequest("Please use only F,B,L,R commands");
            }

            try
            {
                return _movementHandler
                    .HandleMovementCommands(commands.Select(c => (MovementCommand)c).ToList());
            }
            catch (ObstacleEncounteredException e)
            {
                return UnprocessableEntity($"An obstacle was encountered. Details: {e.Message}.");
            }
        }
    }
}
