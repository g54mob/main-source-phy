using Photon.Bolt.Exceptions;
using Photon.Bolt.Internal;
using UnityEngine;

namespace Photon.Bolt
{
	public class ProjectileHitUnitEvent : Event
	{
		public int ProjectileNetworkId
		{
			get
			{
				return Storage.Values[OffsetStorage].Int0;
			}
			set
			{
				value = Mathf.Clamp(value, 0, 65535);
				int @int = Storage.Values[OffsetStorage].Int0;
				Storage.Values[OffsetStorage].Int0 = value;
				if (!NetworkValue.Diff(@int, value))
				{
				}
			}
		}

		public int UnitSmallNetworkId
		{
			get
			{
				return Storage.Values[OffsetStorage + 1].Int0;
			}
			set
			{
				value = Mathf.Clamp(value, 0, 65535);
				int @int = Storage.Values[OffsetStorage + 1].Int0;
				Storage.Values[OffsetStorage + 1].Int0 = value;
				if (!NetworkValue.Diff(@int, value))
				{
				}
			}
		}

		public ProjectileHitUnitEvent()
			: base(ProjectileHitUnitEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[ProjectileHitUnitEvent ProjectileNetworkId={ProjectileNetworkId} UnitSmallNetworkId={UnitSmallNetworkId}]";
		}

		private static ProjectileHitUnitEvent Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!BoltCore.isServer)
			{
				throw new BoltException("You are not the server, you can not raise this event");
			}
			if (!(Factory.NewEvent(((IFactory)ProjectileHitUnitEvent_Meta.Instance).TypeKey) is ProjectileHitUnitEvent projectileHitUnitEvent))
			{
				return null;
			}
			projectileHitUnitEvent.Targets = targets;
			projectileHitUnitEvent.TargetConnection = connection;
			projectileHitUnitEvent.Reliability = reliability;
			return projectileHitUnitEvent;
		}

		public static ProjectileHitUnitEvent Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static ProjectileHitUnitEvent Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static ProjectileHitUnitEvent Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static ProjectileHitUnitEvent Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static ProjectileHitUnitEvent Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static ProjectileHitUnitEvent Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability, int ProjectileNetworkId, int UnitSmallNetworkId)
		{
			ProjectileHitUnitEvent projectileHitUnitEvent = Create(targets, connection, reliability);
			if (projectileHitUnitEvent == null)
			{
				return false;
			}
			projectileHitUnitEvent.ProjectileNetworkId = ProjectileNetworkId;
			projectileHitUnitEvent.UnitSmallNetworkId = UnitSmallNetworkId;
			projectileHitUnitEvent.Send();
			return true;
		}

		public static bool Post(GlobalTargets targets, int ProjectileNetworkId, int UnitSmallNetworkId)
		{
			return Post((byte)targets, null, ReliabilityModes.ReliableOrdered, ProjectileNetworkId, UnitSmallNetworkId);
		}

		public static bool Post(GlobalTargets targets, ReliabilityModes reliability, int ProjectileNetworkId, int UnitSmallNetworkId)
		{
			return Post((byte)targets, null, reliability, ProjectileNetworkId, UnitSmallNetworkId);
		}

		public static bool Post(BoltConnection connection, int ProjectileNetworkId, int UnitSmallNetworkId)
		{
			return Post(10, connection, ReliabilityModes.ReliableOrdered, ProjectileNetworkId, UnitSmallNetworkId);
		}

		public static bool Post(BoltConnection connection, ReliabilityModes reliability, int ProjectileNetworkId, int UnitSmallNetworkId)
		{
			return Post(10, connection, reliability, ProjectileNetworkId, UnitSmallNetworkId);
		}

		public static bool Post(int ProjectileNetworkId, int UnitSmallNetworkId)
		{
			return Post(2, null, ReliabilityModes.ReliableOrdered, ProjectileNetworkId, UnitSmallNetworkId);
		}

		public static bool Post(ReliabilityModes reliability, int ProjectileNetworkId, int UnitSmallNetworkId)
		{
			return Post(2, null, reliability, ProjectileNetworkId, UnitSmallNetworkId);
		}
	}
}
