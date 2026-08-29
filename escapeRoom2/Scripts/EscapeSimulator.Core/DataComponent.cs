using System;
using System.Collections.Generic;
using UnityEngine;

public class DataComponent : MonoBehaviour, ISaveable
{
	public enum CustomEnum
	{
		A = 0,
		B = 1,
		C = 2,
		D = 3
	}

	[Serializable]
	public struct CustomStruct
	{
		public int nestedInt;

		public string nestedString;
	}

	[Header("1 byte values")]
	public byte byteValue;

	public sbyte sbyteValue;

	public bool boolValue;

	[Header("2 byte values")]
	public short shortValue;

	public ushort ushortValue;

	public char charValue;

	[Header("4 byte values")]
	public int intValue;

	public uint uintValue;

	public float floatValue;

	[Header("8 byte values")]
	public long longValue;

	public ulong ulongValue;

	public double doubleValue;

	[Header("Strings and enums")]
	public string stringValue;

	public CustomEnum enumValue;

	[Header("References")]
	public GameObject objectReference;

	public Transform transformReference;

	[Header("Custom nested types")]
	public CustomStruct structValue;

	[Header("Collections")]
	public int[] intArray;

	public List<int> intList;

	public Dictionary<string, int> dictionary = new Dictionary<string, int>();

	public int? nullableInt;

	public Vector3? nullableVector3;

	[Header("Not saved")]
	[DontSave]
	public int dontSaveInt;

	public const int constInt = 0;

	public static int staticInt;

	public readonly int readonlyInt;

	public int propertyInt { get; set; }

	private void Start()
	{
		Debug.Log("DataComponent started!");
	}

	public virtual void save(FastBinaryWriter writer)
	{
		writer.Write(byteValue);
		writer.Write(in sbyteValue, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in boolValue, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in shortValue, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in ushortValue, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in charValue, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in intValue, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in uintValue, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in floatValue, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in longValue, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in ulongValue, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in doubleValue, default(FastBinaryWriter.ForPrimitives));
		writer.Write(stringValue);
		int value = (int)enumValue;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.WriteGameObject(objectReference);
		writer.WriteComponent(transformReference);
		writer.WriteCustomStruct(structValue);
		writer.WriteArray(intArray, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteList(intList, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteDictionary(dictionary, delegate(FastBinaryWriter w, string k)
		{
			w.Write(k);
		}, delegate(FastBinaryWriter w, int v)
		{
			w.Write(in v, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteNullable(nullableInt, delegate(FastBinaryWriter w, int n)
		{
			w.Write(in n, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteNullable(nullableVector3, delegate(FastBinaryWriter w, Vector3 n)
		{
			w.WriteVector3(in n);
		});
	}

	public virtual void load(FastBinaryReader reader)
	{
		byteValue = reader.ReadByte();
		sbyteValue = reader.ReadSByte();
		boolValue = reader.ReadBoolean();
		shortValue = reader.ReadInt16();
		ushortValue = reader.ReadUInt16();
		charValue = reader.ReadChar();
		intValue = reader.ReadInt32();
		uintValue = reader.ReadUInt32();
		floatValue = reader.ReadSingle();
		longValue = reader.ReadInt64();
		ulongValue = reader.ReadUInt64();
		doubleValue = reader.ReadDouble();
		stringValue = reader.ReadString();
		enumValue = (CustomEnum)reader.ReadInt32();
		objectReference = reader.ReadGameObject();
		transformReference = reader.ReadComponent<Transform>();
		structValue = reader.ReadCustomStruct();
		intArray = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		intList = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		dictionary = reader.ReadDictionary((FastBinaryReader kr) => kr.ReadString(), (FastBinaryReader vr) => vr.ReadInt32());
		nullableInt = reader.ReadNullable((FastBinaryReader r) => r.ReadInt32());
		nullableVector3 = reader.ReadNullable((FastBinaryReader r) => r.ReadVector3());
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		byte b = reader.ReadByte();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "byteValue",
			fieldValue = $"{b}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		sbyte b2 = reader.ReadSByte();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "sbyteValue",
			fieldValue = $"{b2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "boolValue",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		short num = reader.ReadInt16();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "shortValue",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		ushort num2 = reader.ReadUInt16();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "ushortValue",
			fieldValue = $"{num2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		char c = reader.ReadChar();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "charValue",
			fieldValue = $"{c}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num3 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "intValue",
			fieldValue = $"{num3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		uint num4 = reader.ReadUInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "uintValue",
			fieldValue = $"{num4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num5 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "floatValue",
			fieldValue = $"{num5}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		long num6 = reader.ReadInt64();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "longValue",
			fieldValue = $"{num6}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		ulong num7 = reader.ReadUInt64();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "ulongValue",
			fieldValue = $"{num7}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		double num8 = reader.ReadDouble();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "doubleValue",
			fieldValue = $"{num8}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		string text = reader.ReadString();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "stringValue",
			fieldValue = (text ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		CustomEnum customEnum = (CustomEnum)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "enumValue",
			fieldValue = $"{customEnum}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject arg = reader.ReadGameObject();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "objectReference",
			fieldValue = $"{arg}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Transform arg2 = reader.ReadComponent<Transform>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "transformReference",
			fieldValue = $"{arg2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		CustomStruct customStruct = reader.ReadCustomStruct();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "structValue",
			fieldValue = $"{customStruct}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[] array = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "intArray[" + ((array == null) ? string.Empty : array.Length.ToString()) + "]",
			fieldValue = (((array == null) ? "null" : string.Join(", ", array)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<int> list = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "intList[" + ((list == null) ? string.Empty : list.Count.ToString()) + "]",
			fieldValue = (((list == null) ? "null" : string.Join(", ", list)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Dictionary<string, int> dictionary = reader.ReadDictionary((FastBinaryReader kr) => kr.ReadString(), (FastBinaryReader vr) => vr.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "dictionary[" + ((dictionary == null) ? string.Empty : dictionary.Count.ToString()) + "]",
			fieldValue = (((dictionary == null) ? "null" : string.Join(", ", dictionary)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int? num9 = reader.ReadNullable((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "nullableInt",
			fieldValue = $"{num9}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Vector3? vector = reader.ReadNullable((FastBinaryReader r) => r.ReadVector3());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "nullableVector3",
			fieldValue = $"{vector}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}
