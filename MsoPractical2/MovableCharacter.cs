namespace MsoPractical2;

public class MovableCharacter(int id, Coordinate location = default, Direction facing = Direction.East) : Character(id, location, facing)
{
    // record total steps moved since creation
    public int StepsMoved { get; private set; }

    public void Move(int steps) {
        int x = Location.X;
        int y = Location.Y;
        switch (Facing) {
            case Direction.East:
                x += steps;
                break;
            case Direction.West:
                x -= steps;
                break;
            case Direction.North:
                y += steps;
                break;
            case Direction.South:
                y -= steps;
                break;
        }

        Location = new Coordinate(x, y);
        StepsMoved += Math.Abs(steps);
    }
}
