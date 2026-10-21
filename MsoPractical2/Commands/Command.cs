namespace MsoPractical2.Commands;

public abstract class Command
{
    // Subclasses must implement at least the methods to check if the command is valid, execute the command, and log the command
    public abstract bool CheckValidity(Character character);
    public abstract void Execute(Character character);
    public abstract string LogMessage();
    // accept IVisitor instances
    public abstract void Accept(IVisitor visitor);
}

// Enables visiting of each command type
public interface IVisitor
{
    void Visit(Move move);
    void Visit(Turn turn);
    void Visit(Repeat repeat);
}
