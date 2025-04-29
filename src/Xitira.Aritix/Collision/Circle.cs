using System;

namespace Xitira.Aritix.Collision;

public class Circle : ICollidable
{
    public float Radius { get; set; }
    public Vector2 Center { get; set; }
    
    public Circle(float radius, Vector2 center)
    {
         Radius = radius;
         Center = center;
    }

    public double GetArea()
    {
        return  Math.PI * Math.Pow(Radius, 2);
    }
}

