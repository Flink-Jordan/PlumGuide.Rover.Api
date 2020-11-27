using System;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using NUnit.Framework;
using PlumGuide.Rover.Models;
using PlumGuide.Rover.Providers;
using PlumGuide.Rover.Application.MovementStrategies;

namespace PlumGuide.Rover.Application.UnitTests.MovementStrategies
{
    [TestFixture]
    internal class ForwardBackwardMovementStrategyTests
    {
        private Fixture _fixture;
        private Mock<IObstacleProvider> _mockObstacleProvider;
        private Mock<IPositionProvider> _mockPositionProvider;
        private Mock<IBoundaryAdjuster> _mockBoundaryAdjuster;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _movementCommand = MovementCommand.Forward;

            _mockObstacleProvider = new Mock<IObstacleProvider>();
            _mockPositionProvider = new Mock<IPositionProvider>();
            _mockBoundaryAdjuster = new Mock<IBoundaryAdjuster>();

            _mockObstacleProvider
                .Setup(e => 
                    e.GetObstacle(It.IsAny<uint>(), It.IsAny<uint>())).Returns((Obstacle) null);
        }

        private MovementCommand _movementCommand;

        private ForwardBackwardMovementStrategy ForwardBackwardMovementStrategy =>
            new ForwardBackwardMovementStrategy(_movementCommand, _mockObstacleProvider?.Object,
                _mockBoundaryAdjuster?.Object, _mockPositionProvider?.Object);

        [Test]
        public void Ctor_WhenObstacleProviderIsNull_ThrowsArgumentNullException()
        {
            _mockObstacleProvider = null;
            Func<ForwardBackwardMovementStrategy> func = () => ForwardBackwardMovementStrategy;
            func.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("obstacleProvider");
        }

        [Test]
        public void Ctor_WhenBoundaryAdjusterIsNull_ThrowsArgumentNullException()
        {
            _mockBoundaryAdjuster = null;
            Func<ForwardBackwardMovementStrategy> func = () => ForwardBackwardMovementStrategy;
            func.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("boundaryAdjuster");
        }

        [Test]
        public void Ctor_WhenPositionProviderIsNull_ThrowsArgumentNullException()
        {
            _mockPositionProvider = null;
            Func<ForwardBackwardMovementStrategy> func = () => ForwardBackwardMovementStrategy;
            func.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("positionProvider");
        }

        [Test]
        public void Ctor_WhenMovementCommandIsNotForwardOrBackward_ThrowsInvalidOperationException()
        {
            _movementCommand = MovementCommand.Left;
            Func<ForwardBackwardMovementStrategy> func = () => ForwardBackwardMovementStrategy;
            func.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Move_WhenCurrentPositionIsNull_ThrowsArgumentNullException()
        {
            Func<Position> func = () => ForwardBackwardMovementStrategy.Move(null);
            func.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("currentPosition");
        }

        [Test]
        public void Move_WhenCurrentDirectionIsNorth_ReturnsCurrentYPositionPlus1()
        {
            var position = _fixture.Build<Position>()
                .With(p => p.CompassDirection, CompassDirection.North)
                .Create();

            var expectedYPosition = _fixture.Create<uint>();

            _mockBoundaryAdjuster
                .Setup(e => e.WrapYAround((int) position.YPosition + 1))
                .Returns(expectedYPosition);

            var result = ForwardBackwardMovementStrategy.Move(position);

            using (new AssertionScope())
            {
                result.YPosition.Should().Be(expectedYPosition);
                result.XPosition.Should().Be(position.XPosition);
            }
        }

        [Test]
        public void Move_WhenCurrentDirectionIsSouth_ReturnsCurrentYPositionMinus1()
        {
            var position = _fixture.Build<Position>()
                .With(p => p.CompassDirection, CompassDirection.South)
                .Create();

            var expectedYPosition = _fixture.Create<uint>();

            _mockBoundaryAdjuster
                .Setup(e => e.WrapYAround((int)position.YPosition - 1))
                .Returns(expectedYPosition);

            var result = ForwardBackwardMovementStrategy.Move(position);

            using (new AssertionScope())
            {
                result.YPosition.Should().Be(expectedYPosition);
                result.XPosition.Should().Be(position.XPosition);
            }
        }

        [Test]
        public void Move_WhenCurrentDirectionIsEast_ReturnsCurrentXPositionPlus1()
        {
            var position = _fixture.Build<Position>()
                .With(p => p.CompassDirection, CompassDirection.East)
                .Create();

            var expectedXPosition = _fixture.Create<uint>();

            _mockBoundaryAdjuster
                .Setup(e => e.WrapXAround((int)position.XPosition + 1))
                .Returns(expectedXPosition);

            var result = ForwardBackwardMovementStrategy.Move(position);

            using (new AssertionScope())
            {
                result.XPosition.Should().Be(expectedXPosition);
                result.YPosition.Should().Be(position.YPosition);
            }
        }

        [Test]
        public void Move_WhenCurrentDirectionIsWest_ReturnsCurrentXPositionMinus1()
        {
            var position = _fixture.Build<Position>()
                .With(p => p.CompassDirection, CompassDirection.West)
                .Create();

            var expectedXPosition = _fixture.Create<uint>();

            _mockBoundaryAdjuster
                .Setup(e => e.WrapXAround((int)position.XPosition - 1))
                .Returns(expectedXPosition);

            var result = ForwardBackwardMovementStrategy.Move(position);

            using (new AssertionScope())
            {
                result.XPosition.Should().Be(expectedXPosition);
                result.YPosition.Should().Be(position.YPosition);
            }
        }

        [Test]
        public void Move_WhenObstacle_DoesNotUpdatePosition()
        {
            _mockObstacleProvider
                .Setup(e => e.GetObstacle(It.IsAny<uint>(),
                    It.IsAny<uint>()))
                .Returns(_fixture.Create<Obstacle>());

            Func<Position> func = () => ForwardBackwardMovementStrategy.Move(_fixture.Create<Position>());

            using (new AssertionScope())
            {
                func.Should().Throw<ObstacleEncounteredException>();
                _mockPositionProvider.Verify(e => e.UpdatePosition(It.IsAny<Position>()), Times.Never());
            }
        }

        [Test]
        public void Move_WhenNoObstacles_UpdatesPosition()
        {
            _mockObstacleProvider
                .Setup(e => e.GetObstacle(It.IsAny<uint>(),
                    It.IsAny<uint>()))
                .Returns((Obstacle)null);

            ForwardBackwardMovementStrategy.Move(_fixture.Create<Position>());

            _mockPositionProvider.Verify(e => e.UpdatePosition(It.IsAny<Position>()), Times.Once());

        }
    }
}
