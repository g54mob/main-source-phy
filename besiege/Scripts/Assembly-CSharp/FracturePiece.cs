using System;
using UnityEngine;

[Serializable]
public class FracturePiece
{
	[Serializable]
	public class Particle
	{
		public Mesh Mesh;

		public bool Sticky;

		public Side Side;
	}

	public enum Side
	{
		Bottom = 0,
		Right = 1,
		Top = 2,
		Left = 3
	}

	public Mesh MainPiece;

	public Particle[] FractureParticles;

	public static int SideToEdgeIndexTri(Side s)
	{
		switch (s)
		{
		case Side.Left:
			return 0;
		case Side.Bottom:
			return 1;
		case Side.Right:
			return 2;
		default:
			throw new ArgumentException("Not a valid side for a triangle: " + s);
		}
	}
}
