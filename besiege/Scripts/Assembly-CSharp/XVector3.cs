using System;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;
using XMLTypes;

public class XVector3 : XData
{
	public const string TYPE = "Vector3";

	public const int EncodedSize = 12;

	private byte[] encodeArray;

	private NetworkWriter nw;

	public override string Type
	{
		get
		{
			return "Vector3";
		}
	}

	public override object RawValue
	{
		get
		{
			return Value;
		}
	}

	public Vector3 Value { get; set; }

	public XVector3(string key, float x, float y, float z)
		: this(key, new Vector3(x, y, z))
	{
	}

	public XVector3(string key, Vector3 value)
		: base(key)
	{
		Value = value;
		encodeArray = new byte[12];
		nw = new NetworkWriter(encodeArray);
	}

	public XVector3(string key)
		: base(key)
	{
		encodeArray = new byte[12];
		nw = new NetworkWriter(encodeArray);
	}

	public override XAttribute[] Serialize()
	{
		return new XAttribute[3]
		{
			new XAttribute("X", Value.x.ToString(StaticSettings.Culture)),
			new XAttribute("Y", Value.y.ToString(StaticSettings.Culture)),
			new XAttribute("Z", Value.z.ToString(StaticSettings.Culture))
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
		float x = BitConverter.ToSingle(data, offset);
		float y = BitConverter.ToSingle(data, offset + 4);
		float z = BitConverter.ToSingle(data, offset + 8);
		Value = new Vector3(x, y, z);
		return 12;
	}

	public static XData DeSerialize(string key, XmlReader reader)
	{
		Vector3 zero = Vector3.zero;
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
			zero[i] = result;
		}
		reader.Read();
		return new XVector3(key, zero);
	}

	public static XData DeSerialize(string key, XAttribute[] data)
	{
		return new XVector3(key, XmlLoader.FastParseFloat(data[0].Value), XmlLoader.FastParseFloat(data[1].Value), XmlLoader.FastParseFloat(data[2].Value));
	}

	public static explicit operator Vector3(XVector3 xVector3)
	{
		return new Vector3(xVector3.Value.x, xVector3.Value.y, xVector3.Value.z);
	}
}
