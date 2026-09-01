using Photon.Bolt.Exceptions;
using Photon.Bolt.Internal;
using UnityEngine;

namespace Photon.Bolt
{
	public class PlaceUnitEvent : Event
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

		public bool IsCampaignUnit
		{
			get
			{
				return Storage.Values[OffsetStorage + 4].Bool;
			}
			set
			{
				bool a = Storage.Values[OffsetStorage + 4].Bool;
				Storage.Values[OffsetStorage + 4].Bool = value;
				if (!NetworkValue.Diff(a, value))
				{
				}
			}
		}

		public int UnitInstanceId
		{
			get
			{
				return Storage.Values[OffsetStorage + 5].Int0;
			}
			set
			{
				value = Mathf.Clamp(value, -32768, 32767);
				int @int = Storage.Values[OffsetStorage + 5].Int0;
				Storage.Values[OffsetStorage + 5].Int0 = value;
				if (!NetworkValue.Diff(@int, value))
				{
				}
			}
		}

		public PlaceUnitEvent()
			: base(PlaceUnitEvent_Meta.Instance)
		{
		}

		public override string ToString()
		{
			return $"[PlaceUnitEvent UnitId={UnitId} UnitModId={UnitModId} Position={Position} Rotation={Rotation} IsCampaignUnit={IsCampaignUnit} UnitInstanceId={UnitInstanceId}]";
		}

		private static PlaceUnitEvent Create(byte targets, BoltConnection connection, ReliabilityModes reliability)
		{
			if (!BoltCore.isClient)
			{
				throw new BoltException("You are not a client, you can not raise this event");
			}
			if (!(Factory.NewEvent(((IFactory)PlaceUnitEvent_Meta.Instance).TypeKey) is PlaceUnitEvent placeUnitEvent))
			{
				return null;
			}
			placeUnitEvent.Targets = targets;
			placeUnitEvent.TargetConnection = connection;
			placeUnitEvent.Reliability = reliability;
			return placeUnitEvent;
		}

		public static PlaceUnitEvent Create(GlobalTargets targets)
		{
			return Create((byte)targets, null, ReliabilityModes.ReliableOrdered);
		}

		public static PlaceUnitEvent Create(GlobalTargets targets, ReliabilityModes reliability)
		{
			return Create((byte)targets, null, reliability);
		}

		public static PlaceUnitEvent Create(BoltConnection connection)
		{
			return Create(10, connection, ReliabilityModes.ReliableOrdered);
		}

		public static PlaceUnitEvent Create(BoltConnection connection, ReliabilityModes reliability)
		{
			return Create(10, connection, reliability);
		}

		public static PlaceUnitEvent Create()
		{
			return Create(2, null, ReliabilityModes.ReliableOrdered);
		}

		public static PlaceUnitEvent Create(ReliabilityModes reliability)
		{
			return Create(2, null, reliability);
		}

		private static bool Post(byte targets, BoltConnection connection, ReliabilityModes reliability, int UnitId, int UnitModId, Vector3 Position, Quaternion Rotation, bool IsCampaignUnit, int UnitInstanceId)
		{
			PlaceUnitEvent placeUnitEvent = Create(targets, connection, reliability);
			if (placeUnitEvent == null)
			{
				return false;
			}
			placeUnitEvent.UnitId = UnitId;
			placeUnitEvent.UnitModId = UnitModId;
			placeUnitEvent.Position = Position;
			placeUnitEvent.Rotation = Rotation;
			placeUnitEvent.IsCampaignUnit = IsCampaignUnit;
			placeUnitEvent.UnitInstanceId = UnitInstanceId;
			placeUnitEvent.Send();
			return true;
		}

		public static bool Post(GlobalTargets targets, int UnitId, int UnitModId, Vector3 Position, Quaternion Rotation, bool IsCampaignUnit, int UnitInstanceId)
		{
			return Post((byte)targets, null, ReliabilityModes.ReliableOrdered, UnitId, UnitModId, Position, Rotation, IsCampaignUnit, UnitInstanceId);
		}

		public static bool Post(GlobalTargets targets, ReliabilityModes reliability, int UnitId, int UnitModId, Vector3 Position, Quaternion Rotation, bool IsCampaignUnit, int UnitInstanceId)
		{
			return Post((byte)targets, null, reliability, UnitId, UnitModId, Position, Rotation, IsCampaignUnit, UnitInstanceId);
		}

		public static bool Post(BoltConnection connection, int UnitId, int UnitModId, Vector3 Position, Quaternion Rotation, bool IsCampaignUnit, int UnitInstanceId)
		{
			return Post(10, connection, ReliabilityModes.ReliableOrdered, UnitId, UnitModId, Position, Rotation, IsCampaignUnit, UnitInstanceId);
		}

		public static bool Post(BoltConnection connection, ReliabilityModes reliability, int UnitId, int UnitModId, Vector3 Position, Quaternion Rotation, bool IsCampaignUnit, int UnitInstanceId)
		{
			return Post(10, connection, reliability, UnitId, UnitModId, Position, Rotation, IsCampaignUnit, UnitInstanceId);
		}

		public static bool Post(int UnitId, int UnitModId, Vector3 Position, Quaternion Rotation, bool IsCampaignUnit, int UnitInstanceId)
		{
			return Post(2, null, ReliabilityModes.ReliableOrdered, UnitId, UnitModId, Position, Rotation, IsCampaignUnit, UnitInstanceId);
		}

		public static bool Post(ReliabilityModes reliability, int UnitId, int UnitModId, Vector3 Position, Quaternion Rotation, bool IsCampaignUnit, int UnitInstanceId)
		{
			return Post(2, null, reliability, UnitId, UnitModId, Position, Rotation, IsCampaignUnit, UnitInstanceId);
		}
	}
}
