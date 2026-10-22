using MsoPractical2.Commands;

namespace MsoPractical2.Tests.Commands;

public class RepeatCommandTests
{
    [Test]
    public void RepeatCommandEmpty() {
        var repeatCommand = new Repeat(3);

        Assert.That(repeatCommand, Is.Not.Null);
        using (Assert.EnterMultipleScope()) {
            Assert.That(repeatCommand.Times, Is.EqualTo(3));
            Assert.That(repeatCommand.Instructions, Is.Empty);
        }
    }

    [Test]
    public void RepeatCommandInstructions() {
        var instructions = new List<Command>
        {
            new Move(5),
            new Turn(TurnDirection.Right)
        };
        var repeatCommand = new Repeat(2, instructions);

        using (Assert.EnterMultipleScope()) {
            Assert.That(repeatCommand.Times, Is.EqualTo(2));
            Assert.That(repeatCommand.Instructions, Has.Count.EqualTo(2));
        }
    }

    [Test]
    public void RepeatCommandZeroTimes() {
        var repeatCommand = new Repeat(0);

        Assert.That(repeatCommand.Times, Is.EqualTo(0));
    }

    [Test]
    public void RepeatCommandNegativeTimesException() {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Repeat(-1));
    }

    [Test]
    public void RepeatCommandIsValidAndExecutes() {
        var instructions = new List<Command>
        {
            new Move(5),
            new Turn(TurnDirection.Right)
        };
        var repeatCommand = new Repeat(2, instructions);
        var character = new MovableCharacter(0);

        Assert.That(repeatCommand.CheckValidity(character), Is.True);
        Assert.DoesNotThrow(() => repeatCommand.Execute(character));
        using (Assert.EnterMultipleScope()) {
            Assert.That(character.Location.X, Is.EqualTo(5));
            Assert.That(character.Location.Y, Is.EqualTo(-5));
            Assert.That(character.Facing, Is.EqualTo(Direction.West));
        }
    }
}
