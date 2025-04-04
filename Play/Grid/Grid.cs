using Aritix.Extensions;

namespace Aritix.Play.Grid;

using System.Collections.Generic;



public class Grid
{
    public Spring [] Springs;
    public PointMass [,] Points;
    public Vector2 ScreenSize ;
    public Texture2D Pixel;

    public Texture2D Pixel2;

    public Grid(Rectangle size, Vector2 spacing, Vector2 screenSize, Texture2D pixel, Texture2D pixel2)
    {
        Pixel2 = pixel2;
        Pixel = pixel;
        ScreenSize = screenSize;
        var springList = new List<Spring>();

        int numColumns = (int)( size.Width / spacing.X) + 1;
        int numRows = (int)( size.Height / spacing.Y) + 1;

        Points = new PointMass [numColumns, numRows];
        PointMass [,] fixedPoints = new PointMass [numColumns, numRows];

        int column = 0, row = 0;

        for (float y = size.Top; y <= size.Bottom; y += spacing.Y)
        {
            for (float x = size.Left; x <= size.Right; x += spacing.X)
            {
                Points[column, row] = new PointMass(new Vector3(x, y, 0), 1);
                fixedPoints[column, row] = new PointMass(new Vector3(x, y, 0), 0);
                column++;
            }

            row++;
            column = 0;
        }

        // link the point masses with springs

        for (int y = 0; y < numRows; y++)

        for (int x = 0; x < numColumns; x++)

        {
            if (x == 0 || y == 0 || x == numColumns - 1 || y == numRows - 1)	// anchor the border of the grid

                springList.Add(new Spring(fixedPoints[x, y], Points[x, y], 0.1f, 0.1f));

            else if (x % 3 == 0 && y % 3 == 0)									// loosely anchor 1/9th of the point masses

                springList.Add(new Spring(fixedPoints[x, y], Points[x, y], 0.002f, 0.02f));

            const float stiffness = 0.28f;

            const float damping = 0.06f;

            if (x > 0)

                springList.Add(new Spring(Points[x - 1, y], Points[x, y], stiffness, damping));

            if (y > 0)

                springList.Add(new Spring(Points[x, y - 1], Points[x, y], stiffness, damping));
        }

        Springs = springList.ToArray();
    }

    public void Update()
    {
        foreach (var spring in Springs)
        {
            spring.Update();
        }

        foreach (var mass in Points)
        {
            mass.Update();
        }
    }

    public void ApplyDirectedForce(Vector3 force, Vector3 position, float radius)
    {
        foreach (var mass in Points)
        {
            if (Vector3.DistanceSquared(position, mass.Position) < radius * radius)
            {
                mass.ApplyForce(10 * force / (10 + Vector3.Distance(position, mass.Position)));
            }
        }
    }

    public void ApplyImplosiveForce(float force, Vector3 position, float radius)
    {
        foreach (var mass in Points)
        {
            float dist2 = Vector3.DistanceSquared(position, mass.Position);
            if (dist2 < radius * radius)
            {
                mass.ApplyForce(10 * force * (position - mass.Position) / (100 + dist2));
                mass.IncreaseDamping(0.6f);
            }
        }
    }

    public void ApplyExplosiveForce(float force, Vector3 position, float radius)
    {
        foreach (var mass in Points)
        {
            float dist2 = Vector3.DistanceSquared(position, mass.Position);
            if (dist2 < radius * radius)
            {
                mass.ApplyForce(100 * force * (mass.Position - position) / (10000 + dist2));
                mass.IncreaseDamping(0.6f);
            }
        }
    }

    public Vector2 ToVec2(Vector3 v)

    {
        // do a perspective projection

        float factor = (v.Z + 2000) / 2000;

        return (new Vector2(v.X, v.Y) - ScreenSize / 2f) * factor + ScreenSize / 2;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        int width = Points.GetLength(0);
        int height = Points.GetLength(1);
        Color color = new Color(30, 30, 139, 85);

        for (int y = 1; y < height; y++)

        {
            for (int x = 1; x < width; x++)
            {
                Vector2 left = new Vector2(), up = new Vector2();
                Vector2 p = ToVec2(Points[x, y].Position);
                Vector2 origin = new Vector2(Pixel2.Width / 2, Pixel2.Height / 2);

                // if (x > 1)
                //
                // {
                //     left = ToVec2(Points[x - 1, y].Position);
                //
                //     float thickness = y % 3 == 1 ? 3f : 1f;
                //
                //     spriteBatch.DrawLine(left, p, color, Pixel, thickness);
                // }
                //
                // if (y > 1)
                //
                // {
                //     up = ToVec2(Points[x, y - 1].Position);
                //
                //     float thickness = x % 3 == 1 ? 3f : 1f;
                //
                //     spriteBatch.DrawLine(up, p, color, Pixel, thickness);
                // }

                PointMass point = Points[x, y];


                float scale = .7f * point.TransientForce.Length();
                float rotation = point.TransientForce.Length();

                spriteBatch.Draw(
                    Pixel2,
                    p,
                    null,
                    Color.White,
                    rotation,
                    origin,
                    scale,
                    SpriteEffects.None,
                    0f
                );
            }
        }




    }
}