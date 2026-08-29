using System.Text;

public sealed class FinishLevelSwapPlaceSyncedPacket : Packet
{
	public NetPlayerId[] playerLocations;

	public override byte getTypeId()
	{
		return 118;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteArray(playerLocations, delegate(FastBinaryWriter w, NetPlayerId e)
		{
			w.WriteNetPlayerId(e);
		});
	}

	public override void readData(FastBinaryReader reader)
	{
		playerLocations = reader.ReadArray((FastBinaryReader r) => r.ReadNetPlayerId());
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("playerLocations: " + ToStringHelper.Stringify(playerLocations, (NetPlayerId e) => $"{e}"));
		return stringBuilder.ToString();
	}
}
