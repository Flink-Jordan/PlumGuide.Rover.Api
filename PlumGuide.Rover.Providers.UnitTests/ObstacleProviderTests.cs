using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PlumGuide.Rover.Models;

namespace PlumGuide.Rover.Providers.UnitTests
{
    [TestFixture]
    public class ObstacleProviderTests
    {
        private Fixture _fixture;
        private Mock<IRoverPositionState> _mockRoverPositionState;

        private ObstacleProvider ObstacleProvider => new ObstacleProvider(_mockRoverPositionState.Object);

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mockRoverPositionState = new Mock<IRoverPositionState>();
        }

        [Test]
        public void Ctor_WhenRoverPositionStateIsNull_ThrowsArgumentNullException()
        {
            Func<ObstacleProvider> func = () => new ObstacleProvider(null);
            func.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("roverPositionState");
        }

        [Test]
        public void AddObstacle_WhenObstacleIsNull_ThrowsInvalidOperationException()
        {
            Action act = () => ObstacleProvider.AddObstacle(null);

            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("obstacle");
        }

        [Test]
        public void AddObstacle_WhenObstacleNotNull_CallsAddObstacleToRoverPositionState()
        {
            var obstacle = _fixture.Create<Obstacle>();
            ObstacleProvider.AddObstacle(obstacle);
            _mockRoverPositionState.Verify(e => e.AddObstacle(obstacle), Times.Once);
        }

        [Test]
        public void RemoveObstacle_WhenObstacleIsNull_ThrowsInvalidOperationException()
        {
            Action act = () => ObstacleProvider.RemoveObstacle(null);

            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("obstacle");
        }

        [Test]
        public void RemoveObstacle_WhenObstacleNotNull_CallsRemoveObstacleToRoverPositionState()
        {
            var obstacle = _fixture.Create<Obstacle>();
            ObstacleProvider.RemoveObstacle(obstacle);
            _mockRoverPositionState.Verify(e => e.RemoveObstacle(obstacle), Times.Once);
        }

        [Test]
        public void GetObstacle_ReturnsDefaultWhenNoMatchingObstacle()
        {
            _mockRoverPositionState
                .Setup(e => e.GetObstacles())
                .Returns(Array.Empty<Obstacle>());

            var matchingObstacle = 
                ObstacleProvider.GetObstacle(_fixture.Create<uint>(), _fixture.Create<uint>());

            matchingObstacle.Should().BeNull();
        }

        [Test]
        public void GetObstacle_ReturnsMatchingObstacle()
        {
            var obstacles = _fixture.Create<IReadOnlyList<Obstacle>>();
            
            _mockRoverPositionState
                .Setup(e => e.GetObstacles())
                .Returns(obstacles);
            
            var matchingObstacle =
                ObstacleProvider
                    .GetObstacle(obstacles.First().XPosition, obstacles.First().YPosition);

            matchingObstacle.Should().Be(obstacles.First());
        }
    }
}