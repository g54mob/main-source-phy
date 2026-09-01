using System;
using UnityEngine;
using Vectrosity;

public class WaterLineMaker
{
	public static float THICKNESS = 8f;

	public static float AMPLITUDE = 0.03f;

	public static float PERIOD = 1.5f;

	public static float X_INTERVAL = 0.1f;

	public static void Generate(VectorLine line, float startX, float endX, float height)
	{
		line.points3.Clear();
		int num = 0;
		for (float num2 = startX; num2 < endX; num2 += X_INTERVAL)
		{
			Vector3 vector = new Vector3(num2, height + AMPLITUDE * Mathf.Sin(MathF.PI * 2f / PERIOD * num2), 0f);
			if (num >= line.points3.Count)
			{
				line.points3.Add(vector);
			}
			else
			{
				line.points3[num] = vector;
			}
			num++;
		}
		Vector3 vector2 = new Vector3(endX, height + AMPLITUDE * Mathf.Sin(MathF.PI * 2f / PERIOD * endX), 0f);
		if (num >= line.points3.Count)
		{
			line.points3.Add(vector2);
		}
		else
		{
			line.points3[num] = vector2;
		}
	}

	public static void GenerateSimple(VectorLine line, float startX, float endX, float height)
	{
		line.points3.Clear();
		line.points3.Add(new Vector3(startX, height, 0f));
		line.points3.Add(new Vector3(endX, height, 0f));
	}
}
