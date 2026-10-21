namespace MsoPractical2;

// Stores the latest position of each character
public class Grid
{
    // Use a Dictionary instead of a 2D array so there are no bounds
    private readonly Dictionary<int, Position> charPositions = new();

    // Saves or updates a character's position
    public void UpdatePosition(int characterId, Position position) =>
        charPositions[characterId] = position;

}
