namespace MsoPractical2;

// Stores the latest position of each character
public class Grid
{
    // Use a Dictionary instead of a 2D array so there are no bounds
    private readonly Dictionary<int, Position> charPositions = new();

    public Celltype[,] cellGrid;

    // Saves or updates a character's position
    public void UpdatePosition(int characterId, Position position) {
        charPositions[characterId] = position;
    }

    // Get an entity's position by Id
    public Position GetPosition(int characterId) {
        return charPositions[characterId];
    }
}

public enum Celltype
{
    Start,
    free,
    Blocked,
    Finish
}
