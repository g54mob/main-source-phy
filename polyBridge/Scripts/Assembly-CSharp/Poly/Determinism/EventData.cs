using System;

namespace Poly.Determinism
{
	[Serializable]
	public struct EventData
	{
		public EventType type;

		public int objectId;

		public int chronologicalIndex;

		public string objectName;

		public string dataString => "#" + objectId + " " + type;

		public EventData(LoggingBehaviour obj, EventType eventType, int chronologicalIndex)
		{
			if ((bool)obj)
			{
				objectId = obj.persistentId;
				objectName = obj.name;
			}
			else
			{
				objectId = -1;
				objectName = "null";
			}
			type = eventType;
			this.chronologicalIndex = chronologicalIndex;
		}

		public static bool operator ==(EventData a, EventData b)
		{
			return !(a != b);
		}

		public static bool operator !=(EventData a, EventData b)
		{
			if (a.type == b.type)
			{
				return a.objectId != b.objectId;
			}
			return true;
		}

		public override bool Equals(object other)
		{
			if (other is EventData)
			{
				return this == (EventData)other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return type.GetHashCode() ^ objectId.GetHashCode();
		}

		public static int Comparison(EventData a, EventData b)
		{
			int num = a.objectId - b.objectId;
			if (num == 0)
			{
				num = a.chronologicalIndex - b.chronologicalIndex;
			}
			return num;
		}
	}
}
