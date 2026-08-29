using System;
using System.Text;

[Serializable]
public class TokenData : IReadWrite
{
	public int id;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(in id, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		id = reader.ReadInt32();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("id: " + $"{id}");
		return stringBuilder.ToString();
	}
}
