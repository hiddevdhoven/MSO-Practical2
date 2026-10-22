namespace MsoPractical2.Tests.Models;

public class PositionTests
{
    [Test]
    public void PositionsInitializeCorrectly() {
        var location1 = new Coordinate(3, 4);
        var location2 = new Coordinate(-7, 0);
        var position1 = new Position(location1, Direction.North);
        var position2 = new Position(location2, Direction.South);

        using (Assert.EnterMultipleScope()) {
            Assert.That(position1.Location, Is.EqualTo(location1));
            Assert.That(position1.Facing, Is.EqualTo(Direction.North));
            Assert.That(position2.Location, Is.EqualTo(location2));
            Assert.That(position2.Facing, Is.EqualTo(Direction.South));
        }
    }
}
