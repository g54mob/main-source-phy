using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class CreateArguments
	{
		[Tooltip("How will this lobby be used? This is an optional feature. If set to Group or Session then features of the LobbyData object can be used in code to fetch the created lobby such as LobbyData.GetGroup(...)")]
		public SteamLobbyModeType usageHint;

		[Tooltip("The name to assign to the lobby when it is created")]
		public string name;

		[Tooltip("The number of slots the newly created lobby should have")]
		public int slots;

		[Tooltip("The type of lobby to create")]
		public ELobbyType type;

		[Tooltip("The metadata to add to the lobby after creation. This is a dictionary and fields will not be repeated")]
		public List<MetadataTemplate> metadata;

		[Tooltip("The Rich Presence fields to be set when a lobby is created.")]
		public List<StringKeyValuePair> richPresenceFields;
	}
}
