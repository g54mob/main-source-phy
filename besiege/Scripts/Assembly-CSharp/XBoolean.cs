using System;
using System.Xml;
using UnityEngine;
using XMLTypes;

public class XBoolean : XData
{
	public const string TYPE = "Boolean";

	public const int EncodedSize = 1;

	public override string Type
	{
		get
		{
			return "Boolean";
		}
	}

	public override object RawValue
	{
		get
		{
			return Value;
		}
	}

	public bool Value { get; set; }

	public XBoolean(string key, bool value)
		: base(key)
	{
		Value = value;
	}

	public XBoolean(string key)
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
		return new byte[1] { (byte)(Value ? 1u : 0u) };
	}

	public override int Decode(byte[] data, int offset)
	{
		Value = data[offset] == 1;
		return 1;
	}

	public static XData DeSerialize(string key, XmlReader reader)
	{
		if (reader.Read() && reader.NodeType == XmlNodeType.Text)
		{
			bool value = reader.Value.Equals("True");
			reader.Read();
			return new XBoolean(key, value);
		}
		Debug.LogError("Error reading bool!");
		return null;
	}

	public static XData DeSerialize(string key, XAttribute[] data)
	{
		return new XBoolean(key, Convert.ToBoolean(data[0].Value));
	}

	public static explicit operator bool(XBoolean xBoolean)
	{
		return xBoolean.Value;
	}
}
