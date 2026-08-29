using System;
using System.Collections.Generic;
using System.Text;

public class ToStringTestA : IReadWrite
{
	public int integer = 1;

	public int? nullableIntegerValue = 2;

	public int? nullableIntegerNull;

	public ToStringTestEnum enumeration;

	public ToStringTestEnum? nullableEnumerationValue = ToStringTestEnum.Second;

	public ToStringTestEnum? nullableEnumerationNull;

	public string text = "Some text!";

	public string textEmpty = string.Empty;

	public string textNull;

	public int[] intArray = new int[3] { 1, 2, 3 };

	public int[] intArrayEmpty = Array.Empty<int>();

	public int[] intArrayNull;

	public List<int> intList = new List<int> { 1, 2, 3 };

	public List<int> intListEmpty = new List<int>();

	public List<int> intListNull;

	public HashSet<int> intSet = new HashSet<int> { 1, 2, 3 };

	public HashSet<int> intSetEmpty = new HashSet<int>();

	public HashSet<int> intSetNull;

	public Dictionary<string, int> stringToIntDictionary = new Dictionary<string, int>
	{
		{ "1st", 1 },
		{ "2nd", 2 },
		{ "3rd", 3 }
	};

	public Dictionary<string, int> stringToIntDictionaryEmpty = new Dictionary<string, int>();

	public Dictionary<string, int> stringToIntDictionaryNull;

	public ToStringTestB bInstance = new ToStringTestB
	{
		bInt = 3,
		bString = "b"
	};

	public ToStringTestB bInstanceNull;

	public ToStringTestB[] bArrayNull;

	public List<ToStringTestB> bListNull;

	public HashSet<ToStringTestB> bSetNull;

	public Dictionary<string, ToStringTestB> bDictionaryNull;

	public ToStringTestB[] bArrayEmpty = Array.Empty<ToStringTestB>();

	public List<ToStringTestB> bListEmpty = new List<ToStringTestB>();

	public HashSet<ToStringTestB> bSetEmpty = new HashSet<ToStringTestB>();

	public Dictionary<string, ToStringTestB> bDictionaryEmpty = new Dictionary<string, ToStringTestB>();

	public ToStringTestB[] bArray = new ToStringTestB[3]
	{
		new ToStringTestB
		{
			bInt = 1,
			bString = "arrayA"
		},
		new ToStringTestB
		{
			bInt = 2,
			bString = "arrayB"
		},
		new ToStringTestB
		{
			bInt = 3,
			bString = "arrayC"
		}
	};

	public List<ToStringTestB> bList = new List<ToStringTestB>
	{
		new ToStringTestB
		{
			bInt = 1,
			bString = "listA"
		},
		new ToStringTestB
		{
			bInt = 2,
			bString = "listB"
		},
		new ToStringTestB
		{
			bInt = 3,
			bString = "listC"
		}
	};

	public HashSet<ToStringTestB> bSet = new HashSet<ToStringTestB>
	{
		new ToStringTestB
		{
			bInt = 1,
			bString = "setA"
		},
		new ToStringTestB
		{
			bInt = 2,
			bString = "setB"
		},
		new ToStringTestB
		{
			bInt = 3,
			bString = "setC"
		}
	};

	public Dictionary<string, ToStringTestB> bDictionary = new Dictionary<string, ToStringTestB>
	{
		{
			"keyA",
			new ToStringTestB
			{
				bInt = 1,
				bString = "valueA"
			}
		},
		{
			"keyB",
			new ToStringTestB
			{
				bInt = 2,
				bString = "valueB"
			}
		},
		{
			"keyC",
			new ToStringTestB
			{
				bInt = 3,
				bString = "valueC"
			}
		}
	};

	public List<List<int>> intDoubleList = new List<List<int>>
	{
		new List<int> { 1, 2, 3 },
		new List<int> { 4, 5, 6 }
	};

