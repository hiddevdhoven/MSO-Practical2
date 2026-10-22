using MsoPractical2.Commands;

namespace MsoPractical2.Tests.Characters;

public class MovableCharacterTests
{
    [Test]
    public void MovableCharacterDefaultSetup() {
        var character = new MovableCharacter(0);

        Assert.That(character, Is.Not.Null);
        using (Assert.EnterMultipleScope()) {
            Assert.That(character.Id, Is.EqualTo(0));
            Assert.That(character.Location.X, Is.EqualTo(0));
            Assert.That(character.Location.Y, Is.EqualTo(0));
            Assert.That(character.Facing, Is.EqualTo(Direction.East));
            Assert.That(character.StepsMoved, Is.EqualTo(0));
        }
    }

    [Test]
    public void MovableCharacterCustomSetup() {
        var character = new MovableCharacter(42, new Coordinate(6, 42), Direction.North);

        using (Assert.EnterMultipleScope()) {
            Assert.That(character.Id, Is.EqualTo(42));
            Assert.That(character.Location.X, Is.EqualTo(6));
            Assert.That(character.Location.Y, Is.EqualTo(42));
            Assert.That(character.Facing, Is.EqualTo(Direction.North));
            Assert.That(character.StepsMoved, Is.EqualTo(0));
        }
    }

    [Test]
    public void MovableCharacterMovesInAllDirections() {
        var character = new MovableCharacter(0);
        Assert.DoesNotThrow(() => character.Move(5));
        using (Assert.EnterMultipleScope()) {
            Assert.That(character.Location.X, Is.EqualTo(5));
            Assert.That(character.Location.Y, Is.EqualTo(0));
        }

        Assert.DoesNotThrow(() => character.Turn(TurnDirection.Left));
        Assert.DoesNotThrow(() => character.Move(3));
        using (Assert.EnterMultipleScope()) {
            Assert.That(character.Location.X, Is.EqualTo(5));
            Assert.That(character.Location.Y, Is.EqualTo(3));
        }

        Assert.DoesNotThrow(() => character.Turn(TurnDirection.Left));
        Assert.DoesNotThrow(() => character.Move(8));
        using (Assert.EnterMultipleScope()) {
            Assert.That(character.Location.X, Is.EqualTo(-3));
            Assert.That(character.Location.Y, Is.EqualTo(3));
        }

        Assert.DoesNotThrow(() => character.Turn(TurnDirection.Left));
        Assert.DoesNotThrow(() => character.Move(5));
        using (Assert.EnterMultipleScope()) {
            Assert.That(character.Location.X, Is.EqualTo(-3));
            Assert.That(character.Location.Y, Is.EqualTo(-2));
        }

        Assert.DoesNotThrow(() => character.Turn(TurnDirection.Right));
        using (Assert.EnterMultipleScope()) {
            Assert.That(character.Facing, Is.EqualTo(Direction.West));
            Assert.That(character.StepsMoved, Is.EqualTo(21));
        }
    }
}
