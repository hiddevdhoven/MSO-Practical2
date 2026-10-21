using MsoPractical2.Commands;

namespace MsoPractical2;

// Calculates program metrics through the visitor pattern
public class MetricsState : IVisitor
{
    private readonly Program program;
    private int commandCount;
    private int repeatCount;

    public int TotalCommands { get; private set; }
    public int MaxNesting { get; private set; }
    public int TotalRepeats { get; private set; }

    // Calculate all metrics upon creation
    public MetricsState(Program program) {
        this.program = program ?? throw new ArgumentNullException(nameof(program));
        Calculate();
    }

    // Calculates all metrics and populates properties
    private void Calculate() {
        commandCount = 0;
        repeatCount = 0;

        VisitInstructions(program.Instructions);

        TotalCommands = commandCount;
        TotalRepeats = repeatCount;
        // Calculate nesting by finding the maximum nesting level of any Command tree
        MaxNesting = program.Instructions.Count == 0
            ? 0
            : program.Instructions.Max(GetNesting);
    }

    // Visits each Command instance in a Command collection
    private void VisitInstructions(IEnumerable<Command> instructions) {
        foreach (var instruction in instructions)
            instruction.Accept(this);
    }

    // Visitor methods for each command type
    public void Visit(Move move) => commandCount++;
    public void Visit(Turn turn) => commandCount++;
    public void Visit(Repeat repeat) {
        commandCount++;
        repeatCount++;
    }

    // Calculates nesting for a list of instructions (recursive)
    private static int GetNesting(Command command) {
        if (command is Repeat repeat) {
            return 1 + repeat.Instructions.Max(GetNesting);
        } else {
            return 0;
        }
    }
}