	public List<List<ToStringTestB>> bDoubleList = new List<List<ToStringTestB>>
	{
		new List<ToStringTestB>
		{
			new ToStringTestB
			{
				bInt = 1,
				bString = "listA"
			},
			new ToStringTestB
			{
				bInt = 2,
				bString = "listB"
			},
			new ToStringTestB
			{
				bInt = 3,
				bString = "listC"
			}
		},
		new List<ToStringTestB>
		{
			new ToStringTestB
			{
				bInt = 1,
				bString = "listA"
			},
			new ToStringTestB
			{
				bInt = 2,
				bString = "listB"
			},
			new ToStringTestB
			{
				bInt = 3,
				bString = "listC"
			}
		}
	};

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(in integer, default(FastBinaryWriter.ForPrimitives));
		writer.WriteNullable(nullableIntegerValue, delegate(FastBinaryWriter w, int n)
		{
			w.Write(in n, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteNullable(nullableIntegerNull, delegate(FastBinaryWriter w, int n)
		{
			w.Write(in n, default(FastBinaryWriter.ForPrimitives));
		});
		int value = (int)enumeration;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.WriteNullable(nullableEnumerationValue, delegate(FastBinaryWriter w, ToStringTestEnum n)
		{
			int value2 = (int)n;
			w.Write(in value2, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteNullable(nullableEnumerationNull, delegate(FastBinaryWriter w, ToStringTestEnum n)
		{
			int value2 = (int)n;
			w.Write(in value2, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(text);
		writer.Write(textEmpty);
		writer.Write(textNull);
		writer.WriteArray(intArray, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(intArrayEmpty, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(intArrayNull, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteList(intList, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteList(intListEmpty, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteList(intListNull, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteHashSet(intSet, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteHashSet(intSetEmpty, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteHashSet(intSetNull, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteDictionary(stringToIntDictionary, delegate(FastBinaryWriter w, string k)
		{
			w.Write(k);
		}, delegate(FastBinaryWriter w, int v)
		{
			w.Write(in v, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteDictionary(stringToIntDictionaryEmpty, delegate(FastBinaryWriter w, string k)
		{
			w.Write(k);
		}, delegate(FastBinaryWriter w, int v)
		{
			w.Write(in v, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteDictionary(stringToIntDictionaryNull, delegate(FastBinaryWriter w, string k)
		{
			w.Write(k);
		}, delegate(FastBinaryWriter w, int v)
		{
			w.Write(in v, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteIReadWrite(bInstance);
		writer.WriteIReadWrite(bInstanceNull);
		writer.WriteArray(bArrayNull, delegate(FastBinaryWriter w, ToStringTestB e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(bListNull, delegate(FastBinaryWriter w, ToStringTestB e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteHashSet(bSetNull, delegate(FastBinaryWriter w, ToStringTestB e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteDictionary(bDictionaryNull, delegate(FastBinaryWriter w, string k)
		{
			w.Write(k);
		}, delegate(FastBinaryWriter w, ToStringTestB v)
		{
			w.WriteIReadWrite(v);
		});
		writer.WriteArray(bArrayEmpty, delegate(FastBinaryWriter w, ToStringTestB e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(bListEmpty, delegate(FastBinaryWriter w, ToStringTestB e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteHashSet(bSetEmpty, delegate(FastBinaryWriter w, ToStringTestB e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteDictionary(bDictionaryEmpty, delegate(FastBinaryWriter w, string k)
		{
			w.Write(k);
		}, delegate(FastBinaryWriter w, ToStringTestB v)
		{
			w.WriteIReadWrite(v);
		});
		writer.WriteArray(bArray, delegate(FastBinaryWriter w, ToStringTestB e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteList(bList, delegate(FastBinaryWriter w, ToStringTestB e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteHashSet(bSet, delegate(FastBinaryWriter w, ToStringTestB e)
		{
			w.WriteIReadWrite(e);
		});
		writer.WriteDictionary(bDictionary, delegate(FastBinaryWriter w, string k)
		{
			w.Write(k);
		}, delegate(FastBinaryWriter w, ToStringTestB v)
		{
			w.WriteIReadWrite(v);
		});
		writer.WriteList(intDoubleList, delegate(FastBinaryWriter w, List<int> e)
		{
			w.WriteList(e, delegate(FastBinaryWriter fastBinaryWriter, int value2)
			{
				fastBinaryWriter.Write(in value2, default(FastBinaryWriter.ForPrimitives));
			});
		});
		writer.WriteList(bDoubleList, delegate(FastBinaryWriter w, List<ToStringTestB> e)
		{
			w.WriteList(e, delegate(FastBinaryWriter writer2, ToStringTestB value2)
			{
				writer2.WriteIReadWrite(value2);
			});
		});
	}

	public virtual void Read(FastBinaryReader reader)
	{
		integer = reader.ReadInt32();
		nullableIntegerValue = reader.ReadNullable((FastBinaryReader r) => r.ReadInt32());
		nullableIntegerNull = reader.ReadNullable((FastBinaryReader r) => r.ReadInt32());
		enumeration = (ToStringTestEnum)reader.ReadInt32();
		nullableEnumerationValue = reader.ReadNullable((FastBinaryReader r) => (ToStringTestEnum)r.ReadInt32());
		nullableEnumerationNull = reader.ReadNullable((FastBinaryReader r) => (ToStringTestEnum)r.ReadInt32());
		text = reader.ReadString();
		textEmpty = reader.ReadString();
		textNull = reader.ReadString();
		intArray = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		intArrayEmpty = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		intArrayNull = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
		intList = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		intListEmpty = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		intListNull = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		intSet = reader.ReadHashSet((FastBinaryReader r) => r.ReadInt32());
		intSetEmpty = reader.ReadHashSet((FastBinaryReader r) => r.ReadInt32());
		intSetNull = reader.ReadHashSet((FastBinaryReader r) => r.ReadInt32());
		stringToIntDictionary = reader.ReadDictionary((FastBinaryReader kr) => kr.ReadString(), (FastBinaryReader vr) => vr.ReadInt32());
		stringToIntDictionaryEmpty = reader.ReadDictionary((FastBinaryReader kr) => kr.ReadString(), (FastBinaryReader vr) => vr.ReadInt32());
		stringToIntDictionaryNull = reader.ReadDictionary((FastBinaryReader kr) => kr.ReadString(), (FastBinaryReader vr) => vr.ReadInt32());
		bInstance = reader.ReadIReadWrite<ToStringTestB>();
		bInstanceNull = reader.ReadIReadWrite<ToStringTestB>();
		bArrayNull = reader.ReadArray((FastBinaryReader r) => r.ReadIReadWrite<ToStringTestB>());
		bListNull = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<ToStringTestB>());
		bSetNull = reader.ReadHashSet((FastBinaryReader r) => r.ReadIReadWrite<ToStringTestB>());
		bDictionaryNull = reader.ReadDictionary((FastBinaryReader kr) => kr.ReadString(), (FastBinaryReader vr) => vr.ReadIReadWrite<ToStringTestB>());
		bArrayEmpty = reader.ReadArray((FastBinaryReader r) => r.ReadIReadWrite<ToStringTestB>());
		bListEmpty = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<ToStringTestB>());
		bSetEmpty = reader.ReadHashSet((FastBinaryReader r) => r.ReadIReadWrite<ToStringTestB>());
		bDictionaryEmpty = reader.ReadDictionary((FastBinaryReader kr) => kr.ReadString(), (FastBinaryReader vr) => vr.ReadIReadWrite<ToStringTestB>());
		bArray = reader.ReadArray((FastBinaryReader r) => r.ReadIReadWrite<ToStringTestB>());
		bList = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<ToStringTestB>());
		bSet = reader.ReadHashSet((FastBinaryReader r) => r.ReadIReadWrite<ToStringTestB>());
		bDictionary = reader.ReadDictionary((FastBinaryReader kr) => kr.ReadString(), (FastBinaryReader vr) => vr.ReadIReadWrite<ToStringTestB>());
		intDoubleList = reader.ReadList((FastBinaryReader r) => r.ReadList((FastBinaryReader fastBinaryReader) => fastBinaryReader.ReadInt32()));
		bDoubleList = reader.ReadList((FastBinaryReader r) => r.ReadList((FastBinaryReader reader2) => reader2.ReadIReadWrite<ToStringTestB>()));
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("integer: " + $"{integer}");
		stringBuilder.AppendLine("nullableIntegerValue: " + ToStringHelper.Stringify(nullableIntegerValue, (int v) => $"{v}"));
		stringBuilder.AppendLine("nullableIntegerNull: " + ToStringHelper.Stringify(nullableIntegerNull, (int v) => $"{v}"));
		stringBuilder.AppendLine("enumeration: " + $"{enumeration}");
		stringBuilder.AppendLine("nullableEnumerationValue: " + ToStringHelper.Stringify(nullableEnumerationValue, (ToStringTestEnum v) => $"{v}"));
		stringBuilder.AppendLine("nullableEnumerationNull: " + ToStringHelper.Stringify(nullableEnumerationNull, (ToStringTestEnum v) => $"{v}"));
		stringBuilder.AppendLine("text: " + ToStringHelper.Stringify(text));
		stringBuilder.AppendLine("textEmpty: " + ToStringHelper.Stringify(textEmpty));
		stringBuilder.AppendLine("textNull: " + ToStringHelper.Stringify(textNull));
		stringBuilder.AppendLine("intArray: " + ToStringHelper.Stringify(intArray, (int e) => $"{e}"));
		stringBuilder.AppendLine("intArrayEmpty: " + ToStringHelper.Stringify(intArrayEmpty, (int e) => $"{e}"));
		stringBuilder.AppendLine("intArrayNull: " + ToStringHelper.Stringify(intArrayNull, (int e) => $"{e}"));
		stringBuilder.AppendLine("intList: " + ToStringHelper.Stringify(intList, (int e) => $"{e}"));
		stringBuilder.AppendLine("intListEmpty: " + ToStringHelper.Stringify(intListEmpty, (int e) => $"{e}"));
		stringBuilder.AppendLine("intListNull: " + ToStringHelper.Stringify(intListNull, (int e) => $"{e}"));
		stringBuilder.AppendLine("intSet: " + ToStringHelper.Stringify(intSet, (int e) => $"{e}"));
		stringBuilder.AppendLine("intSetEmpty: " + ToStringHelper.Stringify(intSetEmpty, (int e) => $"{e}"));
		stringBuilder.AppendLine("intSetNull: " + ToStringHelper.Stringify(intSetNull, (int e) => $"{e}"));
		stringBuilder.AppendLine("stringToIntDictionary: " + ToStringHelper.Stringify(stringToIntDictionary, (string k) => ToStringHelper.Stringify(k), (int v) => $"{v}"));
		stringBuilder.AppendLine("stringToIntDictionaryEmpty: " + ToStringHelper.Stringify(stringToIntDictionaryEmpty, (string k) => ToStringHelper.Stringify(k), (int v) => $"{v}"));
		stringBuilder.AppendLine("stringToIntDictionaryNull: " + ToStringHelper.Stringify(stringToIntDictionaryNull, (string k) => ToStringHelper.Stringify(k), (int v) => $"{v}"));
		stringBuilder.AppendLine("bInstance: " + ToStringHelper.Stringify(bInstance));
		stringBuilder.AppendLine("bInstanceNull: " + ToStringHelper.Stringify(bInstanceNull));
		stringBuilder.AppendLine("bArrayNull: " + ToStringHelper.Stringify(bArrayNull, (ToStringTestB e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("bListNull: " + ToStringHelper.Stringify(bListNull, (ToStringTestB e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("bSetNull: " + ToStringHelper.Stringify(bSetNull, (ToStringTestB e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("bDictionaryNull: " + ToStringHelper.Stringify(bDictionaryNull, (string k) => ToStringHelper.Stringify(k), (ToStringTestB v) => ToStringHelper.Stringify(v)));
		stringBuilder.AppendLine("bArrayEmpty: " + ToStringHelper.Stringify(bArrayEmpty, (ToStringTestB e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("bListEmpty: " + ToStringHelper.Stringify(bListEmpty, (ToStringTestB e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("bSetEmpty: " + ToStringHelper.Stringify(bSetEmpty, (ToStringTestB e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("bDictionaryEmpty: " + ToStringHelper.Stringify(bDictionaryEmpty, (string k) => ToStringHelper.Stringify(k), (ToStringTestB v) => ToStringHelper.Stringify(v)));
		stringBuilder.AppendLine("bArray: " + ToStringHelper.Stringify(bArray, (ToStringTestB e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("bList: " + ToStringHelper.Stringify(bList, (ToStringTestB e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("bSet: " + ToStringHelper.Stringify(bSet, (ToStringTestB e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("bDictionary: " + ToStringHelper.Stringify(bDictionary, (string k) => ToStringHelper.Stringify(k), (ToStringTestB v) => ToStringHelper.Stringify(v)));
		stringBuilder.AppendLine("intDoubleList: " + ToStringHelper.Stringify(intDoubleList, (List<int> e) => ToStringHelper.Stringify(e, (int num) => $"{num}")));
		stringBuilder.Append("bDoubleList: " + ToStringHelper.Stringify(bDoubleList, (List<ToStringTestB> e) => ToStringHelper.Stringify(e, (ToStringTestB value) => ToStringHelper.Stringify(value))));
		return stringBuilder.ToString();
	}
}
