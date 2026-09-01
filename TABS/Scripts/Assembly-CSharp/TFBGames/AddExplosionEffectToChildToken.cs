using Photon.Bolt;
using UdpKit;

namespace TFBGames
{
	public class AddExplosionEffectToChildToken : IProtocolToken
	{
		public ushort TargetSmallNetworkId;

		public AddExplosionEffectToChildToken()
		{
		}

		public AddExplosionEffectToChildToken(ushort targetSmallNetworkId)
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
