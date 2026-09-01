using Photon.Bolt;
using UdpKit;
using UnityEngine;

namespace TFBGames
{
	public class DebugSyncCameraToken : IProtocolToken
	{
		public bool? EnableSync;

		public Vector3? Position;

		public DebugSyncCameraToken()
		{
		}

		public DebugSyncCameraToken(bool? enableSync, Vector3? position)
		{
			EnableSync = enableSync;
			Position = position;
		}

		public void Write(UdpPacket packet)
		{
			bool hasValue = EnableSync.HasValue;
			bool hasValue2 = Position.HasValue;
			packet.WriteBool(hasValue);
			packet.WriteBool(hasValue2);
			if (hasValue)
			{
				packet.WriteBool(EnableSync.Value);
			}
			if (hasValue2)
			{
				packet.WriteTabsPosition(Position.Value);
			}
		}

		public void Read(UdpPacket packet)
		{
			bool num = packet.ReadBool();
			bool flag = packet.ReadBool();
			if (num)
			{
				EnableSync = packet.ReadBool();
			}
			if (flag)
			{
				Position = packet.ReadTabsPosition();
			}
		}
	}
}
