using UnityEngine.Networking;

namespace NATTraversal
{
	public class ConnectionInfoMessage : MessageBase
	{
		public string clientGUID;

		public ulong raknetGUID;

		public string externalIP;

		public string internalIP;

		public string externalIPv6;

		public string internalIPv6;

		public ConnectionType connectionType;

		public ConnectionInfoMessage()
		{
		}

		public ConnectionInfoMessage(string clientGuid, ulong raknetGUID, string externalIP, string internalIP, string externalIPv6, string internalIPv6, ConnectionType connectionType)
		{
			clientGUID = clientGuid;
			this.raknetGUID = raknetGUID;
			this.externalIP = externalIP;
			this.internalIP = internalIP;
			this.externalIPv6 = externalIPv6;
			this.internalIPv6 = internalIPv6;
			this.connectionType = connectionType;
		}

		public override void Deserialize(NetworkReader reader)
		{
			base.Deserialize(reader);
			clientGUID = reader.ReadString();
			raknetGUID = reader.ReadPackedUInt64();
			externalIP = reader.ReadString();
			internalIP = reader.ReadString();
			externalIPv6 = reader.ReadString();
			internalIPv6 = reader.ReadString();
			connectionType = (ConnectionType)reader.ReadInt32();
		}

		public override void Serialize(NetworkWriter writer)
		{
			base.Serialize(writer);
			writer.Write(clientGUID);
			writer.WritePackedUInt64(raknetGUID);
			writer.Write(externalIP);
			writer.Write(internalIP);
			writer.Write(externalIPv6);
			writer.Write(internalIPv6);
			writer.Write((int)connectionType);
		}

		public override string ToString()
		{
			return clientGUID + ", " + raknetGUID + ", " + externalIP + ", " + internalIP + ", " + externalIPv6 + ", " + internalIPv6 + ", " + connectionType;
		}
	}
}
