using System;
using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct TimelineEventData : IEquatable<TimelineEventHandle_t>, IEquatable<ulong>, IComparable<TimelineEventHandle_t>, IComparable<ulong>
	{
		[SerializeField]
		private TimelineEventHandle_t handle;

		public readonly TimelineEventHandle_t Handle => default(TimelineEventHandle_t);

		public readonly ulong Id => 0uL;

		public readonly TimelineEventDataArguments Arguments => default(TimelineEventDataArguments);

		public readonly int CompareTo(TimelineEventData other)
		{
			return 0;
		}

		public readonly int CompareTo(TimelineEventHandle_t other)
		{
			return 0;
		}

		public readonly int CompareTo(ulong other)
		{
			return 0;
		}

		public override readonly string ToString()
		{
			return null;
		}

		public readonly bool Equals(TimelineEventData other)
		{
			return false;
		}

		public readonly bool Equals(TimelineEventHandle_t other)
		{
			return false;
		}

		public readonly bool Equals(ulong other)
		{
			return false;
		}

		public override readonly bool Equals(object obj)
		{
			return false;
		}

		public override readonly int GetHashCode()
		{
			return 0;
		}

		public static bool operator ==(TimelineEventData l, TimelineEventData r)
		{
			return false;
		}

		public static bool operator ==(TimelineEventHandle_t l, TimelineEventData r)
		{
			return false;
		}

		public static bool operator ==(TimelineEventData l, TimelineEventHandle_t r)
		{
			return false;
		}

		public static bool operator !=(TimelineEventData l, TimelineEventData r)
		{
			return false;
		}

		public static bool operator !=(TimelineEventHandle_t l, TimelineEventData r)
		{
			return false;
		}

		public static bool operator !=(TimelineEventData l, TimelineEventHandle_t r)
		{
			return false;
		}

		public static implicit operator TimelineEventData(TimelineEventHandle_t value)
		{
			return default(TimelineEventData);
		}

		public static implicit operator ulong(TimelineEventData c)
		{
			return 0uL;
		}

		public static implicit operator TimelineEventData(ulong id)
		{
			return default(TimelineEventData);
		}

		public static implicit operator TimelineEventHandle_t(TimelineEventData c)
		{
			return default(TimelineEventHandle_t);
		}
	}
}
