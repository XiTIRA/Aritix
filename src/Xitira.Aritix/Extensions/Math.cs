using System;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Vector3 = Microsoft.Xna.Framework.Vector3;

namespace Xitira.Aritix.Extensions;

public static class MathExtensions
{
    public static float Map(this float value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }

    public static float MapClamp(this float value, float fromSource, float toSource, float fromTarget,
        float toTarget)
    {
        float res = Map(value, fromSource, toSource, fromTarget, toTarget);
        if (res < fromTarget)
        {
            return fromTarget;
        }
        if (res > toTarget)
        {
            return toTarget;
        }
        
        return res;
    }

    public static float ToAngle(this Vector2 vector)
    {
        return (float)Math.Atan2(vector.Y, vector.X);
    }

    public static Point ToPoint(this Vector2 vector)
    {
        return new Point((int)vector.X, (int)vector.Y);
    }

    public static Vector2 ToVector2(this Point point)
    {
        return new Vector2(point.X, point.Y);
    }

    public static Vector2 ToVector2(this Rectangle rectangle)
    {
        return new Vector2(rectangle.Width, rectangle.Height);
    }

    public static Vector3 ToVector3(this Vector2 vector, float z) => new Vector3(vector.X, vector.Y, z);
}