using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using PlumGuide.Rover.Models;

namespace PlumGuide.Rover.Application.UnitTests
{
    [TestFixture]
    public class BoundaryAdjusterTests
    {
        private Fixture _fixture;
        private BoundaryAdjuster BoundaryAdjuster => new BoundaryAdjuster();

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void WrapXAround_WhenTargetPositionLowerThanDefaultXPosition_ReturnsMaximumXPosition()
        {
            var targetPosition = (int)PositioningConstants.DefaultXPosition - _fixture.Create<int>();
            BoundaryAdjuster.WrapXAround(targetPosition).Should().Be(PositioningConstants.MaximumXPosition);
        }

        [Test]
        public void WrapXAround_WhenTargetPositionGreaterThanMaximumXPosition_ReturnsDefaultXPosition()
        {
            var targetPosition = (int)PositioningConstants.MaximumXPosition + _fixture.Create<int>();
            BoundaryAdjuster.WrapXAround(targetPosition).Should().Be(PositioningConstants.DefaultXPosition);
        }

        [Test]
        public void WrapXAround_WhenTargetPositionWithinBoundaries_ReturnsTargetPosition()
        {
            var targetPosition = (int)PositioningConstants.MaximumXPosition;
            BoundaryAdjuster.WrapXAround(targetPosition).Should().Be((uint)targetPosition);
        }

        [Test]
        public void WrapYAround_WhenTargetPositionLowerThanDefaultYPosition_ReturnsMaximumYPosition()
        {
            var targetPosition = (int)PositioningConstants.DefaultYPosition - _fixture.Create<int>();
            BoundaryAdjuster.WrapYAround(targetPosition).Should().Be(PositioningConstants.MaximumYPosition);
        }

        [Test]
        public void WrapYAround_WhenTargetPositionGreaterThanMaximumYPosition_ReturnsDefaultYPosition()
        {
            var targetPosition = (int)PositioningConstants.MaximumYPosition + _fixture.Create<int>();
            BoundaryAdjuster.WrapYAround(targetPosition).Should().Be(PositioningConstants.DefaultYPosition);
        }

        [Test]
        public void WrapYAround_WhenTargetPositionWithinBoundaries_ReturnsTargetPosition()
        {
            var targetPosition = (int)PositioningConstants.MaximumYPosition;
            BoundaryAdjuster.WrapYAround(targetPosition).Should().Be((uint)targetPosition);
        }
    }
}