namespace MsoPractical2.Tests.Models;

public class GridTests
{
    [Test]
    public void GridCreatedSuccessfully() {
        var grid = new Grid();

        Assert.That(grid, Is.Not.Null);
    }

    [Test]
    public void GridUpdatePosition() {
        var grid = new Grid();
        var position = new Position(new Coordinate(5, 5), Direction.East);

        Assert.DoesNotThrow(() => grid.UpdatePosition(42, position));
        Assert.That(grid.GetPosition(42), Is.EqualTo(position));
    }
}
