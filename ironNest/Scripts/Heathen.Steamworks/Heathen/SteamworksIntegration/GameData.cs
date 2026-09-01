using System;
using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct GameData : IEquatable<AppId_t>, IEquatable<CGameID>, IEquatable<uint>, IEquatable<ulong>, IEquatable<AppData>, IComparable<AppData>, IComparable<AppId_t>, IComparable<uint>, IComparable<ulong>
	{
		[SerializeField]
		private ulong id;

		public static GameData Me => default(GameData);

		public readonly CGameID GameId => default(CGameID);

		public readonly ulong Id => 0uL;

		public readonly AppData App => default(AppData);

		public readonly bool IsMod => false;

		public readonly bool IsP2PFile => false;

		public readonly bool IsShortcut => false;

		public readonly bool IsSteamApp => false;

		public readonly bool IsValid => false;

		public readonly uint ModID => 0u;

		public readonly bool IsMe => false;

		public readonly CGameID.EGameIDType Type => default(CGameID.EGameIDType);

		public static GameData Get()
		{
			return default(GameData);
		}

		public static GameData Get(CGameID gameId)
		{
			return default(GameData);
		}

		public static GameData Get(ulong gameId)
		{
			return default(GameData);
		}

		public static GameData Get(uint appId)
		{
			return default(GameData);
		}

		public static GameData Get(AppId_t appId)
		{
			return default(GameData);
		}

		public static void OpenSteamStore(AppData app, EOverlayToStoreFlag flag = EOverlayToStoreFlag.k_EOverlayToStoreFlag_None)
		{
		}

		public static void OpenMySteamStore(EOverlayToStoreFlag flag = EOverlayToStoreFlag.k_EOverlayToStoreFlag_None)
		{
		}

		public readonly int CompareTo(AppData other)
		{
			return 0;
		}

		public readonly int CompareTo(GameData other)
		{
			return 0;
		}

		public readonly int CompareTo(AppId_t other)
		{
			return 0;
		}

		public readonly int CompareTo(ulong other)
		{
			return 0;
		}

		public readonly int CompareTo(uint other)
		{
			return 0;
		}

		public override readonly string ToString()
		{
			return null;
		}

		public readonly bool Equals(AppData other)
		{
			return false;
		}

		public readonly bool Equals(GameData other)
		{
			return false;
		}

		public readonly bool Equals(AppId_t other)
		{
			return false;
		}

		public readonly bool Equals(uint other)
		{
			return false;
		}

		public readonly bool Equals(CGameID other)
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

		public static bool operator ==(GameData l, GameData r)
		{
			return false;
		}

		public static bool operator ==(AppId_t l, GameData r)
		{
			return false;
		}

		public static bool operator ==(GameData l, AppId_t r)
		{
			return false;
		}

		public static bool operator !=(GameData l, GameData r)
		{
			return false;
		}

		public static bool operator !=(AppId_t l, GameData r)
		{
			return false;
		}

		public static bool operator !=(GameData l, AppId_t r)
		{
			return false;
		}

		public static implicit operator GameData(CGameID id)
		{
			return default(GameData);
		}

		public static implicit operator uint(GameData c)
		{
			return 0u;
		}

		public static implicit operator ulong(GameData c)
		{
			return 0uL;
		}

		public static implicit operator GameData(ulong id)
		{
			return default(GameData);
		}

		public static implicit operator GameData(uint id)
		{
			return default(GameData);
		}

		public static implicit operator AppId_t(GameData c)
		{
			return default(AppId_t);
		}

		public static implicit operator GameData(AppId_t id)
		{
			return default(GameData);
		}
	}
}
