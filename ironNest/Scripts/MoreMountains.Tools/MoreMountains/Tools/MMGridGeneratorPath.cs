using System;
using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMGridGeneratorPath : MMGridGenerator
	{
		public enum Directions
		{
			TopToBottom = 0,
			BottomToTop = 1,
			LeftToRight = 2,
			RightToLeft = 3
		}

		public static int[,] Generate(int width, int height, int seed, Directions direction, Vector2Int startPosition, int pathMinWidth, int pathMaxWidth, int directionChangeDistance, int widthChangePercentage, int directionChangePercentage)
		{
			return null;
		}

		private static int ComputeWidth(System.Random random, int widthChangePercentage, int pathMinWidth, int pathMaxWidth, int pathWidth)
		{
			return 0;
		}

		private static int DetermineNextStep(System.Random random, int x, int directionChangeDistance, int directionChangePercentage, int pathMaxWidth, int width)
		{
			return 0;
		}
	}
}
