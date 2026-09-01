using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class XDataHolder
{
	private readonly HashSet<XData> data = new HashSet<XData>();

	private static Dictionary<Type, byte> typeMap = new Dictionary<Type, byte>
	{
		{
			typeof(XBoolean),
			0
		},
		{
			typeof(XColor),
			1
		},
		{
			typeof(XInteger),
			2
		},
		{
			typeof(XIntegerArray),
			3
		},
		{
			typeof(XSingle),
			4
		},
		{
			typeof(XSingleArray),
			5
		},
		{
			typeof(XString),
			6
		},
		{
			typeof(XStringArray),
			7
		},
		{
			typeof(XVector3),
			8
		}
	};

	private bool _wasLoadedFromFile;

	private bool _wasCreated;

	public bool WasSimulationStarted { get; set; }

	public bool WasLoadedFromFile
	{
		get
		{
			return _wasLoadedFromFile;
		}
		set
		{
			_wasLoadedFromFile = value;
		}
	}

	public bool WasCreated
	{
		get
		{
			return _wasCreated;
		}
		set
		{
			_wasCreated = value;
		}
	}

	public bool HasData
	{
		get
		{
			return data.Count > 0;
		}
	}

	public void EraseCustomBlockData()
	{
		foreach (XData item in new HashSet<XData>(data))
		{
			if (!item.Key.StartsWith("bmt-"))
			{
				data.Remove(item);
			}
		}
	}

	public void Clear()
	{
		data.Clear();
	}

	public void Remove(string key)
	{
		XData xData = Read(key);
		if (xData != null)
		{
			data.Remove(xData);
		}
	}

	public int Decode(byte[] holderData, int offset)
	{
		Clear();
		int num = offset;
		int count;
		int num2 = NetworkCompression.UnpackUInt(holderData, offset, true, out count);
		offset += num2;
		for (int i = 0; i < count; i++)
		{
			byte type = holderData[offset];
			int num3 = holderData[offset + 1];
			string key = Encoding.UTF8.GetString(holderData, offset + 2, num3);
			offset += num3 + 2;
			int containerSize = GetContainerSize(type, holderData, offset);
			XData instance = GetInstance(type, key);
			instance.Decode(holderData, offset);
			data.Add(instance);
			offset += containerSize;
		}
		return offset - num;
	}

	public bool Encode(out byte[] outData)
	{
		byte[][] array = new byte[data.Count][];
		int num = 0;
		int num2 = 0;
		HashSet<XData>.Enumerator enumerator = data.GetEnumerator();
		while (enumerator.MoveNext())
		{
			byte[] array2 = EncodeXData(enumerator.Current);
			array[num2++] = array2;
			num += array2.Length;
		}
		int num3 = NetworkCompression.PackedUIntLength(data.Count, true);
		outData = new byte[num3 + num];
		NetworkCompression.PackUInt(data.Count, outData, 0, true, num3);
		NetworkCompression.WriteArray(array, outData, num3);
		return data.Count > 0;
	}

	private static XData GetInstance(byte type, string key)
	{
		switch (type)
		{
		case 0:
			return new XBoolean(key);
		case 1:
			return new XColor(key);
		case 2:
			return new XInteger(key);
		case 3:
			return new XIntegerArray(key);
		case 4:
			return new XSingle(key);
		case 5:
			return new XSingleArray(key);
		case 6:
			return new XString(key);
		case 7:
			return new XStringArray(key);
		case 8:
			return new XVector3(key);
		default:
			Debug.LogError(key + ": Couldn't map from " + type + " to valid XData type!");
			return null;
		}
	}

	private static int GetContainerSize(byte type, byte[] buffer, int offset)
	{
		int count;
		switch (type)
		{
		case 0:
			return 1;
		case 1:
			return 12;
		case 2:
			return 4;
		case 3:
		{
			int num = NetworkCompression.UnpackUInt(buffer, offset, false, out count);
			return num + count * 4;
		}
		case 4:
			return 4;
		case 5:
		{
			int num = NetworkCompression.UnpackUInt(buffer, offset, false, out count);
			return num + count * 4;
		}
		case 6:
		{
			int num = NetworkCompression.UnpackUInt(buffer, offset, false, out count);
			return num + count;
		}
		case 7:
		{
			int num = NetworkCompression.UnpackUInt(buffer, offset, false, out count);
			return num + count;
		}
		case 8:
			return 12;
		default:
			Debug.LogError("Couldn't map from " + type + " to valid XData type!");
			return -1;
		}
	}

	public static byte[] EncodeXData(XData xData)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(xData.Key);
		byte[] array = xData.Encode();
		int num = 2 + bytes.Length;
		byte[] array2 = new byte[num + array.Length];
		array2[0] = typeMap[xData.GetType()];
		array2[1] = (byte)bytes.Length;
		Buffer.BlockCopy(bytes, 0, array2, 2, bytes.Length);
		Buffer.BlockCopy(array, 0, array2, num, array.Length);
		return array2;
	}

	public static int DecodeXData(byte[] buffer, int offset, out XData xData)
	{
		int num = offset;
		byte type = buffer[offset];
		int num2 = buffer[offset + 1];
		string key = Encoding.UTF8.GetString(buffer, offset + 2, num2);
		offset += num2 + 2;
		xData = GetInstance(type, key);
		offset += xData.Decode(buffer, offset);
		return offset - num;
	}

	public void Write(string key, object data)
	{
		Type type = data.GetType();
		switch (Type.GetTypeCode(type))
		{
		case TypeCode.Single:
		case TypeCode.Double:
		case TypeCode.Decimal:
			Write(new XSingle(key, (float)data));
			return;
		case TypeCode.Int16:
		case TypeCode.Int32:
		case TypeCode.Int64:
			Write(new XInteger(key, (int)data));
			return;
		case TypeCode.Boolean:
			Write(new XBoolean(key, (bool)data));
			return;
		case TypeCode.String:
			Write(new XString(key, (string)data));
			return;
		}
		if (type == typeof(int[]))
		{
			Write(new XIntegerArray(key, (int[])data));
			return;
		}
		if (type == typeof(float[]))
		{
			Write(new XSingleArray(key, (float[])data));
			return;
		}
		if (type == typeof(string[]))
		{
			Write(new XStringArray(key, (string[])data));
			return;
		}
		if (type == typeof(Vector3))
		{
			Write(new XVector3(key, (Vector3)data));
			return;
		}
		if (type == typeof(Color))
		{
			Write(new XColor(key, (Color)data));
			return;
		}
		throw new ArgumentException(type.Name + " is not supported.");
	}

	public bool HasKey(string key)
	{
		foreach (XData datum in data)
		{
			if (string.Equals(datum.Key, key))
			{
				return true;
			}
		}
		return false;
	}

	public void Write(XData data)
	{
		if (HasKey(data.Key))
		{
			this.data.Remove(Read(data.Key));
		}
		this.data.Add(data);
	}

	public void Write(XDataHolder data)
	{
		foreach (XData datum in this.data)
		{
			data.Write(datum.Key, datum.RawValue);
		}
	}

	public XDataHolder Clone()
	{
		XDataHolder result = new XDataHolder();
		Write(result);
		return result;
	}

	public HashSet<XData> ReadAll()
	{
		return new HashSet<XData>(data);
	}

	public XData Read(string key)
	{
		foreach (XData datum in data)
		{
			if (string.Equals(datum.Key, key))
			{
				return datum;
			}
		}
		return null;
	}

	public Vector3 ReadVector3(string key)
	{
		return (Vector3)(XVector3)Read(key);
	}

	public Color ReadColor(string key)
	{
		return (Color)(XColor)Read(key);
	}

	public float ReadFloat(string key)
	{
		return (float)(XSingle)Read(key);
	}

	public bool ReadBool(string key)
	{
		return (bool)(XBoolean)Read(key);
	}

	public int ReadInt(string key)
	{
		return (int)(XInteger)Read(key);
	}

	public int[] ReadIntArray(string key)
	{
		return (int[])(XIntegerArray)Read(key);
	}

	public float[] ReadFloatArray(string key)
	{
		return (float[])(XSingleArray)Read(key);
	}

	public string[] ReadStringArray(string key)
	{
		return (string[])(XStringArray)Read(key);
	}

	public string ReadString(string key)
	{
		return (string)(XString)Read(key);
	}
}
