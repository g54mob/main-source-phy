using Photon.Bolt.Exceptions;
using Photon.Bolt.Internal;
using UnityEngine;

namespace Photon.Bolt
{
	public class SpawnUnitEvent : Event
	{
		public int UnitId
		{
			get
			{
				return Storage.Values[OffsetStorage].Int0;
			}
			set
			{
				int @int = Storage.Values[OffsetStorage].Int0;
				Storage.Values[OffsetStorage].Int0 = value;
				if (!NetworkValue.Diff(@int, value))
				{
				}
			}
		}

		public int UnitModId
		{
			get
			{
				return Storage.Values[OffsetStorage + 1].Int0;
			}
			set
			{
				int @int = Storage.Values[OffsetStorage + 1].Int0;
				Storage.Values[OffsetStorage + 1].Int0 = value;
				if (!NetworkValue.Diff(@int, value))
				{
				}
			}
		}

		public Vector3 Position
		{
			get
			{
				return Storage.Values[OffsetStorage + 2].Vector3;
			}
			set
			{
				Vector3 vector = Storage.Values[OffsetStorage + 2].Vector3;
				Storage.Values[OffsetStorage + 2].Vector3 = value;
				if (!NetworkValue.Diff(vector, value))
				{
				}
			}
		}

		public Quaternion Rotation
		{
			get
			{
				return Storage.Values[OffsetStorage + 3].Quaternion;
			}
			set
			{
				Quaternion quaternion = Storage.Values[OffsetStorage + 3].Quaternion;
				Storage.Values[OffsetStorage + 3].Quaternion = value;
				if (!NetworkValue.Diff(quaternion, value))
				{
				}
			}
		}

		public int UnitInstanceId
		{
			get
			{
				return Storage.Values[OffsetStorage + 4].Int0;
			}
			set
			{
				value = Mathf.Clamp(value, -32768, 32767);
				int @int = Storage.Values[OffsetStorage + 4].Int0;
				Storage.Values[OffsetStorage + 4].Int0 = value;
				if (!NetworkValue.Diff(@int, value))
				{
				}
			}
		}

		public SpawnUnitEvent()
			: base(SpawnUnitEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[SpawnUnitEvent UnitId={UnitId} UnitModId={UnitModId} Position={Position} Rotation={Rotation} UnitInstanceId={UnitInstanceId}]";
		}

		private static SpawnUnitEvent Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!BoltCore.isClient)
			{
				throw new BoltException("You are not a client, you can not raise this event");
			}
			if (!(Factory.NewEvent(((IFactory)SpawnUnitEvent_Meta.Instance).TypeKey) is SpawnUnitEvent spawnUnitEvent))
			{
				return null;
			}
			spawnUnitEvent.Targets = targets;
			spawnUnitEvent.TargetConnection = connection;
			spawnUnitEvent.Reliability = reliability;
			return spawnUnitEvent;
		}

		public static SpawnUnitEvent Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static SpawnUnitEvent Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static SpawnUnitEvent Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static SpawnUnitEvent Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static SpawnUnitEvent Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static SpawnUnitEvent Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability, int UnitId, int UnitModId, Vector3 Position, Quaternion Rotation, int UnitInstanceId)
		{
			SpawnUnitEvent spawnUnitEvent = Create(targets, connection, reliability);
			if (spawnUnitEvent == null)
			{
				return false;
			}
			spawnUnitEvent.UnitId = UnitId;
			spawnUnitEvent.UnitModId = UnitModId;
			spawnUnitEvent.Position = Position;
			spawnUnitEvent.Rotation = Rotation;
			spawnUnitEvent.UnitInstanceId = UnitInstanceId;
			spawnUnitEvent.Send();
			return true;
		}

		public static bool Post(GlobalTargets targets, int UnitId, int UnitModId, Vector3 Position, Quaternion Rotation, int UnitInstanceId)
		{
			return Post((byte)targets, null, ReliabilityModes.ReliableOrdered, UnitId, UnitModId, Position, Rotation, UnitInstanceId);
		}

		public static bool Post(GlobalTargets targets, ReliabilityModes reliability, int UnitId, int UnitModId, Vector3 Position, Quaternion Rotation, int UnitInstanceId)
		{
			return Post((byte)targets, null, reliability, UnitId, UnitModId, Position, Rotation, UnitInstanceId);
		}

		public static bool Post(BoltConnection connection, int UnitId, int UnitModId, Vector3 Position, Quaternion Rotation, int UnitInstanceId)
		{
			return Post(10, connection, ReliabilityModes.ReliableOrdered, UnitId, UnitModId, Position, Rotation, UnitInstanceId);
		}

		public static bool Post(BoltConnection connection, ReliabilityModes reliability, int UnitId, int UnitModId, Vector3 Position, Quaternion Rotation, int UnitInstanceId)
		{
			return Post(10, connection, reliability, UnitId, UnitModId, Position, Rotation, UnitInstanceId);
		}

		public static bool Post(int UnitId, int UnitModId, Vector3 Position, Quaternion Rotation, int UnitInstanceId)
		{
			return Post(2, null, ReliabilityModes.ReliableOrdered, UnitId, UnitModId, Position, Rotation, UnitInstanceId);
		}

		public static bool Post(ReliabilityModes reliability, int UnitId, int UnitModId, Vector3 Position, Quaternion Rotation, int UnitInstanceId)
		{
			return Post(2, null, reliability, UnitId, UnitModId, Position, Rotation, UnitInstanceId);
		}
	}
}
