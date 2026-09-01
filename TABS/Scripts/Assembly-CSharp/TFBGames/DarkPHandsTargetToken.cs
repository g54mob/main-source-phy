using Photon.Bolt;
using UdpKit;
using UnityEngine;

namespace TFBGames
{
	public class DarkPHandsTargetToken : IProtocolToken
	{
		public ushort PrimeTargetSmallNetworkId;

		public ushort TargetSmallNetworkId;

		public Vector3 PositionOrDirection;

		public DarkPHandsTargetToken()
		{
		}

		public DarkPHandsTargetToken(ushort primeTargetSmallNetworkId, ushort targetSmallNetworkId, Vector3 positionOrDirection)
		{
			PrimeTargetSmallNetworkId = primeTargetSmallNetworkId;
			TargetSmallNetworkId = targetSmallNetworkId;
			PositionOrDirection = positionOrDirection;
		}

		public void Write(UdpPacket packet)
		{
			bool flag = TargetSmallNetworkId != 0;
			float sqrMagnitude = PositionOrDirection.sqrMagnitude;
			bool flag2 = Mathf.Approximately(sqrMagnitude, 1f);
			bool flag3 = Mathf.Approximately(sqrMagnitude, 0f);
			packet.WriteBool(flag);
			packet.WriteBool(flag2);
			packet.WriteBool(flag3);
			packet.WriteUShort(PrimeTargetSmallNetworkId);
			if (flag)
			{
				packet.WriteUShort(TargetSmallNetworkId);
			}
			if (!flag3)
			{
				if (flag2)
				{
					packet.WriteTabsUnitVector(PositionOrDirection);
				}
				else
				{
					packet.WriteTabsPosition(PositionOrDirection);
				}
			}
		}

		public void Read(UdpPacket packet)
		{
			bool num = packet.ReadBool();
			bool flag = packet.ReadBool();
			bool flag2 = packet.ReadBool();
			PrimeTargetSmallNetworkId = packet.ReadUShort();
			if (num)
			{
				TargetSmallNetworkId = packet.ReadUShort();
			}
			if (flag2)
			{
				PositionOrDirection = Vector3.zero;
			}
			else
			{
				PositionOrDirection = (flag ? packet.ReadTabsUnitVector() : packet.ReadTabsPosition());
			}
		}
	}
}
