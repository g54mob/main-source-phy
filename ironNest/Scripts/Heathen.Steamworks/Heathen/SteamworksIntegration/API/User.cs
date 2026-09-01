using System;
using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration.API
{
	public static class User
	{
		public static class Client
		{
			private static CallResult<StoreAuthURLResponse_t> _mStoreAuthURLResponseT;

			public static UserData Id => default(UserData);

			public static int Level => 0;

			public static StringKeyValuePair[] RichPresence
			{
				get
				{
					return null;
				}
				set
				{
				}
			}

			public static bool IsBehindNAT => false;

			public static bool IsPhoneIdentifying => false;

			public static bool IsPhoneRequiringVerification => false;

			public static bool IsPhoneVerified => false;

			public static bool IsTwoFactorEnabled => false;

			public static bool LoggedOn => false;

			[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
			private static void Init()
			{
			}

			public static void AdvertiseGame(CSteamID gameServerId, uint ip, ushort port)
			{
			}

			public static void AdvertiseGame(CSteamID gameServerId, string ip, ushort port)
			{
			}

			public static int GetGameBadgeLevel(int series, bool foil)
			{
				return 0;
			}

			public static void RequestStoreAuthURL(string redirectUrl, Action<StoreAuthURLResponse_t, bool> callback)
			{
			}

			public static bool SetRichPresence(string key, string value)
			{
				return false;
			}

			public static void ClearRichPresence()
			{
			}

			public static string GetRichPresence(string key)
			{
				return null;
			}
		}
	}
}
