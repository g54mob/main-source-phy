using System;
using UnityEngine;

[Serializable]
public class Brush
{
	internal const int kMinBrushSize = 3;

	private float[] m_Strength;

	private float m_HeightMultiplier = 1f;

	private int m_Size;

	private Texture2D m_Brush;

	private Texture2D m_Preview;

	public float HeightMultiplier
	{
		get
		{
			return m_HeightMultiplier;
		}
	}

	public Texture2D BrushTexture
	{
		get
		{
			return m_Brush;
		}
	}

	public Texture2D PreviewTexture
	{
		get
		{
			return m_Preview;
		}
	}

	public bool Load(Texture2D brushTex, int size)
	{
		if (m_Brush == brushTex && size == m_Size && m_Strength != null)
		{
			return true;
		}
		if (brushTex != null)
		{
			float num = size;
			m_Size = size;
			m_Strength = new float[m_Size * m_Size];
			if (m_Size > 3)
			{
				for (int i = 0; i < m_Size; i++)
				{
					for (int j = 0; j < m_Size; j++)
					{
						m_Strength[i * m_Size + j] = brushTex.GetPixelBilinear(((float)j + 0.5f) / num, (float)i / num).a;
					}
				}
			}
			else
			{
				for (int k = 0; k < m_Strength.Length; k++)
				{
					m_Strength[k] = 1f;
				}
			}
			UnityEngine.Object.DestroyImmediate(m_Preview);
			m_Preview = new Texture2D(m_Size, m_Size, TextureFormat.ARGB32, false);
			m_Preview.hideFlags = HideFlags.HideAndDontSave;
			m_Preview.wrapMode = TextureWrapMode.Repeat;
			m_Preview.filterMode = FilterMode.Point;
			Color[] array = new Color[m_Size * m_Size];
			for (int l = 0; l < array.Length; l++)
			{
				array[l] = new Color(1f, 1f, 1f, m_Strength[l]);
			}
			m_Preview.SetPixels(0, 0, m_Size, m_Size, array, 0);
			m_Preview.Apply();
			m_Brush = brushTex;
			return true;
		}
		m_Strength = new float[1];
		m_Strength[0] = 1f;
		m_Size = 1;
		return false;
	}

	public Brush ResizeBrush(int brushSize, float scaleMultiplier)
	{
		Brush brush = new Brush();
		float[,] array = new float[m_Size, m_Size];
		for (int i = 0; i < m_Size; i++)
		{
			for (int j = 0; j < m_Size; j++)
			{
				float num = m_Strength[i * m_Size + j] / m_HeightMultiplier;
				array[i, j] = num;
			}
		}
		brush.LoadFromRaw(array, brushSize, scaleMultiplier);
		return brush;
	}

	public static float[,] BilinearResize(float[,] image, int height, int width)
	{
		int num = image.GetLength(0) - 1;
		int num2 = image.GetLength(1) - 1;
		float[,] array = new float[height, width];
		float num3 = (float)(num2 - 1) / (float)(width - 1);
		float num4 = (float)(num - 1) / (float)(height - 1);
		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				int num5 = (int)Math.Floor(num3 * (float)j);
				int num6 = (int)Math.Floor(num4 * (float)i);
				int num7 = (int)Math.Ceiling(num3 * (float)j);
				int num8 = (int)Math.Ceiling(num4 * (float)i);
				float num9 = num3 * (float)j - (float)num5;
				float num10 = num4 * (float)i - (float)num6;
				float num11 = image[num6, num5];
				float num12 = image[num6, num7];
				float num13 = image[num8, num5];
				float num14 = image[num8, num7];
				float num15 = num11 * (1f - num9) * (1f - num10) + num12 * num9 * (1f - num10) + num13 * num10 * (1f - num9) + num14 * num9 * num10;
				array[i, j] = num15;
			}
		}
		return array;
	}

	private void ScaleHeightmap(ref float[,] heightmap, float multiplier)
	{
		int length = heightmap.GetLength(1);
		int length2 = heightmap.GetLength(0);
		for (int i = 0; i < length2; i++)
		{
			for (int j = 0; j < length; j++)
			{
				heightmap[i, j] *= multiplier;
			}
		}
	}

	public void LoadFromRaw(float[,] rawHeightmap, int size, float heightMultiplier)
	{
		m_HeightMultiplier = heightMultiplier;
		m_Size = size;
		m_Strength = new float[m_Size * m_Size];
		ScaleHeightmap(ref rawHeightmap, heightMultiplier);
		float[,] array = BilinearResize(rawHeightmap, size, size);
		for (int i = 0; i < m_Size; i++)
		{
			for (int j = 0; j < m_Size; j++)
			{
				float num = array[i, j];
				m_Strength[i * m_Size + j] = num;
			}
		}
		UnityEngine.Object.DestroyImmediate(m_Preview);
		m_Preview = new Texture2D(m_Size, m_Size, TextureFormat.ARGB32, false);
		m_Preview.hideFlags = HideFlags.HideAndDontSave;
		m_Preview.wrapMode = TextureWrapMode.Repeat;
		m_Preview.filterMode = FilterMode.Point;
		Color[] array2 = new Color[m_Size * m_Size];
		for (int k = 0; k < array2.Length; k++)
		{
			array2[k] = new Color(1f, 1f, 1f, m_Strength[k]);
		}
		m_Preview.SetPixels(0, 0, m_Size, m_Size, array2, 0);
		m_Preview.Apply();
	}

	public float GetStrengthInt(int ix, int iy)
	{
		if (ix < 0 || m_Size <= ix || iy < 0 || m_Size <= iy)
		{
			return 0f;
		}
		return m_Strength[iy * m_Size + ix];
	}

	public void Dispose()
	{
		UnityEngine.Object.DestroyImmediate(m_Preview);
		m_Preview = null;
	}
}
