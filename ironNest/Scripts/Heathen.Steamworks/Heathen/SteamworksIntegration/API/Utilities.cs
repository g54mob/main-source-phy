using System;
using Steamworks;
using Unity.Mathematics;
using UnityEngine;

namespace Heathen.SteamworksIntegration.API
{
	public static class Utilities
	{
		public static class Client
		{
			public static string IpCountry => null;

			public static uint SecondsSinceAppActive => 0u;

			public static DateTime ServerRealTime => default(DateTime);

			public static string SteamUILanguage => null;

			public static bool IsSteamInBigPictureMode => false;

			public static bool IsSteamRunningInVR => false;

			public static bool IsSteamRunningOnSteamDeck => false;

			public static bool IsVRHeadsetStreamingEnabled
			{
				get
				{
					return false;
				}
				set
				{
				}
			}

			public static void SetGameLauncherMode(bool mode)
			{
			}

			public static void StartVRDashboard()
			{
			}

			public static bool ShowVirtualKeyboard(EFloatingGamepadTextInputMode mode, int2 fieldPosition, int2 fieldSize)
			{
				return false;
			}

			public static bool ShowVirtualKeyboard(EFloatingGamepadTextInputMode mode, RectTransform fieldTransform, Canvas canvas)
			{
				return false;
			}

			public static bool ShowVirtualKeyboard(EFloatingGamepadTextInputMode mode, float2 fieldPosition, float2 fieldSize)
			{
				return false;
			}

			public static string GetPingLocationString()
			{
				return null;
			}

			public static int PingLocation(string locationString)
			{
				return 0;
			}

			public static int PingBetweenLocations(string fromLocationString, string toLocationString)
			{
				return 0;
			}
		}

		public static class Server
		{
			public static string GetPingLocationString()
			{
				return null;
			}

			public static int PingLocation(string locationString)
			{
				return 0;
			}

			public static int PingBetweenLocations(string fromLocationString, string toLocationString)
			{
				return 0;
			}
		}

		public static uint IPStringToUint(string address)
		{
			return 0u;
		}

		public static string IPUintToString(uint address)
		{
			return null;
		}

		public static byte[] IPStringToBytes(string address)
		{
			return null;
		}

		public static byte[] FlipImageBufferVertical(int width, int height, byte[] buffer)
		{
			return null;
		}

		public static bool FindToken(string openPattern, string closePattern, string input, out string result)
		{
			result = null;
			return false;
		}
	}
}
