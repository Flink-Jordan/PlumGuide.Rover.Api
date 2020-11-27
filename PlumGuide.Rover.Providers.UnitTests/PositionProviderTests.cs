using System;
using AutoFixture;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PlumGuide.Rover.Models;

namespace PlumGuide.Rover.Providers.UnitTests
{
    [TestFixture]
    internal class PositionProviderTests
    {
        private Fixture _fixture;
        private Mock<IRoverPositionState> _mockRoverPositionState;

        private PositionProvider PositionProvider => new PositionProvider(_mockRoverPositionState.Object);

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _mockRoverPositionState = new Mock<IRoverPositionState>();
        }

        [Test]
        public void Ctor_WhenPositionProviderIsNull_ThrowsArgumentNullException()
        {
            Func<PositionProvider> func = () => new PositionProvider(null);
            func.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("roverPositionState");
        }

        [Test]
        public void GetPosition_ReturnsRoverPositionStatePosition()
        {
            var position = _fixture.Create<Position>();
            _mockRoverPositionState
                .Setup(e => e.GetPosition())
                .Returns(position);

            PositionProvider.GetPosition().Should().Be(position);
        }

        [Test]
        public void UpdatePosition_WhenPositionIsNull_ThrowsArgumentNullException()
        {
            Action act = () => PositionProvider.UpdatePosition(null);
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("position");
        }

        [Test]
        public void UpdatePosition_WhenPositionIsNotNull_SetsPositionOnRoverPositionState()
        {
            var position = _fixture.Create<Position>();

            PositionProvider.UpdatePosition(position);

            _mockRoverPositionState.Verify(e => e.SetPosition(position),Times.Once);
        }
    }
}
