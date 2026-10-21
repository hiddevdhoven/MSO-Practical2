using MsoPractical2.Commands;

namespace MsoPractical2;

// Program with a list of commands
public class Program
{
    // Create an empty list if no instructions are provided
    public Program(bool readOnly = false, IEnumerable<Command>? instructions = null) {
        ReadOnly = readOnly;
        Instructions = instructions?.ToList() ?? new List<Command>();
    }

    public bool ReadOnly { get; }
    public List<Command> Instructions { get; }

    // Checks whether every command can operate on a character
    public bool CheckValidity(Character character) =>
        Instructions.All(command => command.CheckValidity(character));

    // Check validity with a default movable character
    public bool CheckValidity() => CheckValidity(new MovableCharacter(0));
}
