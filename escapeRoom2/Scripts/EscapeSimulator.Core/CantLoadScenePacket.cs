using System.Collections.Generic;
using System.Text;

public sealed class CantLoadScenePacket : Packet
{
	public string sceneName;

	public List<NetPlayerId> invalidPlayerIds;

	public List<Game.CanLoadSceneResult> invalidPlayerResults;

	public override byte getTypeId()
	{
		return 20;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.Write(sceneName);
		writer.WriteList(invalidPlayerIds, delegate(FastBinaryWriter w, NetPlayerId e)
		{
			w.WriteNetPlayerId(e);
		});
		writer.WriteList(invalidPlayerResults, delegate(FastBinaryWriter w, Game.CanLoadSceneResult e)
		{
			int value = (int)e;
			w.Write(in value, default(FastBinaryWriter.ForPrimitives));
		});
	}

	public override void readData(FastBinaryReader reader)
	{
		sceneName = reader.ReadString();
		invalidPlayerIds = reader.ReadList((FastBinaryReader r) => r.ReadNetPlayerId());
		invalidPlayerResults = reader.ReadList((FastBinaryReader r) => (Game.CanLoadSceneResult)r.ReadInt32());
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("sceneName: " + ToStringHelper.Stringify(sceneName));
		stringBuilder.AppendLine("invalidPlayerIds: " + ToStringHelper.Stringify(invalidPlayerIds, (NetPlayerId e) => $"{e}"));
		stringBuilder.Append("invalidPlayerResults: " + ToStringHelper.Stringify(invalidPlayerResults, (Game.CanLoadSceneResult e) => $"{e}"));
		return stringBuilder.ToString();
	}
}
