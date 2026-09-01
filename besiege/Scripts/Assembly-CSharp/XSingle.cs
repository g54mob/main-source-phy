using System;
using System.Xml;
using UnityEngine;
using XMLTypes;

public class XSingle : XData
{
	public const string TYPE = "Single";

	public const int EncodedSize = 4;

	public override string Type
	{
		get
		{
			return "Single";
		}
	}

	public override object RawValue
	{
		get
		{
			return Value;
		}
	}

	public float Value { get; set; }

	public XSingle(string key, float value)
		: base(key)
	{
		Value = value;
	}

	public XSingle(string key)
		: base(key)
	{
	}

	public override XAttribute[] Serialize()
	{
		return XAttribute.Single(Convert.ToString(Value, StaticSettings.Culture));
	}

	public override string ToString()
	{
		return Value.ToString(StaticSettings.Culture);
	}

	public override byte[] Encode()
	{
		return BitConverter.GetBytes(Value);
	}

	public override int Decode(byte[] data, int offset)
	{
		Value = BitConverter.ToSingle(data, offset);
		return 4;
	}

	public static XData DeSerialize(string key, XmlReader reader)
	{
		if (reader.Read() && reader.NodeType == XmlNodeType.Text)
		{
			string value = reader.Value;
			reader.Read();
			float result;
			if (float.TryParse(value, out result))
			{
				return new XSingle(key, result);
			}
		}
		Debug.LogError("Error reading float!");
		return null;
	}

	public static XData DeSerialize(string key, XAttribute[] data)
	{
		return new XSingle(key, XmlLoader.FastParseFloat(data[0].Value));
	}

	public static explicit operator float(XSingle xSingle)
	{
		return xSingle.Value;
	}
}
