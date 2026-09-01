using XMLTypes;

public abstract class XData
{
	public static bool Clamp;

	public string Key { get; private set; }

	public abstract object RawValue { get; }

	public bool IsArrayData
	{
		get
		{
			return this is XIntegerArray || this is XSingleArray || this is XStringArray;
		}
	}

	public abstract string Type { get; }

	protected XData(string key)
	{
		Key = key;
	}

	public abstract XAttribute[] Serialize();

	public abstract byte[] Encode();

	public abstract int Decode(byte[] data, int offset);
}
