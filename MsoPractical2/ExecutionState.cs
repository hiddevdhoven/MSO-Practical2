using MsoPractical2.Commands;

namespace MsoPractical2;

// Stores the logic and components for executing a program
public class ExecutionState
{
    private readonly Program program;
    private readonly Grid grid;
    private readonly List<Character> characters;
    private readonly List<string> actionLog = new();

    // For now, all programs are executed with one movable character
    public ExecutionState(Program program) {
        this.program = program ?? throw new ArgumentNullException(nameof(program));
        characters = new List<Character> { new MovableCharacter(0) };
        grid = new Grid();
        foreach (var character in characters) {
            grid.UpdatePosition(character.Id, new Position(character.Location, character.Facing));
        }
    }

    public Grid Grid => grid;
    public List<string> ActionLog => actionLog;
    public List<Character> Characters => characters;

    // Executes every command in the program
    public void Execute() {
        foreach (var character in characters) {
            if (!program.CheckValidity(character))
                throw new InvalidOperationException("The program contains an invalid command.");

            foreach (var command in program.Instructions)
                ExecuteCommand(command, character);
        }
    }

    // Map all Characters to an end state string and join with newlines
    public string GetEndState() {
        return String.Join("\n", characters.Select(c => {
            string location = $"({c.Location.X}, {c.Location.Y})";
            string facing = c.Facing.ToString().ToLower();
            return $"End state {location} facing {facing}.";
        }));
    }

    // Executes one command, handle repeats recursively, and save the position after
    private void ExecuteCommand(Command command, Character character) {
        if (command is Repeat repeat) {
            for (int i = 0; i < repeat.Times; i++)
                foreach (var instruction in repeat.Instructions)
                    ExecuteCommand(instruction, character);
            return;
        }

        command.Execute(character);
        actionLog.Add(command.LogMessage());
        grid.UpdatePosition(character.Id, new Position(character.Location, character.Facing));
    }
}
