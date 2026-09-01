using System;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;
using XMLTypes;

public class XColor : XData
{
	public const string TYPE = "Color";

	public const int EncodedSize = 12;

	private byte[] encodeArray;

	private NetworkWriter nw;

	public override string Type
	{
		get
		{
			return "Color";
		}
	}

	public override object RawValue
	{
		get
		{
			return Value;
		}
	}

	public Color Value { get; set; }

	public XColor(string key, float r, float g, float b)
		: this(key, new Color(r, g, b))
	{
	}

	public XColor(string key, Color value)
		: this(key)
	{
		Value = value;
	}

	public XColor(string key)
		: base(key)
	{
		encodeArray = new byte[12];
		nw = new NetworkWriter(encodeArray);
	}

	public override XAttribute[] Serialize()
	{
		return new XAttribute[3]
		{
			new XAttribute("R", Value.r.ToString(StaticSettings.Culture)),
			new XAttribute("G", Value.g.ToString(StaticSettings.Culture)),
			new XAttribute("B", Value.b.ToString(StaticSettings.Culture))
		};
	}

	public override string ToString()
	{
		return Value.ToString();
	}

	public override byte[] Encode()
	{
		nw.SeekZero();
		nw.Write(Value);
		Buffer.BlockCopy(nw.AsArray(), 0, encodeArray, 0, 12);
		return encodeArray;
	}

	public override int Decode(byte[] data, int offset)
	{
		float r = BitConverter.ToSingle(data, offset);
		float g = BitConverter.ToSingle(data, offset + 4);
		float b = BitConverter.ToSingle(data, offset + 8);
		Value = new Color(r, g, b);
		return 12;
	}

	public static XData DeSerialize(string key, XmlReader reader)
	{
		Color white = Color.white;
		for (int i = 0; i < 3; i++)
		{
			if (!reader.Read())
			{
				break;
			}
			if (!reader.Read())
			{
				break;
			}
			float result;
			if (!float.TryParse(reader.Value, out result))
			{
				break;
			}
			if (!reader.Read())
			{
				break;
			}
			white[i] = result;
		}
		reader.Read();
		return new XColor(key, white);
	}

	public static XData DeSerialize(string key, XAttribute[] data)
	{
		return new XColor(key, Convert.ToSingle(data[0].Value), Convert.ToSingle(data[1].Value), Convert.ToSingle(data[2].Value));
	}

	public static explicit operator Color(XColor xColor)
	{
		return new Color(xColor.Value.r, xColor.Value.g, xColor.Value.b);
	}
}
