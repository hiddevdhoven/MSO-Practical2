using MsoPractical2.Commands;

namespace MsoPractical2.Tests.Commands;

public class TurnCommandTests
{
    [Test]
    public void TurnCommandLeft() {
        var turnCommand = new Turn(TurnDirection.Left);

        Assert.That(turnCommand, Is.Not.Null);
        Assert.That(turnCommand.Direction, Is.EqualTo(TurnDirection.Left));
    }

    [Test]
    public void TurnCommandRight() {
        var turnCommand = new Turn(TurnDirection.Right);

        Assert.That(turnCommand.Direction, Is.EqualTo(TurnDirection.Right));
    }

    [Test]
    public void TurnCommandLogMessage() {
        var turnCommand = new Turn(TurnDirection.Left);
        var message = turnCommand.LogMessage();

        Assert.That(message, Is.EqualTo("Turn left"));
    }

    [Test]
    public void TurnCommandIsValidAndExecutes() {
        var turnCommand = new Turn(TurnDirection.Left);
        var character = new MovableCharacter(0);

        Assert.That(turnCommand.CheckValidity(character), Is.True);
        Assert.DoesNotThrow(() => turnCommand.Execute(character));
        Assert.That(character.Facing, Is.EqualTo(Direction.North));
    }
}
