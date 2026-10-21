namespace MsoPractical2.Commands;

// Repeats a nested list of commands
public class Repeat : Command
{
    public Repeat(int times, IEnumerable<Command>? instructions = null) {
        ArgumentOutOfRangeException.ThrowIfNegative(times);
        Times = times;
        // If instructions are missing, create an empty list
        Instructions = instructions?.ToList() ?? new List<Command>();
    }

    public int Times { get; }
    public List<Command> Instructions { get; }

    // Checks whether all nested commands are valid.
    public override bool CheckValidity(Character character) {
        return Times >= 0 && Instructions.All(command => command.CheckValidity(character));
    }

    // Executes all nested commands the requested number of times.
    public override void Execute(Character character) {
        for (int i = 0; i < Times; i++)
            foreach (var command in Instructions)
                command.Execute(character);
    }

    public override string LogMessage() => $"Repeat {Times} times";

    // Visits this command and all nested commands
    public override void Accept(IVisitor visitor) {
        visitor.Visit(this);
        foreach (var instruction in Instructions)
            instruction.Accept(visitor);
    }
}
