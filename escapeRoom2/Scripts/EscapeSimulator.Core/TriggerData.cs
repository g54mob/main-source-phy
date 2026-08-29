using System;
using System.Collections.Generic;
using System.Text;

[Serializable]
public class TriggerData : IReadWrite
{
	public List<InstanceID> keys;

	public bool anyObjectCanTrigger;

	public bool canPlayerTrigger;

	public bool triggerWhenAllPlayersEnter;

	public bool isSticky;

	public RoomEditorLinksData onEnter;

	public RoomEditorLinksData onExit;

	public RoomEditorLinksData onStart;

	public RoomEditorLinksData onEnd;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.WriteList(keys, delegate(FastBinaryWriter w, InstanceID e)
		{
			w.WriteIReadWrite(e);
		});
		writer.Write(in anyObjectCanTrigger, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in canPlayerTrigger, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in triggerWhenAllPlayersEnter, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isSticky, default(FastBinaryWriter.ForPrimitives));
		writer.WriteIReadWrite(onEnter);
		writer.WriteIReadWrite(onExit);
		writer.WriteIReadWrite(onStart);
		writer.WriteIReadWrite(onEnd);
	}

	public virtual void Read(FastBinaryReader reader)
	{
		keys = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<InstanceID>());
		anyObjectCanTrigger = reader.ReadBoolean();
		canPlayerTrigger = reader.ReadBoolean();
		triggerWhenAllPlayersEnter = reader.ReadBoolean();
		isSticky = reader.ReadBoolean();
		onEnter = reader.ReadIReadWrite<RoomEditorLinksData>();
		onExit = reader.ReadIReadWrite<RoomEditorLinksData>();
		onStart = reader.ReadIReadWrite<RoomEditorLinksData>();
		onEnd = reader.ReadIReadWrite<RoomEditorLinksData>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("keys: " + ToStringHelper.Stringify(keys, (InstanceID e) => ToStringHelper.Stringify(e)));
		stringBuilder.AppendLine("anyObjectCanTrigger: " + $"{anyObjectCanTrigger}");
		stringBuilder.AppendLine("canPlayerTrigger: " + $"{canPlayerTrigger}");
		stringBuilder.AppendLine("triggerWhenAllPlayersEnter: " + $"{triggerWhenAllPlayersEnter}");
		stringBuilder.AppendLine("isSticky: " + $"{isSticky}");
		stringBuilder.AppendLine("onEnter: " + ToStringHelper.Stringify(onEnter));
		stringBuilder.AppendLine("onExit: " + ToStringHelper.Stringify(onExit));
		stringBuilder.AppendLine("onStart: " + ToStringHelper.Stringify(onStart));
		stringBuilder.Append("onEnd: " + ToStringHelper.Stringify(onEnd));
		return stringBuilder.ToString();
	}
}
