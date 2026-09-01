using Photon.Bolt;
using Photon.Bolt.Utils;
using UdpKit;
using UnityEngine;

namespace TFBGames
{
	public class ProjectileSpawnToken : IProtocolToken
	{
		public const int RandomSeedMax = 31;

		private const int PrefabIndexBits = 7;

		private const int WeaponIndexBits = 4;

		private const int RandomSeedBits = 5;

		public int PrefabIndex;

		public ushort UnitSmallNetworkId;

		public byte WeaponIndex;

		public Vector3 SpawnPosition;

		public Quaternion SpawnRotation;

		public Vector3 SpawnDirection;

		public Vector3 DirectionToTarget;

		public ushort TargetSmallNetworkId;

		public Vector3 ShootPositionForward;

		public Vector3 TargetPosition;

		public Vector3 TargetVelocity;

		public ushort? NetworkId;

		public byte? RandomSeed;

		public ProjectileSpawnToken()
		{
		}

		public ProjectileSpawnToken(int prefabIndex, ushort unitSmallNetworkId, byte weaponIndex, Vector3 spawnPosition, Quaternion spawnRotation, Vector3 spawnDirection, Vector3 directionToTarget, ushort targetSmallNetworkId, Vector3 shootPositionForward, Vector3 targetPosition, Vector3 targetVelocity, ushort? networkId, byte? randomSeed)
		{
			PrefabIndex = prefabIndex;
			UnitSmallNetworkId = unitSmallNetworkId;
			WeaponIndex = weaponIndex;
			SpawnPosition = spawnPosition;
			SpawnRotation = spawnRotation;
			SpawnDirection = spawnDirection;
			DirectionToTarget = directionToTarget;
			TargetSmallNetworkId = targetSmallNetworkId;
			ShootPositionForward = shootPositionForward;
			TargetPosition = targetPosition;
			TargetVelocity = targetVelocity;
			NetworkId = networkId;
			RandomSeed = randomSeed;
		}

		public void Write(UdpPacket packet)
		{
			bool flag = WeaponIndex > 0;
			bool hasValue = NetworkId.HasValue;
			bool hasValue2 = RandomSeed.HasValue;
			packet.WriteBool(flag);
			packet.WriteBool(hasValue);
			packet.WriteBool(hasValue2);
			packet.WriteInt(PrefabIndex, 7);
			packet.WriteUShort(UnitSmallNetworkId);
			if (flag)
			{
				packet.WriteByte(WeaponIndex, 4);
			}
			packet.WriteTabsPosition(SpawnPosition);
			packet.WriteTabsQuaternion(SpawnRotation);
			packet.WriteTabsUnitVector(SpawnDirection);
			packet.WriteTabsUnitVector(DirectionToTarget);
			packet.WriteUShort(TargetSmallNetworkId);
			packet.WriteTabsUnitVector(ShootPositionForward);
			packet.WriteTabsPosition(TargetPosition);
			packet.WriteVector3(TargetVelocity);
			if (hasValue)
			{
				packet.WriteUShort(NetworkId.Value);
			}
			if (hasValue2)
			{
				packet.WriteByte(RandomSeed.Value, 5);
			}
		}

		public void Read(UdpPacket packet)
		{
			bool flag = packet.ReadBool();
			bool flag2 = packet.ReadBool();
			bool flag3 = packet.ReadBool();
			PrefabIndex = packet.ReadInt(7);
			UnitSmallNetworkId = packet.ReadUShort();
			WeaponIndex = (byte)(flag ? packet.ReadByte(4) : 0);
			SpawnPosition = packet.ReadTabsPosition();
			SpawnRotation = packet.ReadTabsQuaternion();
			SpawnDirection = packet.ReadTabsUnitVector();
			DirectionToTarget = packet.ReadTabsUnitVector();
			TargetSmallNetworkId = packet.ReadUShort();
			ShootPositionForward = packet.ReadTabsUnitVector();
			TargetPosition = packet.ReadTabsPosition();
			TargetVelocity = packet.ReadVector3();
			NetworkId = (flag2 ? new ushort?(packet.ReadUShort()) : ((ushort?)null));
			RandomSeed = (flag3 ? new byte?(packet.ReadByte(5)) : ((byte?)null));
		}
	}
}
