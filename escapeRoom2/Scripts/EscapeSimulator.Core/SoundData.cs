using System;
using System.Text;

[Serializable]
public class SoundData : IReadWrite
{
	public string path;

	public string _event;

	public Sound.SoundType type;

	public bool isLoopable;

	public bool is2D;

	public float minDistance3D;

	public float maxDistance3D;

	public bool activeOnStart;

	public float volume;

	public bool localOnly;

	public Sound.FalloffMode falloffMode;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(path);
		writer.Write(_event);
		int value = (int)type;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isLoopable, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in is2D, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in minDistance3D, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in maxDistance3D, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in activeOnStart, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in volume, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in localOnly, default(FastBinaryWriter.ForPrimitives));
		value = (int)falloffMode;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		path = reader.ReadString();
		_event = reader.ReadString();
		type = (Sound.SoundType)reader.ReadInt32();
		isLoopable = reader.ReadBoolean();
		is2D = reader.ReadBoolean();
		minDistance3D = reader.ReadSingle();
		maxDistance3D = reader.ReadSingle();
		activeOnStart = reader.ReadBoolean();
		volume = reader.ReadSingle();
		localOnly = reader.ReadBoolean();
		falloffMode = (Sound.FalloffMode)reader.ReadInt32();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("path: " + ToStringHelper.Stringify(path));
		stringBuilder.AppendLine("_event: " + ToStringHelper.Stringify(_event));
		stringBuilder.AppendLine("type: " + $"{type}");
		stringBuilder.AppendLine("isLoopable: " + $"{isLoopable}");
		stringBuilder.AppendLine("is2D: " + $"{is2D}");
		stringBuilder.AppendLine("minDistance3D: " + $"{minDistance3D}");
		stringBuilder.AppendLine("maxDistance3D: " + $"{maxDistance3D}");
		stringBuilder.AppendLine("activeOnStart: " + $"{activeOnStart}");
		stringBuilder.AppendLine("volume: " + $"{volume}");
		stringBuilder.AppendLine("localOnly: " + $"{localOnly}");
		stringBuilder.Append("falloffMode: " + $"{falloffMode}");
		return stringBuilder.ToString();
	}
}
