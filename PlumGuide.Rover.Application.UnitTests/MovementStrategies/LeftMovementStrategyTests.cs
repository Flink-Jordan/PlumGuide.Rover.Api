﻿using System;
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
    internal class LeftMovementStrategyTests
    {
        private Fixture _fixture;
        private Mock<IPositionProvider> _mockPositionProvider;
        private LeftMovementStrategy LeftMovementStrategy => new LeftMovementStrategy(_mockPositionProvider.Object);

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _mockPositionProvider = new Mock<IPositionProvider>();
        }

        [Test]
        public void Ctor_WhenPositionProviderIsNull_ThrowsArgumentNullException()
        {
            Func<LeftMovementStrategy> func = () => new LeftMovementStrategy(null);
            func.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("positionProvider");
        }

        [Test]
        public void Move_WhenValidDirection_UpdatesPosition()
        {
            var position = _fixture.Create<Position>();
            LeftMovementStrategy.Move(position);

            _mockPositionProvider.Verify(e => e.UpdatePosition(It.IsAny<Position>()), Times.Once);
        }

        [Test]
        public void Move_WhenNorth_ReturnsWest()
        {
            var position = _fixture.Build<Position>()
                .With(p => p.CompassDirection, CompassDirection.North)
                .Create();

            var result = LeftMovementStrategy.Move(position);

            using (new AssertionScope())
            {
                result.CompassDirection.Should().Be(CompassDirection.West);
                result.XPosition.Should().Be(position.XPosition);
                result.YPosition.Should().Be(position.YPosition);
            }
        }

        [Test]
        public void Move_WhenEast_ReturnsNorth()
        {
            var position = _fixture.Build<Position>()
                .With(p => p.CompassDirection, CompassDirection.East)
                .Create();

            var result = LeftMovementStrategy.Move(position);

            using (new AssertionScope())
            {
                result.CompassDirection.Should().Be(CompassDirection.North);
                result.XPosition.Should().Be(position.XPosition);
                result.YPosition.Should().Be(position.YPosition);
            }
        }

        [Test]
        public void Move_WhenSouth_ReturnsEast()
        {
            var position = _fixture.Build<Position>()
                .With(p => p.CompassDirection, CompassDirection.South)
                .Create();

            var result = LeftMovementStrategy.Move(position);

            using (new AssertionScope())
            {
                result.CompassDirection.Should().Be(CompassDirection.East);
                result.XPosition.Should().Be(position.XPosition);
                result.YPosition.Should().Be(position.YPosition);
            }
        }

        [Test]
        public void Move_WhenWest_ReturnsSouth()
        {
            var position = _fixture.Build<Position>()
                .With(p => p.CompassDirection, CompassDirection.West)
                .Create();

            var result = LeftMovementStrategy.Move(position);

            using (new AssertionScope())
            {
                result.CompassDirection.Should().Be(CompassDirection.South);
                result.XPosition.Should().Be(position.XPosition);
                result.YPosition.Should().Be(position.YPosition);
            }
        }
    }
}
