using Photon.Bolt;
using UdpKit;

namespace TFBGames
{
	public class SyncProjectileEffectToken : IProtocolToken
	{
		public ushort TargetSmallNetworkId;

		public SyncProjectileEffectToken()
		{
		}

		public SyncProjectileEffectToken(ushort targetSmallNetworkId)
		{
			TargetSmallNetworkId = targetSmallNetworkId;
		}

		public void Write(UdpPacket packet)
		{
			packet.WriteUShort(TargetSmallNetworkId);
		}

		public void Read(UdpPacket packet)
		{
			TargetSmallNetworkId = packet.ReadUShort();
		}
	}
}
