using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using XMLTypes;

public class XSingleArray : XData
{
	public const string TYPE = "SingleArray";

	public override string Type
	{
		get
		{
			return "SingleArray";
		}
	}

	public override object RawValue
	{
		get
		{
			return Value;
		}
	}

	public float[] Value { get; set; }

	public XSingleArray(string key, float[] value)
		: base(key)
	{
		Value = value;
	}

	public XSingleArray(string key)
		: base(key)
	{
	}

	public override XAttribute[] Serialize()
	{
		XAttribute[] array = new XAttribute[Value.Length];
		for (int i = 0; i < Value.Length; i++)
		{
			array[i] = new XAttribute("Single", Value[i].ToString(StaticSettings.Culture));
		}
		return array;
	}

	public override string ToString()
	{
		return string.Join(", ", Value.Select((float x) => x.ToString(StaticSettings.Culture)).ToArray());
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
		Value = new float[count];
		for (int i = 0; i < count; i++)
		{
			Value[i] = BitConverter.ToSingle(data, offset);
			offset += 4;
		}
		return offset - num;
	}

	public static XData DeSerialize(string key, XmlReader reader)
	{
		List<float> list = new List<float>();
		if (reader.ReadToDescendant("Single"))
		{
			do
			{
				if (reader.Read())
				{
					list.Add(Convert.ToSingle(reader.Value));
					reader.Read();
				}
			}
			while (reader.ReadToNextSibling("Single"));
		}
		while (reader.NodeType != XmlNodeType.Element || !reader.Name.Equals("SingleArray"))
		{
			reader.Read();
		}
		return new XSingleArray(key, list.ToArray());
	}

	public static XData DeSerialize(string key, XAttribute[] data)
	{
		float[] array = new float[data.Length];
		for (int i = 0; i < data.Length; i++)
		{
			array[i] = XmlLoader.FastParseFloat(data[i].Value);
		}
		return new XSingleArray(key, array);
	}

	public static explicit operator float[](XSingleArray xSingleArray)
	{
		return xSingleArray.Value;
	}
}
