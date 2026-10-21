namespace MsoPractical2.Commands;

// Move a character a number of steps in the current direction
public class Move(int steps) : Command
{
    public int Steps { get; } = steps;

    // Checks whether the character can move the requested steps
    public override bool CheckValidity(Character character) {
        return character is MovableCharacter && Steps >= 0;
    }

    // Execute the move command if possible
    public override void Execute(Character character) {
        if (character is not MovableCharacter movable)
            throw new InvalidOperationException("The 'Move' command requires a movable character!");
        movable.Move(Steps);
    }

    public override string LogMessage() => $"Move {Steps}";

    // Implement Visitor pattern
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
}
