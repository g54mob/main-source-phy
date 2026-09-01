using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using XMLTypes;

public class XIntegerArray : XData
{
	public const string TYPE = "IntegerArray";

	public override string Type
	{
		get
		{
			return "IntegerArray";
		}
	}

	public override object RawValue
	{
		get
		{
			return Value;
		}
	}

	public int[] Value { get; set; }

	public XIntegerArray(string key, int[] value)
		: base(key)
	{
		Value = value;
	}

	public XIntegerArray(string key)
		: base(key)
	{
	}

	public override XAttribute[] Serialize()
	{
		XAttribute[] array = new XAttribute[Value.Length];
		for (int i = 0; i < Value.Length; i++)
		{
			array[i] = new XAttribute("Integer", Value[i].ToString());
		}
		return array;
	}

	public override string ToString()
	{
		return string.Join(", ", Value.Select((int x) => x.ToString()).ToArray());
	}

	public override byte[] Encode()
	{
		int num = Value.Length;
		int num2 = NetworkCompression.PackedUIntLength(num, false);
		byte[] array = new byte[num2 + num * 4];
		NetworkCompression.PackUInt(num, array, 0, false, num2);
		for (int i = 0; i < num; i++)
		{
			Buffer.BlockCopy(BitConverter.GetBytes(Value[i]), 0, array, num2 + i * 4, 4);
		}
		return array;
	}

	public override int Decode(byte[] data, int offset)
	{
		int num = offset;
		int count;
		offset += NetworkCompression.UnpackUInt(data, offset, false, out count);
		Value = new int[count];
		for (int i = 0; i < count; i++)
		{
			Value[i] = BitConverter.ToInt32(data, offset);
			offset += 4;
		}
		return offset - num;
	}

	public static XData DeSerialize(string key, XmlReader reader)
	{
		List<int> list = new List<int>();
		if (reader.ReadToDescendant("Integer"))
		{
			do
			{
				if (reader.Read())
				{
					list.Add(Convert.ToInt32(reader.Value));
					reader.Read();
				}
			}
			while (reader.ReadToNextSibling("Integer"));
			while (reader.NodeType != XmlNodeType.EndElement || !reader.Name.Equals("IntegerArray"))
			{
				reader.Read();
			}
		}
		return new XIntegerArray(key, list.ToArray());
	}

	public static XData DeSerialize(string key, XAttribute[] data)
	{
		int[] array = new int[data.Length];
		for (int i = 0; i < data.Length; i++)
		{
			array[i] = Convert.ToInt32(data[i].Value);
		}
		return new XIntegerArray(key, array);
	}

	public static explicit operator int[](XIntegerArray xIntegerArray)
	{
		return xIntegerArray.Value;
	}
}
