using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace MonoGameLibrary;

public readonly struct RectangleBorder : IEquatable<RectangleBorder>
{
    private static readonly RectangleBorder s_empty = new RectangleBorder();

    public readonly int X;
    public readonly int Y;
    public readonly int Width;
    public readonly int Height;

    public readonly Point Location => new Point(X, Y);

    public static RectangleBorder Empty => s_empty;

    public readonly bool IsEmpty => X == 0 && Y == 0 && Width == 0 && Height == 0;

    public readonly int Top => Y;
    public readonly int Bottom => Y + Height;
    public readonly int Left => X;
    public readonly int Right => X + Width;

    public RectangleBorder(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public RectangleBorder(Point location, Point size)
    {
        X = location.X;
        Y = location.Y;
        Width = size.X;
        Height = size.Y;
    }

    public bool Intersects(RectangleBorder other)
    {
        return !(other.Left > this.Right || other.Right < this.Left || other.Top > this.Bottom || other.Bottom < this.Top);
    }

    public bool Contains(Point point)
    {
        return point.X >= this.Left && point.X <= this.Right && point.Y >= this.Top && point.Y <= this.Bottom;
    }  

    public override readonly bool Equals(object obj) => obj is RectangleBorder other && Equals(other);

    public readonly bool Equals(RectangleBorder other) => this.X == other.X && this.Y == other.Y && this.Width == other.Width && this.Height == other.Height;

    public override readonly int GetHashCode() => HashCode.Combine(X, Y, Width, Height);

    public static bool operator ==(RectangleBorder lhs, RectangleBorder rhs) => lhs.Equals(rhs);

    public static bool operator !=(RectangleBorder lhs, RectangleBorder rhs) => !lhs.Equals(rhs);

    public bool Intersects(Rectangle rect)
    {
        return !(rect.Left > this.Right ||
                 rect.Right < this.Left ||
                 rect.Top > this.Bottom ||
                 rect.Bottom < this.Top);
    }

}

