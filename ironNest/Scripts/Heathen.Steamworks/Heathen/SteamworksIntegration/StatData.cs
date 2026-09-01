using System;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct StatData : IEquatable<StatData>, IEquatable<string>, IComparable<StatData>, IComparable<string>
	{
		[SerializeField]
		private string id;

		public readonly string ApiName => null;

		public readonly float FloatValue()
		{
			return 0f;
		}

		public readonly float FloatValue(UserData user)
		{
			return 0f;
		}

		public readonly int IntValue()
		{
			return 0;
		}

		public readonly int IntValue(UserData user)
		{
			return 0;
		}

		public readonly void RequestUserStats(UserData user, Action<UserStatsReceived, bool> callback)
		{
		}

		public readonly bool GetValue(UserData user, out int value)
		{
			value = default(int);
			return false;
		}

		public readonly bool GetValue(UserData user, out float value)
		{
			value = default(float);
			return false;
		}

		public readonly void Set(float value)
		{
		}

		public readonly void Set(int value)
		{
		}

		public readonly void Set(float value, double length)
		{
		}

		public readonly void Store()
		{
		}

		public readonly void ServerSetValue(UserData user, int value)
		{
		}

		public readonly void ServerSetValue(UserData user, float value)
		{
		}

		public readonly bool ServerGetValue(UserData user, out int value)
		{
			value = default(int);
			return false;
		}

		public readonly bool ServerGetValue(UserData user, out float value)
		{
			value = default(float);
			return false;
		}

		public override readonly string ToString()
		{
			return null;
		}

		public readonly bool Equals(string other)
		{
			return false;
		}

		public readonly bool Equals(StatData other)
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

		public readonly int CompareTo(StatData other)
		{
			return 0;
		}

		public readonly int CompareTo(string other)
		{
			return 0;
		}

		public static bool operator ==(StatData l, StatData r)
		{
			return false;
		}

		public static bool operator ==(string l, StatData r)
		{
			return false;
		}

		public static bool operator ==(StatData l, string r)
		{
			return false;
		}

		public static bool operator !=(StatData l, StatData r)
		{
			return false;
		}

		public static bool operator !=(string l, StatData r)
		{
			return false;
		}

		public static bool operator !=(StatData l, string r)
		{
			return false;
		}

		public static implicit operator string(StatData c)
		{
			return null;
		}

		public static implicit operator StatData(string id)
		{
			return default(StatData);
		}
	}
}
