using System;
using System.Text;

[Serializable]
public class Switch3DData : IReadWrite
{
	public Switch3DType type;

	public float duration;

	public Interpolation interpolation;

	public bool autoplay;

	public float pauseOnLoop;

	public bool isAnimation;

	public RoomEditorLinksData onLinks;

	public RoomEditorLinksData offLinks;

	public virtual void Write(FastBinaryWriter writer)
	{
		int value = (int)type;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in duration, default(FastBinaryWriter.ForPrimitives));
		value = (int)interpolation;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in autoplay, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in pauseOnLoop, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isAnimation, default(FastBinaryWriter.ForPrimitives));
		writer.WriteIReadWrite(onLinks);
		writer.WriteIReadWrite(offLinks);
	}

	public virtual void Read(FastBinaryReader reader)
	{
		type = (Switch3DType)reader.ReadInt32();
		duration = reader.ReadSingle();
		interpolation = (Interpolation)reader.ReadInt32();
		autoplay = reader.ReadBoolean();
		pauseOnLoop = reader.ReadSingle();
		isAnimation = reader.ReadBoolean();
		onLinks = reader.ReadIReadWrite<RoomEditorLinksData>();
		offLinks = reader.ReadIReadWrite<RoomEditorLinksData>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("type: " + $"{type}");
		stringBuilder.AppendLine("duration: " + $"{duration}");
		stringBuilder.AppendLine("interpolation: " + $"{interpolation}");
		stringBuilder.AppendLine("autoplay: " + $"{autoplay}");
		stringBuilder.AppendLine("pauseOnLoop: " + $"{pauseOnLoop}");
		stringBuilder.AppendLine("isAnimation: " + $"{isAnimation}");
		stringBuilder.AppendLine("onLinks: " + ToStringHelper.Stringify(onLinks));
		stringBuilder.Append("offLinks: " + ToStringHelper.Stringify(offLinks));
		return stringBuilder.ToString();
	}
}
