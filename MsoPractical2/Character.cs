using MsoPractical2.Commands;

namespace MsoPractical2;

// Defines the four directions a character can face
public enum Direction
{
    East,
    South,
    West,
    North
}

public class Character
{
    protected Character(int id, Coordinate location = default, Direction facing = Direction.East) {
        Id = id;
        Location = location;
        Facing = facing;
    }

    public int Id { get; }
    public Coordinate Location { get; protected set; }
    public Direction Facing { get; protected set; }

    // Turns the character left or right
    public void Turn(TurnDirection direction) {
        int offset = direction == TurnDirection.Right ? 1 : -1;
        Facing = (Direction)(((int)Facing + offset + 4) % 4);
    }
}
