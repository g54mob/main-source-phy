using System;
using System.Collections.Generic;
using System.Text;

[Serializable]
public class CustomModelData : IReadWrite
{
	public bool disableGeneratedCollider;

	public List<string> usedFiles;

	public int currentAnimation;

	public CustomModelColliderType colliderType;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(in disableGeneratedCollider, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(usedFiles, delegate(FastBinaryWriter w, string e)
		{
			w.Write(e);
		});
		writer.Write(in currentAnimation, default(FastBinaryWriter.ForPrimitives));
		int value = (int)colliderType;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		disableGeneratedCollider = reader.ReadBoolean();
		usedFiles = reader.ReadList((FastBinaryReader r) => r.ReadString());
		currentAnimation = reader.ReadInt32();
		colliderType = (CustomModelColliderType)reader.ReadInt32();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("disableGeneratedCollider: " + $"{disableGeneratedCollider}");
		stringBuilder.AppendLine("usedFiles: " + ToStringHelper.Stringify(usedFiles, (string e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("currentAnimation: " + $"{currentAnimation}");
		stringBuilder.Append("colliderType: " + $"{colliderType}");
		return stringBuilder.ToString();
	}
}
