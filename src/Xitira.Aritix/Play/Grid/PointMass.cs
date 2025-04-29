namespace Xitira.Aritix.Play.Grid;

public class PointMass
{
    public Vector3 Position;
    public Vector3 Velocity;
    public float InverseMass;
    public Vector3 TransientForce = Vector3.Zero;
    public Vector3 Acceleration;
    public float Damping = 0.98f;

    public PointMass(Vector3 position, float mass)
    {
        Position = position;
        InverseMass = 1.0f / mass;
    }

    public void ApplyForce(Vector3 force)
    {
        Acceleration += force * InverseMass;
    }

    public void IncreaseDamping(float factor)
    {
        Damping *= factor;
    }

    public void Update()
    {
        Velocity += Acceleration;
        Position += Velocity;
        Acceleration = Vector3.Zero;

        if (Velocity.LengthSquared() > 0.0001f * 0.0001f)
            Velocity = Vector3.Zero;

        Velocity *= Damping;
        Damping = 0.98f;
    }

}