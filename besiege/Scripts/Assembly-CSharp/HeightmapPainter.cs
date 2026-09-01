using System.Collections.Generic;
using UnityEngine;

public class HeightmapPainter
{
	public enum TerrainTool
	{
		PaintHeight = 0,
		SetHeight = 1,
		SmoothHeight = 2,
		StampHeight = 3
	}

	public int brushSize;

	public float strength;

	public float targetHeight;

	public TerrainTool tool;

	public Brush brush;

	public TerrainData terrainData;

	public ModifierBlendMode blendMode;

	public Texture2D stampTexture;

	public Texture2D heightmapStampTexture;

	private float[,] cachedHeightmap;

	private Dictionary<int, float[,]> heightsSpanCache;

	public HeightmapPainter(TerrainTool tool, Brush brush, TerrainData terrainData)
	{
		this.tool = tool;
		this.brush = brush;
		this.terrainData = terrainData;
		Initialize();
	}

	private void Initialize()
	{
		cachedHeightmap = new float[terrainData.heightmapHeight, terrainData.heightmapWidth];
		heightsSpanCache = new Dictionary<int, float[,]>();
	}

	public void PreparePainter()
	{
		ResetCachedHeightmap();
	}

	public void StampBrushHeight(float xCenterNormalized, float yCenterNormalized, AnimationCurve brushCurve)
	{
		int num;
		int num2;
		if (brushSize % 2 == 0)
		{
			num = Mathf.CeilToInt(xCenterNormalized * (float)(terrainData.heightmapWidth - 1));
			num2 = Mathf.CeilToInt(yCenterNormalized * (float)(terrainData.heightmapHeight - 1));
		}
		else
		{
			num = Mathf.RoundToInt(xCenterNormalized * (float)(terrainData.heightmapWidth - 1));
			num2 = Mathf.RoundToInt(yCenterNormalized * (float)(terrainData.heightmapHeight - 1));
		}
		int num3 = Mathf.CeilToInt((float)brushSize / 2f);
		int num4 = brushSize % 2;
		int num5 = Mathf.Clamp(num - num3, 0, terrainData.heightmapWidth - 1);
		int num6 = Mathf.Clamp(num2 - num3, 0, terrainData.heightmapHeight - 1);
		int num7 = Mathf.Clamp(num + num3 + num4, 0, terrainData.heightmapWidth);
		int num8 = Mathf.Clamp(num2 + num3 + num4, 0, terrainData.heightmapHeight);
		int num9 = num7 - num5;
		int num10 = num8 - num6;
		float[,] orCreateSpan = GetOrCreateSpan(num5, num6, num9, num10);
		Vector2 vector = new Vector2((float)num9 / 2f, (float)num10 / 2f);
		float sqrMagnitude = vector.sqrMagnitude;
		for (int i = 0; i < num10; i++)
		{
			for (int j = 0; j < num9; j++)
			{
				float strengthInt = brush.GetStrengthInt(num5 + j - (num - num3), num6 + i - (num2 - num3));
				float num11 = vector.x - (float)j;
				float num12 = vector.y - (float)i;
				float num13 = 1f - (num11 * num11 + num12 * num12) / sqrMagnitude;
				float time = num13 * strengthInt;
				float brushStrength = brushCurve.Evaluate(time) * strength;
				float num14 = ApplyBrush(orCreateSpan[i, j], brushStrength, j + num5, i + num6);
				orCreateSpan[i, j] = num14;
			}
		}
		SetHeights(num5, num6, num9, num10, orCreateSpan);
		terrainData.SetHeightsDelayLOD(num5, num6, orCreateSpan);
	}

	public void PaintIslandStamp(float xCenterNormalized, float yCenterNormalized)
	{
		int num;
		int num2;
		if (brushSize % 2 == 0)
		{
			num = Mathf.CeilToInt(xCenterNormalized * (float)(terrainData.heightmapWidth - 1));
			num2 = Mathf.CeilToInt(yCenterNormalized * (float)(terrainData.heightmapHeight - 1));
		}
		else
		{
			num = Mathf.RoundToInt(xCenterNormalized * (float)(terrainData.heightmapWidth - 1));
			num2 = Mathf.RoundToInt(yCenterNormalized * (float)(terrainData.heightmapHeight - 1));
		}
		int num3 = brushSize / 2;
		int num4 = brushSize % 2;
		int num5 = Mathf.Clamp(num - num3, 0, terrainData.heightmapWidth - 1);
		int num6 = Mathf.Clamp(num2 - num3, 0, terrainData.heightmapHeight - 1);
		int num7 = Mathf.Clamp(num + num3 + num4, 0, terrainData.heightmapWidth);
		int num8 = Mathf.Clamp(num2 + num3 + num4, 0, terrainData.heightmapHeight);
		int num9 = num7 - num5;
		int num10 = num8 - num6;
		float[,] orCreateSpan = GetOrCreateSpan(num5, num6, num9, num10);
		for (int i = 0; i < num10; i++)
		{
			for (int j = 0; j < num9; j++)
			{
				float strengthInt = brush.GetStrengthInt(num5 + j - (num - num3), num6 + i - (num2 - num3));
				float num11 = ApplyBrush(orCreateSpan[i, j], strengthInt * strength, j + num5, i + num6);
				orCreateSpan[i, j] = num11;
			}
		}
		SetHeights(num5, num6, num9, num10, orCreateSpan);
		terrainData.SetHeightsDelayLOD(num5, num6, orCreateSpan);
	}

