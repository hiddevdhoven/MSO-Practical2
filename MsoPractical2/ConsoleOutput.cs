namespace MsoPractical2;

public class ConsoleOutput : Output
{
    // Writes one message to the console
    public override void Display(string message) {
        Console.WriteLine(message);
    }
}
