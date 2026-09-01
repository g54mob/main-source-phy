using System;
using System.IO;
using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct DlcData : IEquatable<AppId_t>, IEquatable<uint>, IEquatable<AppData>, IComparable<AppData>, IComparable<AppId_t>, IComparable<uint>
	{
		[SerializeField]
		private uint id;

		public readonly AppId_t AppId => default(AppId_t);

		public readonly uint Id => 0u;

		public readonly bool Available => false;

		public readonly string Name => null;

		public readonly bool IsSubscribed => false;

		public readonly bool IsInstalled => false;

		public readonly DirectoryInfo InstallDirectory => null;

		public readonly float DownloadProgress => 0f;

		public readonly DateTime EarliestPurchaseTime => default(DateTime);

		public readonly void Install()
		{
		}

		public readonly void Uninstall()
		{
		}

		public readonly void OpenStore(EOverlayToStoreFlag flag = EOverlayToStoreFlag.k_EOverlayToStoreFlag_None)
		{
		}

		public DlcData(AppId_t id, bool available, string name)
		{
			this.id = 0u;
		}

		public static DlcData Get(uint appId)
		{
			return default(DlcData);
		}

		public static DlcData Get(AppId_t appId)
		{
			return default(DlcData);
		}

		public static DlcData Get(AppData appData)
		{
			return default(DlcData);
		}

		public override string ToString()
		{
			return null;
		}

		public readonly int CompareTo(AppData other)
		{
			return 0;
		}

		public readonly int CompareTo(AppId_t other)
		{
			return 0;
		}

		public readonly int CompareTo(uint other)
		{
			return 0;
		}

		public readonly bool Equals(uint other)
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

		public readonly bool Equals(AppId_t other)
		{
			return false;
		}

		public readonly bool Equals(AppData other)
		{
			return false;
		}

		public static bool operator ==(DlcData l, DlcData r)
		{
			return false;
		}

		public static bool operator ==(DlcData l, AppData r)
		{
			return false;
		}

		public static bool operator ==(DlcData l, AppId_t r)
		{
			return false;
		}

		public static bool operator ==(AppId_t l, DlcData r)
		{
			return false;
		}

		public static bool operator !=(DlcData l, DlcData r)
		{
			return false;
		}

		public static bool operator !=(DlcData l, AppData r)
		{
			return false;
		}

		public static bool operator !=(DlcData l, AppId_t r)
		{
			return false;
		}

		public static bool operator !=(AppId_t l, DlcData r)
		{
			return false;
		}

		public static implicit operator uint(DlcData c)
		{
			return 0u;
		}

		public static implicit operator DlcData(uint id)
		{
			return default(DlcData);
		}

		public static implicit operator AppId_t(DlcData c)
		{
			return default(AppId_t);
		}

		public static implicit operator DlcData(AppId_t id)
		{
			return default(DlcData);
		}

		public static implicit operator DlcData(AppData id)
		{
			return default(DlcData);
		}

		public static implicit operator AppData(DlcData id)
		{
			return default(AppData);
		}
	}
}
