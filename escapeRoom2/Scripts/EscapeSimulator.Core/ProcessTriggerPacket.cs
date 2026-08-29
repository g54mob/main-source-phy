using System.Collections.Generic;
using System.Text;

public sealed class ProcessTriggerPacket : Packet
{
	public Trigger trigger;

	public TriggerEventType triggerEventType;

	public HashSet<Interactive> interactives;

	public HashSet<NetPlayerId> players;

	public override byte getTypeId()
	{
		return 97;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(trigger);
		int value = (int)triggerEventType;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.WriteHashSet(interactives, delegate(FastBinaryWriter w, Interactive e)
		{
			w.WriteComponent(e);
		});
		writer.WriteHashSet(players, delegate(FastBinaryWriter w, NetPlayerId e)
		{
			w.WriteNetPlayerId(e);
		});
	}

	public override void readData(FastBinaryReader reader)
	{
		trigger = reader.ReadComponent<Trigger>();
		triggerEventType = (TriggerEventType)reader.ReadInt32();
		interactives = reader.ReadHashSet((FastBinaryReader r) => r.ReadComponent<Interactive>());
		players = reader.ReadHashSet((FastBinaryReader r) => r.ReadNetPlayerId());
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("trigger: " + $"{trigger}");
		stringBuilder.AppendLine("triggerEventType: " + $"{triggerEventType}");
		stringBuilder.AppendLine("interactives: " + ToStringHelper.Stringify(interactives, (Interactive e) => $"{e}"));
		stringBuilder.Append("players: " + ToStringHelper.Stringify(players, (NetPlayerId e) => $"{e}"));
		return stringBuilder.ToString();
	}
}