	public void ClearArea(Vector2 uv, int brushSize)
	{
		int num;
		int num2;
		if (brushSize % 2 == 0)
		{
			num = Mathf.CeilToInt(uv.x * (float)(terrainData.heightmapWidth - 1));
			num2 = Mathf.CeilToInt(uv.y * (float)(terrainData.heightmapHeight - 1));
		}
		else
		{
			num = Mathf.RoundToInt(uv.x * (float)(terrainData.heightmapWidth - 1));
			num2 = Mathf.RoundToInt(uv.y * (float)(terrainData.heightmapHeight - 1));
		}
		int num3 = Mathf.CeilToInt((float)brushSize / 2f);
		int num4 = brushSize % 2;
		int num5 = Mathf.Clamp(num - num3, 0, terrainData.heightmapWidth - 1);
		int num6 = Mathf.Clamp(num2 - num3, 0, terrainData.heightmapHeight - 1);
		int num7 = Mathf.Clamp(num + num3 + num4, 0, terrainData.heightmapWidth);
		int num8 = Mathf.Clamp(num2 + num3 + num4, 0, terrainData.heightmapHeight);
		int width = num7 - num5;
		int height = num8 - num6;
		float[,] orCreateSpan = GetOrCreateSpan(num5, num6, width, height);
		terrainData.SetHeightsDelayLOD(num5, num6, orCreateSpan);
	}

	private float Smooth(int x, int y)
	{
		float num = 0f;
		float num2 = 1f / terrainData.size.y;
		return (num + terrainData.GetHeight(x, y) * num2 + terrainData.GetHeight(x + 1, y) * num2 + terrainData.GetHeight(x - 1, y) * num2 + (float)((double)terrainData.GetHeight(x + 1, y + 1) * (double)num2 * 0.75) + (float)((double)terrainData.GetHeight(x - 1, y + 1) * (double)num2 * 0.75) + (float)((double)terrainData.GetHeight(x + 1, y - 1) * (double)num2 * 0.75) + (float)((double)terrainData.GetHeight(x - 1, y - 1) * (double)num2 * 0.75) + terrainData.GetHeight(x, y + 1) * num2 + terrainData.GetHeight(x, y - 1) * num2) / 8f;
	}

	private float ApplyBrush(float height, float brushStrength, int x, int y)
	{
		if (tool == TerrainTool.PaintHeight)
		{
			return height + brushStrength;
		}
		if (tool == TerrainTool.SetHeight)
		{
			if (targetHeight > height)
			{
				height += brushStrength;
				height = Mathf.Min(height, targetHeight);
				return height;
			}
			height -= brushStrength;
			height = Mathf.Max(height, targetHeight);
			return height;
		}
		if (tool == TerrainTool.StampHeight)
		{
			float num = targetHeight * brushStrength;
			if (blendMode == ModifierBlendMode.Additive)
			{
				if (height < num)
				{
					return num;
				}
			}
			else if (blendMode == ModifierBlendMode.Subtractive)
			{
				float num2 = 1f;
				float num3 = targetHeight - num2;
				float num4 = num2 + num3 * brushStrength;
				float num5 = (height + num4) / 2f;
				if (num5 < height)
				{
					return num5;
				}
			}
			else if (blendMode == ModifierBlendMode.Smooth)
			{
				float num6 = (height + num) / 2f;
				if (height < num6)
				{
					return num6;
				}
				return height;
			}
		}
		return (tool != TerrainTool.SmoothHeight) ? height : Mathf.Lerp(height, Smooth(x, y), brushStrength);
	}

	private void ResetCachedHeightmap()
	{
		int heightmapHeight = terrainData.heightmapHeight;
		int heightmapWidth = terrainData.heightmapWidth;
		for (int i = 0; i < heightmapHeight; i++)
		{
			for (int j = 0; j < heightmapWidth; j++)
			{
				cachedHeightmap[i, j] = 0f;
			}
		}
	}

	private float[,] GetOrCreateSpan(int xBase, int yBase, int width, int height)
	{
		int key = width * height;
		float[,] value;
		if (!heightsSpanCache.TryGetValue(key, out value))
		{
			value = new float[height, width];
			heightsSpanCache.Add(key, value);
		}
		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				value[i, j] = cachedHeightmap[yBase + i, xBase + j];
			}
		}
		return value;
	}

	private void SetHeights(int xBase, int yBase, int width, int height, float[,] span)
	{
		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				cachedHeightmap[yBase + i, xBase + j] = span[i, j];
			}
		}
	}
}
