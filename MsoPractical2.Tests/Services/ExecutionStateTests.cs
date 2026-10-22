using MsoPractical2.Commands;

namespace MsoPractical2.Tests.Services;

public class ExecutionStateTests
{
    [Test]
    public void ExecutionStateInitializedSuccessfully() {
        var program = new Program();
        var executionState = new ExecutionState(program);

        Assert.That(executionState, Is.Not.Null);
        using (Assert.EnterMultipleScope()) {
            Assert.That(executionState.Grid, Is.Not.Null);
            Assert.That(executionState.Characters, Is.Not.Empty);
            Assert.That(executionState.Characters[0], Is.TypeOf<MovableCharacter>());
            Assert.That(executionState.ActionLog, Is.Empty);
        }
    }

    [Test]
    public void ExecutionStateThrowExceptionWithoutProgram() {
        Assert.Throws<ArgumentNullException>(() => new ExecutionState(null!));
    }

    [Test]
    public void ExecutionStateRunsProgram() {
        var commands = new List<Command> {
            new Move(5),
            new Turn(TurnDirection.Right)
        };
        var program = new Program(instructions: commands);
        var executionState = new ExecutionState(program);

        Assert.DoesNotThrow(() => executionState.Execute());
        Assert.That(executionState.ActionLog, Has.Count.EqualTo(2));
        Assert.That(executionState.ActionLog[0], Is.EqualTo("Move 5"));
        Assert.That(executionState.ActionLog[1], Is.EqualTo("Turn right"));
    }
}
