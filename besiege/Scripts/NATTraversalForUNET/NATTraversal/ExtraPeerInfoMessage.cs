using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

namespace NATTraversal
{
	public class ExtraPeerInfoMessage : PeerInfoMessage
	{
		public ulong guid;

		public string internalIP;

		public string externalIPv6;

		public string internalIPv6;

		public override void Deserialize(NetworkReader reader)
		{
			base.Deserialize(reader);
			guid = reader.ReadPackedUInt64();
			internalIP = reader.ReadString();
			externalIPv6 = reader.ReadString();
			internalIPv6 = reader.ReadString();
		}

		public override void Serialize(NetworkWriter writer)
		{
			base.Serialize(writer);
			writer.WritePackedUInt64(guid);
			writer.Write(internalIP);
			writer.Write(externalIPv6);
			writer.Write(internalIPv6);
		}

		public override string ToString()
		{
			return base.ToString() + ", " + internalIP + ", " + externalIPv6 + ", " + internalIPv6 + ", " + guid;
		}
	}
}
