using System;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct PartyBeaconDetails
	{
		public PartyBeaconID_t id;

		public UserData owner;

		public SteamPartyBeaconLocation_t Location;

		public string metadata;
	}
}
