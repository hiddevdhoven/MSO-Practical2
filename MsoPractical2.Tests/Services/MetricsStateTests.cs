using MsoPractical2.Commands;

namespace MsoPractical2.Tests.Services;

public class MetricsStateTests
{
    [Test]
    public void MetricsStateInitializedSuccessfully() {
        var program = new Program();
        var metricsState = new MetricsState(program);

        Assert.That(metricsState, Is.Not.Null);
        using (Assert.EnterMultipleScope()) {
            Assert.That(metricsState.TotalCommands, Is.EqualTo(0));
            Assert.That(metricsState.TotalRepeats, Is.EqualTo(0));
            Assert.That(metricsState.MaxNesting, Is.EqualTo(0));
        }
    }

    [Test]
    public void MetricsStateThrowExceptionWithoutProgram() {
        Assert.Throws<ArgumentNullException>(() => new MetricsState(null!));
    }

    [Test]
    public void MetricsStateCalculatesCorrectly1() {
        var commands = new List<Command> {
            new Move(5),
            new Turn(TurnDirection.Left),
            new Move(3)
        };
        var program = new Program(instructions: commands);
        var metricsState = new MetricsState(program);

        using (Assert.EnterMultipleScope()) {
            Assert.That(metricsState.TotalCommands, Is.EqualTo(3));
            Assert.That(metricsState.TotalRepeats, Is.EqualTo(0));
            Assert.That(metricsState.MaxNesting, Is.EqualTo(0));
        }
    }

    [Test]
    public void MetricsStateCalculatesCorrectly2() {
        var commands = new List<Command> {
            new Repeat(2, new List<Command> { new Move(5) })
        };
        var program = new Program(instructions: commands);
        var metricsState = new MetricsState(program);

        using (Assert.EnterMultipleScope()) {
            Assert.That(metricsState.TotalCommands, Is.EqualTo(2));
            Assert.That(metricsState.TotalRepeats, Is.EqualTo(1));
            Assert.That(metricsState.MaxNesting, Is.EqualTo(1));
        }
    }

    [Test]
    public void MetricsStateCalculatesCorrectly3() {
        var innerRepeat = new Repeat(2, new List<Command> { new Move(5) });
        var outerRepeat = new Repeat(3, new List<Command> { innerRepeat });
        var commands = new List<Command> { outerRepeat };
        var program = new Program(instructions: commands);
        var metricsState = new MetricsState(program);

        using (Assert.EnterMultipleScope()) {
            Assert.That(metricsState.TotalCommands, Is.EqualTo(3));
            Assert.That(metricsState.TotalRepeats, Is.EqualTo(2));
            Assert.That(metricsState.MaxNesting, Is.EqualTo(2));
        }
    }
}
