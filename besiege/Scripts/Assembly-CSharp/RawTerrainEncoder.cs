using System;
using System.IO;
using UnityEngine;

public class RawTerrainEncoder
{
	private enum Depth
	{
		Bit8 = 1,
		Bit16 = 2
	}

	private enum ByteOrder
	{
		Mac = 1,
		Windows = 2
	}

	private Depth m_Depth = Depth.Bit16;

	private int m_Width = 1;

	private int m_Height = 1;

	private ByteOrder m_ByteOrder = ByteOrder.Windows;

	private bool m_FlipVertically;

	private Vector3 m_TerrainSize = new Vector3(2000f, 600f, 2000f);

	private Terrain m_Terrain;

	private string m_Path;

	private TerrainData terrainData
	{
		get
		{
			return (!(m_Terrain != null)) ? null : m_Terrain.terrainData;
		}
	}

	public void DecodeRawTerrain(Terrain terrain, string path)
	{
		m_Path = path;
		m_Terrain = terrain;
		if (terrain == null)
		{
			Debug.LogError("Terrain does not exist.");
			return;
		}
		if (m_Width > 4097 || m_Height > 4097)
		{
			Debug.LogError("Heightmaps above 4097x4097 in resolution are not supported.");
			return;
		}
		if (!File.Exists(m_Path))
		{
			Debug.LogError("Could not find raw terrain at path: " + m_Path);
			return;
		}
		m_Terrain = terrain;
		m_Path = path;
		PickRawDefaults(m_Path);
		terrainData.heightmapResolution = Mathf.Max(m_Width, m_Height);
		terrainData.size = m_TerrainSize;
		ImportRaw(m_Path);
		FlushHeightmapModification();
	}

	public float[,] DecodeTerrain(byte[] data)
	{
		PickRawDefaults(data.Length);
		return ReadRaw(data);
	}

	private void PickRawDefaults(string path)
	{
		FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.Read);
		int length = (int)fileStream.Length;
		fileStream.Close();
		PickRawDefaults(length);
	}

	private void PickRawDefaults(int length)
	{
		m_Depth = Depth.Bit16;
		int num = length / (int)m_Depth;
		int num2 = Mathf.RoundToInt(Mathf.Sqrt(num));
		int num3 = Mathf.RoundToInt(Mathf.Sqrt(num));
		if (num2 * num3 * (int)m_Depth == length)
		{
			m_Width = num2;
			m_Height = num3;
			return;
		}
		m_Depth = Depth.Bit8;
		int num4 = length / (int)m_Depth;
		int num5 = Mathf.RoundToInt(Mathf.Sqrt(num4));
		int num6 = Mathf.RoundToInt(Mathf.Sqrt(num4));
		if (num5 * num6 * (int)m_Depth == length)
		{
			m_Width = num5;
			m_Height = num6;
		}
		else
		{
			m_Depth = Depth.Bit16;
		}
	}

	private void ImportRaw(string path)
	{
		byte[] array;
		using (BinaryReader binaryReader = new BinaryReader(File.Open(path, FileMode.Open, FileAccess.Read)))
		{
			array = binaryReader.ReadBytes(m_Width * m_Height * (int)m_Depth);
			binaryReader.Close();
		}
		int heightmapWidth = terrainData.heightmapWidth;
		int heightmapHeight = terrainData.heightmapHeight;
		float[,] array2 = new float[heightmapHeight, heightmapWidth];
		if (m_Depth == Depth.Bit16)
		{
			float num = 1.525879E-05f;
			for (int i = 0; i < heightmapHeight; i++)
			{
				for (int j = 0; j < heightmapWidth; j++)
				{
					int num2 = Mathf.Clamp(j, 0, m_Width - 1) + Mathf.Clamp(i, 0, m_Height - 1) * m_Width;
					if (m_ByteOrder == ByteOrder.Mac == BitConverter.IsLittleEndian)
					{
						byte b = array[num2 * 2];
						array[num2 * 2] = array[num2 * 2 + 1];
						array[num2 * 2 + 1] = b;
					}
					float num3 = (float)(int)BitConverter.ToUInt16(array, num2 * 2) * num;
					int num4 = (m_FlipVertically ? (heightmapHeight - 1 - i) : i);
					array2[num4, j] = num3;
				}
			}
		}
		else
		{
			float num5 = 0.00390625f;
			for (int k = 0; k < heightmapHeight; k++)
			{
				for (int l = 0; l < heightmapWidth; l++)
				{
					int num6 = Mathf.Clamp(l, 0, m_Width - 1) + Mathf.Clamp(k, 0, m_Height - 1) * m_Width;
					float num7 = (float)(int)array[num6] * num5;
					int num8 = (m_FlipVertically ? (heightmapHeight - 1 - k) : k);
					array2[num8, l] = num7;
				}
			}
		}
		terrainData.SetHeights(0, 0, array2);
	}

	private float[,] ReadRaw(byte[] numArray)
	{
		float[,] array = new float[m_Height, m_Width];
		if (m_Depth == Depth.Bit16)
		{
			float num = 1.525879E-05f;
			for (int i = 0; i < m_Height; i++)
			{
				for (int j = 0; j < m_Width; j++)
				{
					int num2 = Mathf.Clamp(j, 0, m_Width - 1) + Mathf.Clamp(i, 0, m_Height - 1) * m_Width;
					if (m_ByteOrder == ByteOrder.Mac == BitConverter.IsLittleEndian)
					{
						byte b = numArray[num2 * 2];
						numArray[num2 * 2] = numArray[num2 * 2 + 1];
						numArray[num2 * 2 + 1] = b;
					}
					float num3 = (float)(int)BitConverter.ToUInt16(numArray, num2 * 2) * num;
					int num4 = (m_FlipVertically ? (m_Height - 1 - i) : i);
					array[num4, j] = num3;
				}
			}
		}
		else
		{
			float num5 = 0.00390625f;
			for (int k = 0; k < m_Height; k++)
			{
				for (int l = 0; l < m_Width; l++)
				{
					int num6 = Mathf.Clamp(l, 0, m_Width - 1) + Mathf.Clamp(k, 0, m_Height - 1) * m_Width;
					float num7 = (float)(int)numArray[num6] * num5;
					int num8 = (m_FlipVertically ? (m_Height - 1 - k) : k);
					array[num8, l] = num7;
				}
			}
		}
		return array;
	}

	private void FlushHeightmapModification()
	{
		m_Terrain.Flush();
	}
}
