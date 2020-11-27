using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PlumGuide.Rover.Models;
using PlumGuide.Rover.Providers;
using PlumGuide.Rover.Application.MovementStrategies;

namespace PlumGuide.Rover.Application.UnitTests.MovementStrategies
{
    [TestFixture]
    internal class MovementStrategyFactoryTests
    {
        private Mock<IObstacleProvider> _mockObstacleProvider;
        private Mock<IBoundaryAdjuster> _mockBoundaryAdjuster;
        private Mock<IPositionProvider> _mockPositionProvider;

        private MovementStrategyFactory MovementStrategyFactory =>
            new MovementStrategyFactory(_mockObstacleProvider?.Object, _mockBoundaryAdjuster?.Object,
                _mockPositionProvider?.Object);

        [SetUp]
        public void SetUp()
        {
            _mockObstacleProvider = new Mock<IObstacleProvider>();
            _mockBoundaryAdjuster = new Mock<IBoundaryAdjuster>();
            _mockPositionProvider = new Mock<IPositionProvider>();
        }

        [Test]
        public void Ctor_WhenObstacleProviderIsNull_ThrowsArgumentNullException()
        {
            _mockObstacleProvider = null;
            Func<MovementStrategyFactory> func = () => MovementStrategyFactory;
            func.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("obstacleProvider");
        }

        [Test]
        public void Ctor_WhenBoundaryAdjusterIsNull_ThrowsArgumentNullException()
        {
            _mockBoundaryAdjuster = null;
            Func<MovementStrategyFactory> func = () => MovementStrategyFactory;
            func.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("boundaryAdjuster");
        }

        [Test]
        public void Ctor_WhenPositionProviderIsNull_ThrowsArgumentNullException()
        {
            _mockPositionProvider = null;
            Func<MovementStrategyFactory> func = () => MovementStrategyFactory;
            func.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("positionProvider");
        }

        [Test]
        public void GetMovementStrategy_WhenForward_ReturnsForwardBackwardMovementStrategy()
        {
            var strategy = MovementStrategyFactory.GetMovementStrategy(MovementCommand.Forward);
            strategy.Should().BeOfType<ForwardBackwardMovementStrategy>();
        }

        [Test]
        public void GetMovementStrategy_WhenBackward_ReturnsForwardBackwardMovementStrategy()
        {
            var strategy = MovementStrategyFactory.GetMovementStrategy(MovementCommand.Backward);
            strategy.Should().BeOfType<ForwardBackwardMovementStrategy>();
        }

        [Test]
        public void GetMovementStrategy_WhenLeft_ReturnsLeftMovementStrategy()
        {
            var strategy = MovementStrategyFactory.GetMovementStrategy(MovementCommand.Left);
            strategy.Should().BeOfType<LeftMovementStrategy>();
        }

        [Test]
        public void GetMovementStrategy_WhenRight_ReturnsLeftMovementStrategy()
        {
            var strategy = MovementStrategyFactory.GetMovementStrategy(MovementCommand.Right);
            strategy.Should().BeOfType<RightMovementStrategy>();
        }
    }
}