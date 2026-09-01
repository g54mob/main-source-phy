using System;
using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct AppData : IEquatable<AppId_t>, IEquatable<CGameID>, IEquatable<uint>, IEquatable<ulong>, IEquatable<AppData>, IComparable<AppData>, IComparable<AppId_t>, IComparable<uint>, IComparable<ulong>
	{
		[SerializeField]
		private uint id;

		public static AppData Me => default(AppData);

		public readonly AppId_t AppId => default(AppId_t);

		public readonly bool IsMe => false;

		public readonly uint Id => 0u;

		public readonly void OpenSteamStore(EOverlayToStoreFlag flag = EOverlayToStoreFlag.k_EOverlayToStoreFlag_None)
		{
		}

		public static AppData Get()
		{
			return default(AppData);
		}

		public static AppData Get(CGameID gameId)
		{
			return default(AppData);
		}

		public static AppData Get(ulong gameId)
		{
			return default(AppData);
		}

		public static AppData Get(uint appId)
		{
			return default(AppData);
		}

		public static AppData Get(AppId_t appId)
		{
			return default(AppData);
		}

		public static AppData Get(DlcData dlcData)
		{
			return default(AppData);
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

		public static bool operator ==(AppData l, AppData r)
		{
			return false;
		}

		public static bool operator ==(AppId_t l, AppData r)
		{
			return false;
		}

		public static bool operator ==(AppData l, AppId_t r)
		{
			return false;
		}

		public static bool operator !=(AppData l, AppData r)
		{
			return false;
		}

		public static bool operator !=(AppId_t l, AppData r)
		{
			return false;
		}

		public static bool operator !=(AppData l, AppId_t r)
		{
			return false;
		}

		public static implicit operator AppData(CGameID id)
		{
			return default(AppData);
		}

		public static implicit operator uint(AppData c)
		{
			return 0u;
		}

		public static implicit operator AppData(ulong id)
		{
			return default(AppData);
		}

		public static implicit operator AppData(uint id)
		{
			return default(AppData);
		}

		public static implicit operator AppId_t(AppData c)
		{
			return default(AppId_t);
		}

		public static implicit operator AppData(AppId_t id)
		{
			return default(AppData);
		}

		public static implicit operator AppData(GameData id)
		{
			return default(AppData);
		}
	}
}
