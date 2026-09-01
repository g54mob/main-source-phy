using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration.API
{
	public static class Parties
	{
		public static class Client
		{
			private static CallResult<CreateBeaconCallback_t> _createBeaconCallbackT;

			private static CallResult<ChangeNumOpenSlotsCallback_t> _changeNumOpenSlotsCallbackT;

			private static CallResult<JoinPartyCallback_t> _joinPartyCallbackT;

			internal static List<ReservationNotificationCallback_t> ReservationList;

			private static List<PartyBeaconID_t> _createdBeacons;

			public static PartyBeaconID_t[] MyBeacons => null;

			public static ReservationNotificationCallback_t[] Reservations => null;

			[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
			private static void Init()
			{
			}

			public static SteamPartyBeaconLocation_t[] GetAvailableBeaconLocations()
			{
				return null;
			}

			public static void CreateBeacon(uint openSlots, ref SteamPartyBeaconLocation_t location, string connectionString, string metadata, Action<CreateBeaconCallback_t, bool> callback)
			{
			}

			public static void OnReservationCompleted(PartyBeaconID_t beacon, CSteamID user)
			{
			}

			public static bool OnReservationCompleted(UserData user)
			{
				return false;
			}

			public static void ChangeNumOpenSlots(PartyBeaconID_t beacon, uint openSlots, Action<ChangeNumOpenSlotsCallback_t, bool> callback)
			{
			}

			public static bool DestroyBeacon(PartyBeaconID_t beacon)
			{
				return false;
			}

			public static PartyBeaconID_t[] GetBeacons()
			{
				return null;
			}

			public static PartyBeaconDetails? GetBeaconDetails(PartyBeaconID_t beacon)
			{
				return null;
			}

			public static void JoinParty(PartyBeaconID_t beacon, Action<JoinPartyCallback_t, bool> callback)
			{
			}

			public static bool GetBeaconLocationData(SteamPartyBeaconLocation_t location, ESteamPartyBeaconLocationData data, out string result)
			{
				result = null;
				return false;
			}
		}
	}
}
