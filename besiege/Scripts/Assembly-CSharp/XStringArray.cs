using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using XMLTypes;

public class XStringArray : XData
{
	public const string TYPE = "StringArray";

	public override string Type
	{
		get
		{
			return "StringArray";
		}
	}

	public override object RawValue
	{
		get
		{
			return Value;
		}
	}

	public string[] Value { get; set; }

	public XStringArray(string key, string[] value)
		: base(key)
	{
		Value = value;
	}

	public XStringArray(string key)
		: base(key)
	{
	}

	public override XAttribute[] Serialize()
	{
		XAttribute[] array = new XAttribute[Value.Length];
		for (int i = 0; i < Value.Length; i++)
		{
			array[i] = new XAttribute("String", Value[i]);
		}
		return array;
	}

	public override string ToString()
	{
		return string.Join(", ", Value);
	}

	public override byte[] Encode()
	{
		int num = 0;
		int num2 = Value.Length;
		byte[][] array = new byte[num2][];
		for (int i = 0; i < num2; i++)
		{
			byte[] array2 = (array[i] = Encoding.UTF8.GetBytes(Value[i]));
			num += NetworkCompression.PackedUIntLength(array2.Length, false) + array2.Length;
		}
		int num3 = NetworkCompression.PackedUIntLength(num, false);
		byte[] array3 = new byte[num3 + num];
		int num4 = 0;
		NetworkCompression.PackUInt(num, array3, 0, false, num3);
		num4 = num3;
		foreach (byte[] array2 in array)
		{
			num3 = NetworkCompression.PackedUIntLength(array2.Length, false);
			NetworkCompression.PackUInt(array2.Length, array3, num4, false, num3);
			num4 += num3;
			Buffer.BlockCopy(array2, 0, array3, num4, array2.Length);
			num4 += array2.Length;
		}
		return array3;
	}

	public override int Decode(byte[] data, int offset)
	{
		int num = offset;
		List<string> list = new List<string>();
		int count;
		offset += NetworkCompression.UnpackUInt(data, offset, false, out count);
		int num2 = 0;
		while (num2 < count)
		{
			int count2;
			num2 += NetworkCompression.UnpackUInt(data, offset + num2, false, out count2);
			list.Add(Encoding.UTF8.GetString(data, offset + num2, count2));
			num2 += count2;
		}
		Value = list.ToArray();
		offset += count;
		return offset - num;
	}

	public static XData DeSerialize(string key, XmlReader reader)
	{
		List<string> list = new List<string>();
		if (reader.ReadToDescendant("String"))
		{
			do
			{
				if (reader.Read())
				{
					list.Add(reader.Value);
					reader.Read();
				}
			}
			while (reader.ReadToNextSibling("String"));
			while (reader.NodeType != XmlNodeType.EndElement || !reader.Name.Equals("StringArray"))
			{
				reader.Read();
			}
		}
		return new XStringArray(key, list.ToArray());
	}

	public static XData DeSerialize(string key, XAttribute[] data)
	{
		string[] array = new string[data.Length];
		for (int i = 0; i < data.Length; i++)
		{
			array[i] = data[i].Value;
		}
		return new XStringArray(key, array);
	}

	public static explicit operator string[](XStringArray xStringArray)
	{
		return xStringArray.Value;
	}
}
