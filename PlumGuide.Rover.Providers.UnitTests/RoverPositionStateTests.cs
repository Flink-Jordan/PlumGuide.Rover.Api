using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using PlumGuide.Rover.Models;

namespace PlumGuide.Rover.Providers.UnitTests
{
    [TestFixture]
    internal class RoverPositionStateTests
    {
        [Test]
        public void CurrentPosition_DefaultsCorrectly()
        {
            var state = new RoverPositionState();
            var position = state.GetPosition();

            using (new AssertionScope())
            {
                position.CompassDirection.Should().Be(CompassDirection.North);
                position.XPosition.Should().Be(0);
                position.YPosition.Should().Be(0);
            }
        }
    }
}
