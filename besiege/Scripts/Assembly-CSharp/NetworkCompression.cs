using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class NetworkCompression
{
	public const int POS_SIZE = 6;

	public const int ROT_SIZE = 7;

	public const int VECTOR_SIZE = 6;

	public const int VECTOR3_SIZE = 12;

	public const int QUATERNION_SIZE = 16;

	private static Bounds worldBounds;

	private static float maxRotComponentVal;

	private static float rotEncodeMultiplier;

	private static float rotDecodeMultiplier;

	private static float shortMax;

	private static float Max17Bits;

	private static float Max14Bits;

	private static float halfShortMax;

	public static float wMinX;

	public static float wMinY;

	public static float wMinZ;

	public static float wMaxX;

	public static float wMaxY;

	public static float wMaxZ;

	private static float wDeltaX;

	private static float wDeltaY;

	private static float wDeltaZ;

	private static float wShortMaxX;

	private static float wShortMaxY;

	private static float wShortMaxZ;

	private static float invMax14;

	private static float wMaxShortX;

	private static float wMaxShortY;

	private static float wMaxShortZ;

	private static float invDeltaY;

	private static float wMax17X;

	private static float wMax17Z;

	private static float w17MaxX;

	private static float w17MaxZ;

	private static float[] sectorXData;

	private static float[] sectorYData;

	private static float[] sectorZData;

	private static float[] sectorXRange;

	private static float[] sectorYRange;

	private static float[] sectorZRange;

	private static byte[,,] sectorLookupTable;

	private static int[][] inverseSectorLookupTable;

	private static NetworkWriter posNw;

	private static byte[] posBuffer;

	private static NetworkWriter rotNw;

	private static byte[] rotBuffer;

	private static double log92 = Math.Log(9.0, 2.0);

	private static double ilog92 = 1.0 / log92;

	private static double log2 = Math.Log(2.0);

	public static void SetWorldBounds(Bounds levelBounds)
	{
		worldBounds = levelBounds;
		posBuffer = new byte[12];
		rotBuffer = new byte[16];
		posNw = new NetworkWriter(posBuffer);
		rotNw = new NetworkWriter(rotBuffer);
		shortMax = 65535f;
		Max17Bits = 131071f;
		Max14Bits = 16383f;
		halfShortMax = shortMax / 2f;
		maxRotComponentVal = 1f / Mathf.Sqrt(2f);
		rotEncodeMultiplier = halfShortMax / maxRotComponentVal;
		rotDecodeMultiplier = maxRotComponentVal / halfShortMax;
		wMinX = worldBounds.min.x;
		wMaxX = worldBounds.max.x;
		wMinY = worldBounds.min.y;
		wMaxY = worldBounds.max.y;
		wMinZ = worldBounds.min.z;
		wMaxZ = worldBounds.max.z;
		wDeltaX = wMaxX - wMinX;
		wDeltaY = wMaxY - wMinY;
		wDeltaZ = wMaxZ - wMinZ;
		wDeltaZ = wMaxZ - wMinZ;
		wMaxShortX = shortMax / wDeltaX;
		wMaxShortY = shortMax / wDeltaY;
		wMaxShortZ = shortMax / wDeltaZ;
		wShortMaxX = wDeltaX / shortMax;
		wShortMaxY = wDeltaY / shortMax;
		wShortMaxZ = wDeltaZ / shortMax;
		wMax17X = Max17Bits / wDeltaX;
		wMax17Z = Max17Bits / wDeltaZ;
		w17MaxX = wDeltaX / Max17Bits;
		w17MaxZ = wDeltaZ / Max17Bits;
		invDeltaY = 1f / wDeltaY;
		invMax14 = 1f / Max14Bits;
	}

	public static int PackedUIntLength(int count, bool isShort)
	{
		return isShort ? ((count < 128) ? 1 : 2) : ((count < 64) ? 1 : ((count < 16384) ? 2 : ((count >= 4194304) ? 4 : 3)));
	}

	public static void PackUInt(int count, byte[] buffer, int offset, bool isShort, int countLength)
	{
		int num = (isShort ? 1 : 2);
		buffer[offset] = (byte)((countLength - 1) | (count << num));
		for (int i = 1; i < countLength; i++)
		{
			buffer[offset + i] = (byte)(count >> i * 8 - num);
		}
	}

	public static int UnpackUInt(byte[] buffer, int offset, bool isShort, out int count)
	{
		int num = (buffer[offset] & (isShort ? 1 : 3)) + 1;
		int num2 = (isShort ? 1 : 2);
		count = buffer[offset] >> num2;
		for (int i = 1; i < num; i++)
		{
			count |= buffer[offset + i] << i * 8 - num2;
		}
		return num;
	}

	public static void CompressPosition_old(Vector3 pos, byte[] posBuffer, int offset)
	{
		WriteUInt16((ushort)((pos.x - wMinX) * wMaxShortX), posBuffer, offset);
		WriteUInt16((ushort)((pos.y - wMinY) * wMaxShortY), posBuffer, offset + 2);
		WriteUInt16((ushort)((pos.z - wMinZ) * wMaxShortZ), posBuffer, offset + 4);
	}

	public static void DecompressPosition_old(byte[] data, int offset, out Vector3 vec)
	{
		vec.x = wMinX + wShortMaxX * (float)(int)ReadUInt16(data, offset);
		vec.y = wMinY + wShortMaxY * (float)(int)ReadUInt16(data, offset + 2);
		vec.z = wMinZ + wShortMaxZ * (float)(int)ReadUInt16(data, offset + 4);
	}

	public static void CompressPosition(Vector3 pos, byte[] posBuffer, int offset)
	{
		uint num = (uint)((pos.x - wMinX) * wMax17X);
		uint num2 = (uint)(ApplyLog((pos.y - wMinY) * invDeltaY) * (double)Max14Bits);
		uint num3 = (uint)((pos.z - wMinZ) * wMax17Z);
		WriteUInt16((ushort)(num & 0xFFFF), posBuffer, offset);
		WriteUInt16((ushort)(num3 & 0xFFFF), posBuffer, offset + 2);
		ushort val = (ushort)(((num >> 16) & 1) | ((num2 & 0x3FFF) << 1) | (((num3 >> 16) & 1) << 15));
		WriteUInt16(val, posBuffer, offset + 4);
	}

	public static void DecompressPosition(byte[] data, int offset, out Vector3 vec)
	{
		uint num = ReadUInt16(data, offset);
		uint num2 = ReadUInt16(data, offset + 2);
		ushort num3 = ReadUInt16(data, offset + 4);
		num |= (uint)((num3 & 1) << 16);
		uint num4 = (uint)((num3 >> 1) & 0x3FFF);
		num2 |= (uint)(((num3 >> 15) & 1) << 16);
		vec.x = wMinX + (float)((double)w17MaxX * (double)num);
		vec.y = wMinY + wDeltaY * RevertLog((double)num4 * (double)invMax14);
		vec.z = wMinZ + (float)((double)w17MaxZ * (double)num2);
	}

	private static double ApplyLog(float yNormalized)
	{
		return (Math.Log((double)Mathf.Clamp01(yNormalized) + 0.125, 2.0) + 3.0) * ilog92;
	}

	private static float RevertLog(double compressedY)
	{
		return (float)Math.Exp((compressedY * log92 - 3.0) * log2) - 0.125f;
	}

	public static void CompressRotation(Quaternion rot, byte[] rotBuffer, int offset)
	{
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		float num;
		if (rot.x < 0f)
		{
			num = 0f - rot.x;
			flag = true;
		}
		else
		{
			num = rot.x;
		}
		float num2;
		if (rot.y < 0f)
		{
			num2 = 0f - rot.y;
			flag2 = true;
		}
		else
		{
			num2 = rot.y;
		}
		float num3;
		if (rot.z < 0f)
		{
			num3 = 0f - rot.z;
			flag3 = true;
		}
		else
		{
			num3 = rot.z;
		}
		float num4;
		if (rot.w < 0f)
		{
			num4 = 0f - rot.w;
			flag4 = true;
		}
		else
		{
			num4 = rot.w;
		}
		float num5;
		float num6;
		float num7;
		if (num2 > num && num2 > num3 && num2 > num4)
		{
			rotBuffer[offset] = 0;
			num5 = ((!flag2) ? rot.x : ((!flag) ? (0f - rot.x) : num));
			num6 = ((!flag2) ? rot.z : ((!flag3) ? (0f - rot.z) : num3));
			num7 = ((!flag2) ? rot.w : ((!flag4) ? (0f - rot.w) : num4));
		}
		else if (num4 > num && num4 > num2 && num4 > num3)
		{
			rotBuffer[offset] = 1;
			num5 = ((!flag4) ? rot.x : ((!flag) ? (0f - rot.x) : num));
			num6 = ((!flag4) ? rot.y : ((!flag2) ? (0f - rot.y) : num2));
			num7 = ((!flag4) ? rot.z : ((!flag3) ? (0f - rot.z) : num3));
		}
		else if (num > num2 && num > num3 && num > num4)
		{
			rotBuffer[offset] = 2;
			num5 = ((!flag) ? rot.y : ((!flag2) ? (0f - rot.y) : num2));
			num6 = ((!flag) ? rot.z : ((!flag3) ? (0f - rot.z) : num3));
			num7 = ((!flag) ? rot.w : ((!flag4) ? (0f - rot.w) : num4));
		}
		else
		{
			rotBuffer[offset] = 3;
			num5 = ((!flag3) ? rot.x : ((!flag) ? (0f - rot.x) : num));
			num6 = ((!flag3) ? rot.y : ((!flag2) ? (0f - rot.y) : num2));
			num7 = ((!flag3) ? rot.w : ((!flag4) ? (0f - rot.w) : num4));
		}
		WriteUInt16((ushort)(num5 * rotEncodeMultiplier), rotBuffer, offset + 1);
		WriteUInt16((ushort)(num6 * rotEncodeMultiplier), rotBuffer, offset + 3);
		WriteUInt16((ushort)(num7 * rotEncodeMultiplier), rotBuffer, offset + 5);
	}

	public static void DecompressRotation(byte[] data, int offset, out Quaternion rot)
	{
		int num = data[offset];
		float num2 = (float)BitConverter.ToInt16(data, offset + 1) * rotDecodeMultiplier;
		float num3 = (float)BitConverter.ToInt16(data, offset + 3) * rotDecodeMultiplier;
		float num4 = (float)BitConverter.ToInt16(data, offset + 5) * rotDecodeMultiplier;
		float num5 = Mathf.Sqrt(1f - num2 * num2 - num3 * num3 - num4 * num4);
		switch (num)
		{
		case 0:
			rot.x = num2;
			rot.y = num5;
			rot.z = num3;
			rot.w = num4;
			break;
		case 1:
			rot.x = num2;
			rot.y = num3;
			rot.z = num4;
			rot.w = num5;
			break;
		case 2:
			rot.x = num5;
			rot.y = num2;
			rot.z = num3;
			rot.w = num4;
			break;
		default:
			rot.x = num2;
			rot.y = num3;
			rot.z = num5;
			rot.w = num4;
			break;
		}
	}

	public static void CompressVector(Vector3 vec, float min, float max, byte[] vecBuffer, int offset)
	{
		float num = shortMax / (max - min);
		ushort val = (ushort)((vec.x - min) * num);
		ushort val2 = (ushort)((vec.y - min) * num);
		ushort val3 = (ushort)((vec.z - min) * num);
		WriteUInt16(val, vecBuffer, offset);
		WriteUInt16(val2, vecBuffer, offset + 2);
		WriteUInt16(val3, vecBuffer, offset + 4);
	}

	public static void DecompressVector(byte[] data, int offset, float min, float max, out Vector3 vec)
	{
		float num = (max - min) / shortMax;
		vec.x = (float)(int)ReadUInt16(data, offset) * num + min;
		vec.y = (float)(int)ReadUInt16(data, offset + 2) * num + min;
		vec.z = (float)(int)ReadUInt16(data, offset + 4) * num + min;
	}

	public static uint ReadUInt(bool isShort, byte[] buffer, int offset)
	{
		return (!isShort) ? BitConverter.ToUInt32(buffer, offset) : ((uint)(buffer[offset] + (buffer[offset + 1] << 8)));
	}

	public static ushort ReadUInt16(byte[] buffer, int offset)
	{
		return (ushort)(buffer[offset] + (buffer[offset + 1] << 8));
	}

	public static void WriteUInt(uint val, bool isShort, byte[] buffer, int offset)
	{
		buffer[offset] = (byte)(val & 0xFF);
		buffer[offset + 1] = (byte)(val >> 8);
		if (!isShort)
		{
			buffer[offset + 2] = (byte)(val >> 16);
			buffer[offset + 3] = (byte)(val >> 24);
		}
	}

	public static void WriteUInt16(ushort val, byte[] buffer, int offset)
	{
		buffer[offset] = (byte)(val & 0xFF);
		buffer[offset + 1] = (byte)(val >> 8);
	}

	public static byte[] PackVector(Vector3 pos)
	{
		byte[] array = new byte[12];
		PackVector(pos, array, 0);
		return array;
	}

	public static void PackVector(Vector3 pos, byte[] buffer, int offset)
	{
		posNw.SeekZero();
		posNw.Write(pos);
		Buffer.BlockCopy(posNw.AsArray(), 0, buffer, offset, 12);
	}

	public static void UnpackVector(byte[] data, int offset, out Vector3 vec)
	{
		float[] array = new float[3];
		Buffer.BlockCopy(data, offset, array, 0, 12);
		vec.x = array[0];
		vec.y = array[1];
		vec.z = array[2];
	}

	public static byte[] PackQuaternion(Quaternion rot)
	{
		byte[] array = new byte[16];
		PackQuaternion(rot, array, 0);
		return array;
	}

	public static void PackQuaternion(Quaternion rot, byte[] buffer, int offset)
	{
		rotNw.SeekZero();
		rotNw.Write(rot);
		Buffer.BlockCopy(rotNw.AsArray(), 0, buffer, offset, 16);
	}

	public static void UnpackQuaternion(byte[] data, int offset, out Quaternion quat)
	{
		float[] array = new float[4];
		Buffer.BlockCopy(data, offset, array, 0, 16);
		quat.x = array[0];
		quat.y = array[1];
		quat.z = array[2];
		quat.w = array[3];
	}

	public static void WriteArray(List<byte[]> byteList, byte[] buffer, int offset)
	{
		for (int i = 0; i < byteList.Count; i++)
		{
			byte[] array = byteList[i];
			Buffer.BlockCopy(array, 0, buffer, offset, array.Length);
			offset += array.Length;
		}
	}

	public static void WriteArray(byte[][] byteList, byte[] buffer, int offset)
	{
		foreach (byte[] array in byteList)
		{
			Buffer.BlockCopy(array, 0, buffer, offset, array.Length);
			offset += array.Length;
		}
	}

	public static byte[] CombineArray(List<byte[]> byteList, int totalLength)
	{
		byte[] array = new byte[totalLength];
		WriteArray(byteList, array, 0);
		return array;
	}

	public static byte[] CombineArray(List<byte[]> byteList)
	{
		int num = 0;
		for (int i = 0; i < byteList.Count; i++)
		{
			byte[] array = byteList[i];
			num += array.Length;
		}
		return CombineArray(byteList, num);
	}

	public static byte[] Combine(byte[] entryA, byte[] entryB)
	{
		byte[] array = new byte[entryA.Length + entryB.Length];
		Buffer.BlockCopy(entryA, 0, array, 0, entryA.Length);
		Buffer.BlockCopy(entryB, 0, array, entryA.Length, entryB.Length);
		return array;
	}
}
