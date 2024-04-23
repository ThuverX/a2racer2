using System.Numerics;
using Jitter2.Collision.Shapes;

record CarConfig
{
    public record CarWheelConfig
    {
        public float SideFriction = 3.2f;
        public float ForwardFriction = 5.0f;
        public float WheelTravel = 0.2f;
        public float MaximumAngularVelocity = 200;
    }

    public class Cars
    {
        public static CarConfig bmw = new CarConfig
        {
            name = "BMW",
            model = "r_bmw",
            texture = "racer01",
            wheelPositions = new Vector3[]
            {
                new(-0.65f, 0.3f, -1.3f),
                new(+0.65f, 0.3f, -1.3f),
                new(-0.65f, 0.3f, +1.25f),
                new(+0.65f, 0.3f, +1.25f)
            },
            wheelRadius = 0.5f,
            collisionShape = new Vector3(1.6f, 1.4f, 3.6f),
            centerOfMassHeight = 0.2f,
            accelerationRate = 10f,
            driveTorque = 340f,
            steerRate = 1.4f,
            mass = 1200,
            wheelConfig = new CarWheelConfig
            {
                SideFriction = 8f,
                ForwardFriction = 2f,
                WheelTravel = 0.2f,
                MaximumAngularVelocity = 200,
            }
        };
    }

    public required string name;
    public required string model;
    public required string texture;
    public required Vector3[] wheelPositions;
    public required float wheelRadius;
    public required Vector3 collisionShape;
    public required float accelerationRate;
    public required float driveTorque;
    public required float steerRate;
    public required float mass;
    public required float centerOfMassHeight;
    public required CarWheelConfig wheelConfig = new();
}
