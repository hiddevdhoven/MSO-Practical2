using MsoPractical2.Commands;

namespace MsoPractical2.Tests.Commands;

public class MoveCommandTests
{
    [Test]
    public void MoveCommandWithSteps() {
        var moveCommand = new Move(10);

        Assert.That(moveCommand, Is.Not.Null);
        Assert.That(moveCommand.Steps, Is.EqualTo(10));
    }

    [Test]
    public void MoveCommandZeroSteps() {
        var moveCommand = new Move(0);

        Assert.That(moveCommand.Steps, Is.EqualTo(0));
    }

    [Test]
    public void MoveCommandLogMessage() {
        var moveCommand = new Move(5);

        var message = moveCommand.LogMessage();

        Assert.That(message, Is.EqualTo("Move 5"));
    }

    [Test]
    public void MoveCommandIsValidAndExecutes() {
        var moveCommand = new Move(5);
        var character = new MovableCharacter(0);

        Assert.That(moveCommand.CheckValidity(character), Is.True);
        Assert.DoesNotThrow(() => moveCommand.Execute(character));
        using (Assert.EnterMultipleScope()) {
            Assert.That(character.Location.X, Is.EqualTo(5));
            Assert.That(character.Location.Y, Is.EqualTo(0));
        }
    }
}
