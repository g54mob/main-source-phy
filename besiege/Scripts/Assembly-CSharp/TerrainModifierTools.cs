using UnityEngine;

public static class TerrainModifierTools
{
	public static Ray GetDownwardsProjectionRay(Vector3 position, float terrainMaxYPosition)
	{
		Vector3 origin = new Vector3(position.x, terrainMaxYPosition + 1f, position.z);
		return new Ray(origin, Vector3.down);
	}

	public static Brush CreateDefaultBrush(int size)
	{
		Brush brush = new Brush();
		Texture2D brushTex = CreateWhiteCircleTexture(size);
		brush.Load(brushTex, size);
		return brush;
	}

	private static Texture2D CreateWhiteCircleTexture(int size)
	{
		Texture2D texture2D = new Texture2D(size, size, TextureFormat.ARGB32, false);
		float num = (float)size * 0.8f;
		int num2 = Mathf.FloorToInt(num / 2f);
		float num3 = num2 * num2;
		int num4 = Mathf.FloorToInt((float)size / 2f);
		int num5 = num4;
		int num6 = num4;
		for (int i = num5 - num2; i < num5 + num2 + 1; i++)
		{
			for (int j = num6 - num2; j < num6 + num2 + 1; j++)
			{
				if ((float)((num5 - i) * (num5 - i) + (num6 - j) * (num6 - j)) < num3)
				{
					texture2D.SetPixel(i, j, Color.white);
				}
			}
		}
		texture2D.Apply();
		return texture2D;
	}

	private static void WobbleStuff(float[,] heights, TerrainData terrain)
	{
		for (int i = 0; i < heights.GetLength(0); i++)
		{
			for (int j = 0; j < heights.GetLength(1); j++)
			{
				heights[i, j] = (float)(((double)heights[i, j] + 1.0) / 2.0);
			}
		}
	}

	private static void Noise(float[,] heights, TerrainData terrain)
	{
		for (int i = 0; i < heights.GetLength(0); i++)
		{
			for (int j = 0; j < heights.GetLength(1); j++)
			{
				heights[i, j] += Random.value * 0.01f;
			}
		}
	}

	public static void Smooth(float[,] heights, TerrainData terrain)
	{
		float[,] array = heights.Clone() as float[,];
		int length = heights.GetLength(1);
		int length2 = heights.GetLength(0);
		for (int i = 1; i < length2 - 1; i++)
		{
			for (int j = 1; j < length - 1; j++)
			{
				float num = (0f + array[i, j] + array[i, j - 1] + array[i, j + 1] + array[i - 1, j] + array[i + 1, j]) / 5f;
				heights[i, j] = num;
			}
		}
	}

	public static void Smooth(TerrainData terrain)
	{
		int heightmapWidth = terrain.heightmapWidth;
		int heightmapHeight = terrain.heightmapHeight;
		float[,] heights = terrain.GetHeights(0, 0, heightmapWidth, heightmapHeight);
		Smooth(heights, terrain);
		terrain.SetHeights(0, 0, heights);
	}

	public static void Flatten(TerrainData terrain, float height)
	{
		int heightmapWidth = terrain.heightmapWidth;
		int heightmapHeight = terrain.heightmapHeight;
		float[,] array = new float[heightmapHeight, heightmapWidth];
		for (int i = 0; i < heightmapHeight; i++)
		{
			for (int j = 0; j < heightmapWidth; j++)
			{
				array[i, j] = height;
			}
		}
		terrain.SetHeightsDelayLOD(0, 0, array);
	}
}
