using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

namespace NATTraversal
{
	public class ExtraPeerInfoListMessage : PeerListMessage
	{
		public override void Deserialize(NetworkReader reader)
		{
			oldServerConnectionId = (int)reader.ReadPackedUInt32();
			int num = reader.ReadUInt16();
			peers = new PeerInfoMessage[num];
			for (int i = 0; i < peers.Length; i++)
			{
				ExtraPeerInfoMessage extraPeerInfoMessage = new ExtraPeerInfoMessage();
				extraPeerInfoMessage.Deserialize(reader);
				peers[i] = extraPeerInfoMessage;
				peers[i] = extraPeerInfoMessage;
			}
		}

		public override void Serialize(NetworkWriter writer)
		{
			writer.WritePackedUInt32((uint)oldServerConnectionId);
			writer.Write((ushort)peers.Length);
			for (int i = 0; i < peers.Length; i++)
			{
				((ExtraPeerInfoMessage)peers[i]).Serialize(writer);
			}
		}
	}
}
