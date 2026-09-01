using Landfall.TABS;
using Photon.Bolt;
using UdpKit;
using UnityEngine;

namespace TFBGames
{
	public class UnitSpawnToken : IProtocolToken
	{
		private const int TeamBits = 1;

		private const int MountSitIdBits = 4;

		private const int UnitSpawnSourceBits = 1;

		private const int PoolIndexBits = 4;

		public int UnitId;

		public int UnitModId;

		public int Team;

		public bool IsMounted;

		public ushort MountUnitSmallNetworkId;

		public int MountSitId;

		public ushort CopyOfUnitSmallNetworkId;

		public Vector3 CopyOfUnitSpawnPosition;

		public int SpawnSource;

		public short LinkToUnitInstanceId;

		public bool IsRiderWithLinkedMount;

		public short InstanceId;

		public ushort SmallNetworkId;

		public UnitPoolInfo? PoolInfo;

		public bool IsInPool => PoolInfo.HasValue;

		public UnitSpawnToken()
		{
		}

		public UnitSpawnToken(int unitId, int unitModId, Team team, bool isMounted, ushort mountUnitSmallNetworkId, int mountSitId, ushort copyOfUnitSmallNetworkId, Vector3 copyOfUnitSpawnPosition, UnitSpawnSource spawnSource, short linkToUnitInstanceId, bool isRiderWithLinkedMount, short instanceId, ushort smallNetworkId, UnitPoolInfo? poolInfo)
		{
			UnitId = unitId;
			UnitModId = unitModId;
			Team = (int)team;
			IsMounted = isMounted;
			MountUnitSmallNetworkId = mountUnitSmallNetworkId;
			MountSitId = mountSitId;
			CopyOfUnitSmallNetworkId = copyOfUnitSmallNetworkId;
			CopyOfUnitSpawnPosition = copyOfUnitSpawnPosition;
			SpawnSource = (int)spawnSource;
			LinkToUnitInstanceId = linkToUnitInstanceId;
			IsRiderWithLinkedMount = isRiderWithLinkedMount;
			InstanceId = instanceId;
			SmallNetworkId = smallNetworkId;
			PoolInfo = poolInfo;
		}

		public void Write(UdpPacket packet)
		{
			bool flag = CopyOfUnitSmallNetworkId != 0;
			bool flag2 = MountUnitSmallNetworkId != 0;
			packet.WriteBool(flag);
			packet.WriteBool(flag2);
			packet.WriteInt(UnitId);
			packet.WriteInt(UnitModId);
			packet.WriteInt(Team, 1);
			packet.WriteBool(IsMounted);
			packet.WriteInt(MountSitId, 4);
			packet.WriteInt(SpawnSource, 1);
			packet.WriteShort(LinkToUnitInstanceId);
			packet.WriteBool(IsRiderWithLinkedMount);
			packet.WriteShort(InstanceId);
			packet.WriteUShort(SmallNetworkId);
			packet.WriteBool(IsInPool);
			if (flag)
			{
				packet.WriteUShort(CopyOfUnitSmallNetworkId);
				packet.WriteTabsPosition(CopyOfUnitSpawnPosition);
			}
			if (flag2)
			{
				packet.WriteUShort(MountUnitSmallNetworkId);
			}
			if (IsInPool)
			{
				packet.WriteInt(PoolInfo.Value.PoolIndex, 4);
				packet.WriteShort(PoolInfo.Value.PoolId);
			}
		}

		public void Read(UdpPacket packet)
		{
			bool flag = packet.ReadBool();
			bool flag2 = packet.ReadBool();
			UnitId = packet.ReadInt();
			UnitModId = packet.ReadInt();
			Team = packet.ReadInt(1);
			IsMounted = packet.ReadBool();
			MountSitId = packet.ReadInt(4);
			SpawnSource = packet.ReadInt(1);
			LinkToUnitInstanceId = packet.ReadShort();
			IsRiderWithLinkedMount = packet.ReadBool();
			InstanceId = packet.ReadShort();
			SmallNetworkId = packet.ReadUShort();
			bool num = packet.ReadBool();
			if (flag)
			{
				CopyOfUnitSmallNetworkId = packet.ReadUShort();
				CopyOfUnitSpawnPosition = packet.ReadTabsPosition();
			}
			if (flag2)
			{
				MountUnitSmallNetworkId = packet.ReadUShort();
			}
			if (num)
			{
				int poolIndex = packet.ReadInt(4);
				short poolId = packet.ReadShort();
				PoolInfo = new UnitPoolInfo(poolIndex, poolId);
			}
		}
	}
}
