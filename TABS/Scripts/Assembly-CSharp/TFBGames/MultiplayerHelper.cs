using Landfall.TABS;
using Photon.Bolt;
using UdpKit;
using UnityEngine;

namespace TFBGames
{
	public static class MultiplayerHelper
	{
		private const float SpecialAngleUp = 400f;

		private const float SpecialAngleDown = 401f;

		private static readonly Vector3 PositionMin = new Vector3(-4000f, -2000f, -4000f);

		private static readonly Vector3 PositionMax = new Vector3(3000f, 3000f, 3000f);

		private static readonly Vector3 PositionAccuracy = new Vector3(0.01f, 0.01f, 0.01f);

		private static Vector3? m_positionMultiplier;

		private static Vector3Int? m_positionBits;

		private static Quaternion m_ninetyAroundUp = Quaternion.AngleAxis(90f, Vector3.up);

		public static bool IsCorrectTeamToEdit(Team team, bool forRemotePlayer, Team serverTeam, Team clientTeam)
		{
			if (forRemotePlayer)
			{
				if (!BoltNetwork.IsServer || team != clientTeam)
				{
					if (BoltNetwork.IsClient)
					{
						return team == serverTeam;
					}
					return false;
				}
				return true;
			}
			if (!BoltNetwork.IsServer || team != serverTeam)
			{
				if (BoltNetwork.IsClient)
				{
					return team == clientTeam;
				}
				return false;
			}
			return true;
		}

		public static NetworkUnit GetNetworkUnit(NetworkId networkId)
		{
			foreach (BoltEntity entity in BoltNetwork.Entities)
			{
				if (entity.StateIs<IUnitState>() && entity.NetworkId == networkId)
				{
					return entity.GetComponent<NetworkUnit>();
				}
			}
			return null;
		}

		public static NetworkUnit GetNetworkUnit(ulong networkId)
		{
			return GetNetworkUnit(new NetworkId(networkId));
		}

		public static void WriteTabsPosition(this UdpPacket packet, Vector3 position)
		{
			Vector3 positionMultiplier = GetPositionMultiplier();
			Vector3Int positionBits = GetPositionBits();
			Vector3Int vector3Int = new Vector3Int((int)((position.x - PositionMin.x) * positionMultiplier.x), (int)((position.y - PositionMin.y) * positionMultiplier.y), (int)((position.z - PositionMin.z) * positionMultiplier.z));
			packet.WriteInt(vector3Int.x, positionBits.x);
			packet.WriteInt(vector3Int.y, positionBits.y);
			packet.WriteInt(vector3Int.z, positionBits.z);
		}

		public static Vector3 ReadTabsPosition(this UdpPacket packet)
		{
			Vector3 positionMultiplier = GetPositionMultiplier();
			Vector3Int positionBits = GetPositionBits();
			Vector3Int vector3Int = new Vector3Int(packet.ReadInt(positionBits.x), packet.ReadInt(positionBits.y), packet.ReadInt(positionBits.z));
			return new Vector3((float)vector3Int.x / positionMultiplier.x + PositionMin.x, (float)vector3Int.y / positionMultiplier.y + PositionMin.y, (float)vector3Int.z / positionMultiplier.z + PositionMin.z);
		}

		public static void WriteTabsUnitVector(this UdpPacket packet, Vector3 vector)
		{
			if (vector == Vector3.up)
			{
				packet.WriteFloat(400f);
				packet.WriteFloat(400f);
				return;
			}
			if (vector == Vector3.down)
			{
				packet.WriteFloat(401f);
				packet.WriteFloat(401f);
				return;
			}
			Vector3 normalized = Vector3.ProjectOnPlane(vector, Vector3.up).normalized;
			Vector3 axis = m_ninetyAroundUp * normalized;
			float value = Vector3.SignedAngle(Vector3.forward, normalized, Vector3.up);
			float value2 = Vector3.SignedAngle(normalized, vector, axis);
			packet.WriteFloat(value);
			packet.WriteFloat(value2);
		}

		public static Vector3 ReadTabsUnitVector(this UdpPacket packet)
		{
			float num = packet.ReadFloat();
			float angle = packet.ReadFloat();
			if (Mathf.Approximately(num, 400f))
			{
				return Vector3.up;
			}
			if (Mathf.Approximately(num, 401f))
			{
				return Vector3.down;
			}
			Vector3 vector = Quaternion.AngleAxis(num, Vector3.up) * Vector3.forward;
			Vector3 axis = m_ninetyAroundUp * vector;
			return Quaternion.AngleAxis(angle, axis) * vector;
		}

		public static void WriteTabsQuaternion(this UdpPacket packet, Quaternion quaternion)
		{
			Vector3 vector = quaternion * Vector3.forward;
			packet.WriteTabsUnitVector(vector);
		}

		public static Quaternion ReadTabsQuaternion(this UdpPacket packet)
		{
			Vector3 toDirection = packet.ReadTabsUnitVector();
			return Quaternion.FromToRotation(Vector3.forward, toDirection);
		}

		private static Vector3 GetPositionMultiplier()
		{
			if (m_positionMultiplier.HasValue)
			{
				return m_positionMultiplier.Value;
			}
			m_positionMultiplier = new Vector3(1f / PositionAccuracy.x, 1f / PositionAccuracy.y, 1f / PositionAccuracy.z);
			return m_positionMultiplier.Value;
		}

		private static Vector3Int GetPositionBits()
		{
			if (m_positionBits.HasValue)
			{
				return m_positionBits.Value;
			}
			Vector3 positionMultiplier = GetPositionMultiplier();
			Vector3 vector = new Vector3(PositionMax.x - PositionMin.x, PositionMax.y - PositionMin.y, PositionMax.z - PositionMin.z);
			Vector3 vector2 = new Vector3(vector.x * positionMultiplier.x, vector.y * positionMultiplier.y, vector.z * positionMultiplier.z);
			m_positionBits = new Vector3Int(Mathf.CeilToInt(Mathf.Log(vector2.x, 2f)), Mathf.CeilToInt(Mathf.Log(vector2.y, 2f)), Mathf.CeilToInt(Mathf.Log(vector2.z, 2f)));
			return m_positionBits.Value;
		}
	}
}
