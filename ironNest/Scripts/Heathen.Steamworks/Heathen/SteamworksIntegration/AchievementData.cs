using System;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct AchievementData : IEquatable<AchievementData>, IEquatable<string>, IComparable<AchievementData>, IComparable<string>
	{
		[SerializeField]
		private string id;

		public readonly string Name => null;

		public readonly string Description => null;

		public readonly bool Hidden => false;

		public readonly string ApiName => null;

		public readonly bool IsAchieved
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public readonly DateTime? UnlockTime => null;

		public readonly float GlobalPercent => 0f;

		public readonly void Unlock()
		{
		}

		public readonly void Clear()
		{
		}

		public readonly void Unlock(UserData user)
		{
		}

		public readonly void Clear(UserData user)
		{
		}

		public readonly bool GetAchievementStatus(UserData user)
		{
			return false;
		}

		public readonly (bool, DateTime) GetAchievementAndUnlockTime(UserData user)
		{
			return default((bool, DateTime));
		}

		public readonly void GetIcon(Action<Texture2D> callback)
		{
		}

		public readonly void Store()
		{
		}

		public static AchievementData Get(string apiName)
		{
			return default(AchievementData);
		}

		public override readonly string ToString()
		{
			return null;
		}

		public readonly bool Equals(string other)
		{
			return false;
		}

		public readonly bool Equals(AchievementData other)
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

		public readonly int CompareTo(AchievementData other)
		{
			return 0;
		}

		public readonly int CompareTo(string other)
		{
			return 0;
		}

		public static bool operator ==(AchievementData l, AchievementData r)
		{
			return false;
		}

		public static bool operator ==(string l, AchievementData r)
		{
			return false;
		}

		public static bool operator ==(AchievementData l, string r)
		{
			return false;
		}

		public static bool operator !=(AchievementData l, AchievementData r)
		{
			return false;
		}

		public static bool operator !=(string l, AchievementData r)
		{
			return false;
		}

		public static bool operator !=(AchievementData l, string r)
		{
			return false;
		}

		public static implicit operator string(AchievementData c)
		{
			return null;
		}

		public static implicit operator AchievementData(string id)
		{
			return default(AchievementData);
		}
	}
}
