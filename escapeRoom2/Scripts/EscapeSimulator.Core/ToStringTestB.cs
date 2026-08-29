using System.Text;

public class ToStringTestB : IReadWrite
{
	public int bInt;

	public string bString;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(in bInt, default(FastBinaryWriter.ForPrimitives));
		writer.Write(bString);
	}

	public virtual void Read(FastBinaryReader reader)
	{
		bInt = reader.ReadInt32();
		bString = reader.ReadString();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("bInt: " + $"{bInt}");
		stringBuilder.Append("bString: " + ToStringHelper.Stringify(bString));
		return stringBuilder.ToString();
	}
}
