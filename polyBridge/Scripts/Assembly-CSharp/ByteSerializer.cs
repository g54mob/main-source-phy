using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ByteSerializer
{
	public static byte SerializeByte(byte value)
	{
		return value;
	}

	public static byte DeserializeByte(byte[] bytes, ref int offset)
	{
		byte result = bytes[offset];
		offset++;
		return result;
	}

	public static byte[] SerializeInt(int value)
	{
		return BitConverter.GetBytes(value);
	}

	public static int DeserializeInt(byte[] bytes, ref int offset)
	{
		int result = BitConverter.ToInt32(bytes, offset);
		offset += 4;
		return result;
	}

	public static byte[] SerializeUInt(uint value)
	{
		return BitConverter.GetBytes(value);
	}

	public static uint DeserializeUInt(byte[] bytes, ref int offset)
	{
		uint result = BitConverter.ToUInt32(bytes, offset);
		offset += 4;
		return result;
	}

	public static byte[] SerializeFloat(float value)
	{
		return BitConverter.GetBytes(value);
	}

	public static float DeserializeFloat(byte[] bytes, ref int offset)
	{
		float result = BitConverter.ToSingle(bytes, offset);
		offset += 4;
		return result;
	}

	public static byte[] SerializeBool(bool value)
	{
		return BitConverter.GetBytes(value);
	}

	public static bool DeserializeBool(byte[] bytes, ref int offset)
	{
		bool result = BitConverter.ToBoolean(bytes, offset);
		offset++;
		return result;
	}

	public static byte[] SerializeBoolArray(bool[] values)
	{
		List<byte> list = new List<byte>();
		list.AddRange(BitConverter.GetBytes((ushort)values.Length));
		foreach (bool value in values)
		{
			list.AddRange(BitConverter.GetBytes(value));
		}
		return list.ToArray();
	}

	public static bool[] DeserializeBoolArray(byte[] bytes, ref int offset)
	{
		int num = BitConverter.ToUInt16(bytes, offset);
		offset += 2;
		bool[] array = new bool[num];
		for (int i = 0; i < num; i++)
		{
			array[i] = BitConverter.ToBoolean(bytes, offset);
			offset++;
		}
		return array;
	}

	public static byte[] SerializeUshort(ushort value)
	{
		return BitConverter.GetBytes(value);
	}

	public static ushort DeserializeUshort(byte[] bytes, ref int offset)
	{
		ushort result = BitConverter.ToUInt16(bytes, offset);
		offset += 2;
		return result;
	}

	public static byte[] SerializeUlong(ulong value)
	{
		return BitConverter.GetBytes(value);
	}

	public static ulong DeserializeUlong(byte[] bytes, ref int offset)
	{
		ulong result = BitConverter.ToUInt64(bytes, offset);
		offset += 8;
		return result;
	}

	public static byte[] SerializeString(string s)
	{
		if (string.IsNullOrEmpty(s))
		{
			return BitConverter.GetBytes((ushort)0);
		}
		List<byte> list = new List<byte>();
		ushort num = (ushort)Encoding.UTF8.GetByteCount(s);
		list.AddRange(BitConverter.GetBytes(num));
		if (num > 0)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			list.AddRange(bytes);
		}
		return list.ToArray();
	}

	public static string DeserializeString(byte[] bytes, ref int offset)
	{
		ushort num = BitConverter.ToUInt16(bytes, offset);
		offset += 2;
		if (num > 0)
		{
			string result = Encoding.UTF8.GetString(bytes, offset, num);
			offset += num;
			return result;
		}
		return "";
	}

	public static byte[] SerializeStrings(string[] strings)
	{
		List<byte> list = new List<byte>();
		list.AddRange(BitConverter.GetBytes((ushort)strings.Length));
		foreach (string s in strings)
		{
			list.AddRange(SerializeString(s));
		}
		return list.ToArray();
	}

	public static string[] DeserializeStrings(byte[] bytes, ref int offset)
	{
		ushort num = BitConverter.ToUInt16(bytes, offset);
		offset += 2;
		string[] array = new string[num];
		for (int i = 0; i < num; i++)
		{
			array[i] = DeserializeString(bytes, ref offset);
		}
		return array;
	}

	public static byte[] SerializeVector2(Vector2 value)
	{
		List<byte> list = new List<byte>();
		list.AddRange(BitConverter.GetBytes(value.x));
		list.AddRange(BitConverter.GetBytes(value.y));
		return list.ToArray();
	}

	public static Vector2 DeserializeVector2(byte[] bytes, ref int offset)
	{
		Vector2 result = new Vector2
		{
			x = BitConverter.ToSingle(bytes, offset)
		};
		offset += 4;
		result.y = BitConverter.ToSingle(bytes, offset);
		offset += 4;
		return result;
	}

	public static byte[] SerializeVector3(Vector3 value)
	{
		List<byte> list = new List<byte>();
		list.AddRange(BitConverter.GetBytes(value.x));
		list.AddRange(BitConverter.GetBytes(value.y));
		list.AddRange(BitConverter.GetBytes(value.z));
		return list.ToArray();
	}

	public static Vector3 DeserializeVector3(byte[] bytes, ref int offset)
	{
		Vector3 result = new Vector3
		{
			x = BitConverter.ToSingle(bytes, offset)
		};
		offset += 4;
		result.y = BitConverter.ToSingle(bytes, offset);
		offset += 4;
		result.z = BitConverter.ToSingle(bytes, offset);
		offset += 4;
		return result;
	}

	public static byte[] SerializeQuaternion(Quaternion value)
	{
		List<byte> list = new List<byte>();
		list.AddRange(BitConverter.GetBytes(value.x));
		list.AddRange(BitConverter.GetBytes(value.y));
		list.AddRange(BitConverter.GetBytes(value.z));
		list.AddRange(BitConverter.GetBytes(value.w));
		return list.ToArray();
	}

	public static Quaternion DeserializeQuaternion(byte[] bytes, ref int offset)
	{
		Quaternion result = new Quaternion
		{
			x = BitConverter.ToSingle(bytes, offset)
		};
		offset += 4;
		result.y = BitConverter.ToSingle(bytes, offset);
		offset += 4;
		result.z = BitConverter.ToSingle(bytes, offset);
		offset += 4;
		result.w = BitConverter.ToSingle(bytes, offset);
		offset += 4;
		return result;
	}

	public static byte[] SerializeByteArray(byte[] value)
	{
		List<byte> list = new List<byte>();
		if (value == null)
		{
			list.AddRange(BitConverter.GetBytes(0));
		}
		else
		{
			list.AddRange(BitConverter.GetBytes(value.Length));
			list.AddRange(value);
		}
		return list.ToArray();
	}

	public static byte[] DeserializeByteArray(byte[] bytes, ref int offset)
	{
		int num = BitConverter.ToInt32(bytes, offset);
		offset += 4;
		if (num > 0)
		{
			byte[] array = new byte[num];
			Buffer.BlockCopy(bytes, offset, array, 0, num);
			offset += num;
			return array;
		}
		return null;
	}

	public static byte[] SerializeIntArray(int[] value)
	{
		List<byte> list = new List<byte>();
		if (value == null)
		{
			list.AddRange(BitConverter.GetBytes(0));
		}
		else
		{
			list.AddRange(BitConverter.GetBytes(value.Length));
			for (int i = 0; i < value.Length; i++)
			{
				list.AddRange(BitConverter.GetBytes(value[i]));
			}
		}
		return list.ToArray();
	}

	public static int[] DeserializeIntArray(byte[] bytes, ref int offset)
	{
		int num = BitConverter.ToInt32(bytes, offset);
		offset += 4;
		if (num > 0)
		{
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = DeserializeInt(bytes, ref offset);
			}
			return array;
		}
		return null;
	}

	public static byte[] SerializeColor(Color value)
	{
		List<byte> list = new List<byte>();
		Convert.ToByte(value.r);
		list.Add(Convert.ToByte(value.r * 255f));
		list.Add(Convert.ToByte(value.g * 255f));
		list.Add(Convert.ToByte(value.b * 255f));
		return list.ToArray();
	}

	public static Color DeserializeColor(byte[] bytes, ref int offset)
	{
		Color result = new Color
		{
			a = 1f,
			r = (float)(int)bytes[offset] / 255f
		};
		offset++;
		result.g = (float)(int)bytes[offset] / 255f;
		offset++;
		result.b = (float)(int)bytes[offset] / 255f;
		offset++;
		return result;
	}

	public static byte[] SerializeColor32(Color32 value)
	{
		List<byte> list = new List<byte>();
		Convert.ToByte(value.r);
		list.Add(value.r);
		list.Add(value.g);
		list.Add(value.b);
		return list.ToArray();
	}

	public static Color32 DeserializeColor32(byte[] bytes, ref int offset)
	{
		Color32 result = new Color32
		{
			a = byte.MaxValue,
			r = bytes[offset]
		};
		offset++;
		result.g = bytes[offset];
		offset++;
		result.b = bytes[offset];
		offset++;
		return result;
	}
}
