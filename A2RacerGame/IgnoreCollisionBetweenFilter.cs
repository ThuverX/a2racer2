using Jitter2.Collision;
using Jitter2.Collision.Shapes;

public class IgnoreCollisionBetweenFilter : IBroadPhaseFilter
{
    private readonly struct Pair : IEquatable<Pair>
    {
        private readonly Shape shapeA,
            shapeB;

        public Pair(Shape shapeA, Shape shapeB)
        {
            this.shapeA = shapeA;
            this.shapeB = shapeB;
        }

        public bool Equals(Pair other) =>
            shapeA.Equals(other.shapeA) && shapeB.Equals(other.shapeB);

        public override bool Equals(object? obj) => obj is Pair other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(shapeA, shapeB);
    }

    private readonly HashSet<Pair> ignore = new();

    public bool Filter(Shape shapeA, Shape shapeB)
    {
        ulong a = shapeA.ShapeId;
        ulong b = shapeB.ShapeId;

        if (b < a)
            (shapeA, shapeB) = (shapeB, shapeA);
        return !ignore.Contains(new Pair(shapeA, shapeB));
    }

    public void IgnoreCollisionBetween(Shape shapeA, Shape shapeB)
    {
        ulong a = shapeA.ShapeId;
        ulong b = shapeB.ShapeId;

        if (b < a)
            (shapeA, shapeB) = (shapeB, shapeA);
        ignore.Add(new Pair(shapeA, shapeB));
    }
}
