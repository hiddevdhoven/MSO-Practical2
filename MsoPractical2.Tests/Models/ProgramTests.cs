using MsoPractical2.Commands;

namespace MsoPractical2.Tests.Models;

public class ProgramTests
{
    [Test]
    public void ProgramEmpty() {
        var program = new Program();

        Assert.That(program, Is.Not.Null);
        using (Assert.EnterMultipleScope()) {
            Assert.That(program.Instructions, Is.Empty);
            Assert.That(program.ReadOnly, Is.False);
        }
    }

    [Test]
    public void ProgramReadOnly() {
        var program = new Program(readOnly: true);

        Assert.That(program.ReadOnly, Is.True);
    }

    [Test]
    public void ProgramWithInstructionsIsValid() {
        var commands = new List<Command> {
            new Move(5),
            new Turn(TurnDirection.Right)
        };
        var program = new Program(instructions: commands);
        var character = new MovableCharacter(0);

        using (Assert.EnterMultipleScope()) {
            Assert.That(program.Instructions, Has.Count.EqualTo(2));
            Assert.That(program.CheckValidity(character), Is.True);
        }
    }
}
