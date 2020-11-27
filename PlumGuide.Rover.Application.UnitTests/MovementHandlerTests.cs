using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using NUnit.Framework;
using PlumGuide.Rover.Models;
using PlumGuide.Rover.Providers;
using PlumGuide.Rover.Application.MovementStrategies;

namespace PlumGuide.Rover.Application.UnitTests
{
    [TestFixture]
    internal class MovementHandlerTests
    {
        private Fixture _fixture;
        private Mock<IPositionProvider> _mockPositionProvider;
        private Mock<IMovementStrategyFactory> _mockMovementStrategyFactory;

        private MovementHandler MovementHandler =>
            new MovementHandler(_mockPositionProvider?.Object, _mockMovementStrategyFactory?.Object);

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _mockPositionProvider = new Mock<IPositionProvider>();
            _mockMovementStrategyFactory = new Mock<IMovementStrategyFactory>();

            _mockPositionProvider
                .Setup(e => e.GetPosition())
                .Returns(_fixture.Create<Position>());
        }

        [Test]
        public void HandleMovementCommands_WhenMovementCommandsIsNull_ThrowsArgumentNullException()
        {
            Func<Position> func = () => MovementHandler.HandleMovementCommands(null);
            func.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("movementCommands");
        }

        [Test]
        public void HandleMovementCommands_WhenMovementCommandsIsEmpty_ThrowsArgumentNullException()
        {
            Func<Position> func = () => MovementHandler.HandleMovementCommands(Array.Empty<MovementCommand>());
            func.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("movementCommands");
        }

        [Test]
        public void HandleMovementCommands_WhenCurrentPositionIsNull_ThrowsInvalidOperationException()
        {
            _mockPositionProvider.Setup(e => e.GetPosition())
                .Returns((Position) null);

            Func<Position> func = () =>
                MovementHandler.HandleMovementCommands(_fixture.Create<IReadOnlyList<MovementCommand>>());

            func.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void HandleMovementCommands_CallsMoveForEachCommand()
        {
            var commands = _fixture.Create<IReadOnlyList<MovementCommand>>();

            var mockMovementStrategy = new Mock<IMovementStrategy>();
            _mockMovementStrategyFactory.Setup(e => e.GetMovementStrategy(It.IsAny<MovementCommand>()))
                .Returns(mockMovementStrategy.Object);

            MovementHandler.HandleMovementCommands(commands);

            using (new AssertionScope())
            {
                foreach (var movementCommand in commands)
                {
                    _mockMovementStrategyFactory
                        .Verify(e => e.GetMovementStrategy(movementCommand), Times.Once);
                }

                mockMovementStrategy.Verify(e => e.Move(It.IsAny<Position>()), Times.Exactly(commands.Count));
            }
        }
    }
}