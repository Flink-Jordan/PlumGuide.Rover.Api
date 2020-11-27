using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PlumGuide.Rover.Models;
using PlumGuide.Rover.Application;
using PlumGuide.Rover.Application.MovementStrategies;
using PlumGuide.Rover.WebApi.Controllers;

namespace PlumGuide.Rover.WebApi.UnitTests
{
    [TestFixture]
    public class MovementControllerTests
    {
        private Fixture _fixture;
        private Mock<IMovementHandler> _mockMovementHandler;

        private MovementController MovementController => new MovementController(_mockMovementHandler?.Object);

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mockMovementHandler = new Mock<IMovementHandler>();
        }

        [Test]
        public void Ctor_WhenMovementHandlerIsNull_ThrowsArgumentNullException()
        {
            Func<MovementController> func = () => new MovementController(null);
            func.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("movementHandler");
        }

        [Test]
        public void Post_ReturnsBadRequestWhenInvalidCommand()
        {
            var expectedPosition = new Fixture().Create<Position>();

            _mockMovementHandler
                .Setup(e => e.HandleMovementCommands(It.IsAny<IReadOnlyList<MovementCommand>>()))
                .Returns(expectedPosition);

            var actionResult = MovementController.Post(new Fixture().Create<IReadOnlyList<char>>());

            actionResult.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Test]
        public void Post_WhenObstacleEncountered_ReturnsUnprocessableEntity()
        {
            _mockMovementHandler
                .Setup(e => e.HandleMovementCommands(It.IsAny<IReadOnlyList<MovementCommand>>()))
                .Throws(_fixture.Create<ObstacleEncounteredException>());

            var actionResult = MovementController
                .Post(new Fixture().Create<IReadOnlyList<MovementCommand>>().Select(e => (char)e).ToList());

            actionResult.Result.Should().BeOfType<UnprocessableEntityObjectResult>();
        }

        [Test]
        public void Post_ReturnsPositionFromMovementHandler()
        {
            var expectedPosition = new Fixture().Create<Position>();

            _mockMovementHandler
                .Setup(e => e.HandleMovementCommands(It.IsAny<IReadOnlyList<MovementCommand>>()))
                .Returns(expectedPosition);

            var actionResult = MovementController
                .Post(new Fixture().Create<IReadOnlyList<MovementCommand>>().Select(e => (char)e).ToList());

            actionResult.Value.Should().BeOfType<Position>().Which.Should().BeEquivalentTo(expectedPosition);
        }
    }
}