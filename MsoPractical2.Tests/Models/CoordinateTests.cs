namespace MsoPractical2.Tests.Models;

public class CoordinateTests
{
    [Test]
    public void CoordinatesInitializeCorrectly() {
        var coordinate1 = new Coordinate(0, 0);
        var coordinate2 = new Coordinate(5, 10);
        var coordinate3 = new Coordinate(-8, -2);

        using (Assert.EnterMultipleScope()) {
            Assert.That(coordinate1.X, Is.EqualTo(0));
            Assert.That(coordinate1.Y, Is.EqualTo(0));
            Assert.That(coordinate2.X, Is.EqualTo(5));
            Assert.That(coordinate2.Y, Is.EqualTo(10));
            Assert.That(coordinate3.X, Is.EqualTo(-8));
            Assert.That(coordinate3.Y, Is.EqualTo(-2));
        }
    }
}
