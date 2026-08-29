using System;
using System.Text;

[Serializable]
public class RigidbodyData : IReadWrite
{
	public bool isKinematic;

	public float mass;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(in isKinematic, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in mass, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		isKinematic = reader.ReadBoolean();
		mass = reader.ReadSingle();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("isKinematic: " + $"{isKinematic}");
		stringBuilder.Append("mass: " + $"{mass}");
		return stringBuilder.ToString();
	}
}
