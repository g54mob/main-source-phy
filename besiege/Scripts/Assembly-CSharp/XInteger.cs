using System;
using System.Xml;
using UnityEngine;
using XMLTypes;

public class XInteger : XData
{
	public const string TYPE = "Integer";

	public const int EncodedSize = 4;

	public override string Type
	{
		get
		{
			return "Integer";
		}
	}

	public override object RawValue
	{
		get
		{
			return Value;
		}
	}

	public int Value { get; set; }

	public XInteger(string key, int value)
		: base(key)
	{
		Value = value;
	}

	public XInteger(string key)
		: base(key)
	{
	}

	public override XAttribute[] Serialize()
	{
		return XAttribute.Single(Convert.ToString(Value));
	}

	public override string ToString()
	{
		return Value.ToString();
	}

	public override byte[] Encode()
	{
		return BitConverter.GetBytes(Value);
	}

	public override int Decode(byte[] data, int offset)
	{
		Value = BitConverter.ToInt32(data, offset);
		return 4;
	}

	public static XData DeSerialize(string key, XmlReader reader)
	{
		int result;
		if (reader.Read() && reader.NodeType == XmlNodeType.Text && int.TryParse(reader.Value, out result) && reader.Read())
		{
			return new XInteger(key, result);
		}
		Debug.LogError(string.Concat("Error occured while reading Integer: ", reader.NodeType, " ", reader.Value));
		return null;
	}

	public static XData DeSerialize(string key, XAttribute[] data)
	{
		return new XInteger(key, Convert.ToInt32(data[0].Value));
	}

	public static explicit operator int(XInteger xInteger)
	{
		return xInteger.Value;
	}
}
