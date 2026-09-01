using System;
using System.Text;
using System.Xml;
using UnityEngine;
using XMLTypes;

public class XString : XData
{
	public const string TYPE = "String";

	public override string Type
	{
		get
		{
			return "String";
		}
	}

	public override object RawValue
	{
		get
		{
			return Value;
		}
	}

	public string Value { get; set; }

	public XString(string key, string value)
		: base(key)
	{
		Value = value;
	}

	public XString(string key)
		: base(key)
	{
	}

	public override XAttribute[] Serialize()
	{
		return XAttribute.Single(Convert.ToString(Value));
	}

	public override string ToString()
	{
		return Value;
	}

	public override byte[] Encode()
	{
		byte[] bytes = Encoding.UTF8.GetBytes(Value);
		int count = bytes.Length;
		int num = NetworkCompression.PackedUIntLength(count, false);
		byte[] array = new byte[num + bytes.Length];
		NetworkCompression.PackUInt(count, array, 0, false, num);
		Buffer.BlockCopy(bytes, 0, array, num, bytes.Length);
		return array;
	}

	public override int Decode(byte[] data, int offset)
	{
		int num = offset;
		int count;
		offset += NetworkCompression.UnpackUInt(data, offset, false, out count);
		Value = Encoding.UTF8.GetString(data, offset, count);
		offset += count;
		return offset - num;
	}

	public static XData DeSerialize(string key, XmlReader reader)
	{
		if (reader.Read())
		{
			string value;
			if (reader.NodeType == XmlNodeType.Text)
			{
				value = reader.Value;
				reader.Read();
			}
			else
			{
				value = string.Empty;
			}
			return new XString(key, value);
		}
		Debug.LogError("Error reading string!");
		return null;
	}

	public static XData DeSerialize(string key, XAttribute[] data)
	{
		return new XString(key, data[0].Value);
	}

	public static explicit operator string(XString xString)
	{
		return xString.Value;
	}
}
