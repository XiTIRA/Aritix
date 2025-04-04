namespace Aritix.Collision;

public static class CollisionChecks
{
    public static bool CheckCollision(this Circle source, Circle target)
    {
        return true;
    }

    public static bool CheckCollision(this Rect source, Rect target)
    {
        return true;
    }

    public static bool CheckCollision(this Circle source, Rect target)
    {
        return true;
    }

    public static bool CheckCollision(this Rect source, Circle target)
    {
        return true;
    }
}