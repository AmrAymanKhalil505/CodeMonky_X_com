using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GridPosition : IEquatable<GridPosition>
{
    public int X;
    public int Z;

    public GridPosition(int X, int Z)
    {
        this.X = X;
        this.Z = Z;
    }

    public override bool Equals(object obj)
    {
        return obj is GridPosition position&&
               X==position.X&&
               Z==position.Z;
    }

    public bool Equals(GridPosition other)
    {
       return this == other;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Z);
    }

    public override string ToString()
    {
        return "X: "+ X + ", Z: "+Z;
    }

    public static bool operator == (GridPosition left, GridPosition right)
    {
        return left.X == right.X && left.Z == right.Z;
    }
    public static bool operator != (GridPosition left, GridPosition right)
    {
        return !(left == right);
    }
    public static GridPosition operator + (GridPosition left, GridPosition right)
    {
        return new GridPosition(left.X + right.X ,left.Z + right.Z);
    }
    public static GridPosition operator -(GridPosition left, GridPosition right)
    {
        return new GridPosition(left.X - right.X, left.Z - right.Z);
    }
}
