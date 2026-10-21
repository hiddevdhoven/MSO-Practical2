namespace MsoPractical2.Commands;

// The directions in which a character can turn
public enum TurnDirection
{
    Left,
    Right
}

public class Turn(TurnDirection direction) : Command
{
    public TurnDirection Direction { get; } = direction;

    public override bool CheckValidity(Character character) => true;

    public override void Execute(Character character) => character.Turn(Direction);

    public override string LogMessage() => $"Turn {Direction.ToString().ToLower()}";

    // Implement Visitor pattern
    public override void Accept(IVisitor visitor) => visitor.Visit(this);
}
