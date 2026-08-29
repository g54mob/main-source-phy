using System;
using System.Text;

[Serializable]
public class ItemRespawnerData : IReadWrite
{
	public ItemRespawnVolume.RespawnMode respawnMode;

	public bool respawnWithPhysics = true;

	public InstanceID objectToRespawnTo;

	public virtual void Write(FastBinaryWriter writer)
	{
		int value = (int)respawnMode;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in respawnWithPhysics, default(FastBinaryWriter.ForPrimitives));
		writer.WriteIReadWrite(objectToRespawnTo);
	}

	public virtual void Read(FastBinaryReader reader)
	{
		respawnMode = (ItemRespawnVolume.RespawnMode)reader.ReadInt32();
		respawnWithPhysics = reader.ReadBoolean();
		objectToRespawnTo = reader.ReadIReadWrite<InstanceID>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("respawnMode: " + $"{respawnMode}");
		stringBuilder.AppendLine("respawnWithPhysics: " + $"{respawnWithPhysics}");
		stringBuilder.Append("objectToRespawnTo: " + ToStringHelper.Stringify(objectToRespawnTo));
		return stringBuilder.ToString();
	}
}
