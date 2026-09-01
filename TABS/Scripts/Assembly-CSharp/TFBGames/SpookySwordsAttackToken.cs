using Photon.Bolt;
using UdpKit;

namespace TFBGames
{
	public class SpookySwordsAttackToken : IProtocolToken
	{
		private const int AttackIdBits = 4;

		public ushort TargetSmallNetworkId;

		public int AttackId;

		public SpookySwordsAttackToken()
		{
		}

		public SpookySwordsAttackToken(ushort targetSmallNetworkId, int attackId)
		{
			TargetSmallNetworkId = targetSmallNetworkId;
			AttackId = attackId;
		}

		public void Write(UdpPacket packet)
		{
			packet.WriteUShort(TargetSmallNetworkId);
			packet.WriteInt(AttackId, 4);
		}

		public void Read(UdpPacket packet)
		{
			TargetSmallNetworkId = packet.ReadUShort();
			AttackId = packet.ReadInt(4);
		}
	}
}
