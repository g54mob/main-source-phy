using System;
using System.Globalization;

namespace CloudinaryDotNet.Core
{
	public struct Rectangle : IEquatable<Rectangle>
	{
		public int Height { get; set; }

		public int Width { get; set; }

		public int X { get; set; }

		public int Y { get; set; }

		public Rectangle(int x, int y, int width, int height)
		{
			this = default(Rectangle);
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		public static bool operator ==(Rectangle left, Rectangle right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Rectangle left, Rectangle right)
		{
			return !left.Equals(right);
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{{X={0}, Y={1}, Width={2}, Height={3}}}", X, Y, Width, Height);
		}

		public bool Equals(Rectangle other)
		{
			if (Height == other.Height && Width == other.Width && X == other.X)
			{
				return Y == other.Y;
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is Rectangle other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (((((Height * 397) ^ Width) * 397) ^ X) * 397) ^ Y;
		}
	}
}
