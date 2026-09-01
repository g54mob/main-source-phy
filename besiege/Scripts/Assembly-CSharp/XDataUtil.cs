using System;
using System.Xml;
using UnityEngine;
using XMLTypes;

public static class XDataUtil
{
	public static XData CreateXData(string type, string key, XAttribute[] data)
	{
		switch (type)
		{
		case "Integer":
			return XInteger.DeSerialize(key, data);
		case "Boolean":
			return XBoolean.DeSerialize(key, data);
		case "Single":
			return XSingle.DeSerialize(key, data);
		case "Vector3":
			return XVector3.DeSerialize(key, data);
		case "Color":
			return XColor.DeSerialize(key, data);
		case "IntegerArray":
			return XIntegerArray.DeSerialize(key, data);
		case "SingleArray":
			return XSingleArray.DeSerialize(key, data);
		case "StringArray":
			return XStringArray.DeSerialize(key, data);
		case "String":
			return XString.DeSerialize(key, data);
		default:
			throw new ArgumentException(type + " is not recognized and cannot be deserialized.");
		}
	}

	public static XData CreateXData(XmlReader reader, string key)
	{
		switch (reader.Name)
		{
		case "Integer":
			return XInteger.DeSerialize(key, reader);
		case "Boolean":
			return XBoolean.DeSerialize(key, reader);
		case "Single":
			return XSingle.DeSerialize(key, reader);
		case "Vector3":
			return XVector3.DeSerialize(key, reader);
		case "Color":
			return XColor.DeSerialize(key, reader);
		case "IntegerArray":
			return XIntegerArray.DeSerialize(key, reader);
		case "SingleArray":
			return XSingleArray.DeSerialize(key, reader);
		case "StringArray":
			return XStringArray.DeSerialize(key, reader);
		case "String":
			return XString.DeSerialize(key, reader);
		default:
			Debug.LogError("Unknown data type " + reader.Name + "!");
			return new XColor(key, Color.white);
		}
	}
}
