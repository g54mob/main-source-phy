using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Heathen.SteamworksIntegration
{
	[HelpURL("https://kb.heathen.group/steam/features/lobby/unity-lobby/steam-lobby-search")]
	[AddComponentMenu("Steamworks/Lobby Search")]
	public class SteamLobbySearch : MonoBehaviour
	{
		[Header("Configuration")]
		[Tooltip("If true the search will check if there is currently a party lobby if so it will search for a lobby enough slots for each party member, else it will obey the slots field.")]
		public bool partyWise;

		[Tooltip("If less than or equal to 0 then we wont use the open slot filter")]
		public int slots;

		[Tooltip("The distance from the searching user that should be considered when searching")]
		public ELobbyDistanceFilter distance;

		[Tooltip("Metadata values that should be used to sort the results e.g. values `closer` to these values will be weighted higher in the results")]
		public List<NearFilter> nearValues;

		[Tooltip("Metadata values that should be compared as numeric values e.g. should follow typical maths rules for concepts such as less than, greater than, etc.")]
		public List<NumericFilter> numericFilters;

		[Tooltip("Metadata values that should be compared as strings")]
		public List<StringFilter> stringFilters;

		[Range(1f, 50f)]
		public int maxResults;

		[Header("Elements")]
		public SteamLobbyData template;

		public Transform content;

		[FormerlySerializedAs("OnLobbiesFound")]
		[Header("Events")]
		[Tooltip("Invoked when the lobby search completes. Returns an array of LobbyData.")]
		public LobbyDataListEvent onLobbiesFound;

		private readonly List<SteamLobbyData> _lobbies;

		public void Search()
		{
		}
	}
}
