using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace Stella.Core.DataStructures;

public struct Circle
{
    public float Radius;

    public Vector2 Center;

    public Circle(Vector2 center, float radius)
    {
        Center = center;
        Radius = radius;
    }

    private static Vector2 RandomPointUnitCircle() => Main.rand.NextVector2Unit() * (float)Math.Sqrt(Main.rand.NextDouble());

    public readonly Vector2 RandomPointInCircle() => Center + RandomPointUnitCircle() * Radius;

    public readonly Vector2 RandomPointOnCircleEdge()
    {
        Vector2 v = RandomPointUnitCircle();
        // Normalize so that the point is on the edge of the unit circle.
        v.Normalize();

        return Center + v * Radius;
    }
}
