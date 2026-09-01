using System.Collections.Generic;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamLobbyData), "Quick Match", null)]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamLobbyData))]
	public class SteamLobbyQuickMatch : MonoBehaviour
	{
		public enum SteamLobbyType
		{
			Private = 0,
			FriendsOnly = 1,
			Public = 2,
			Invisible = 3
		}

		public enum LobbyDistanceFilter
		{
			Close = 0,
			Default = 1,
			Far = 2,
			Worldwide = 3
		}

		[SettingsField(0, true, null)]
		[Tooltip("If true the search will check if there is currently a party lobby if so it will search for a lobby enough slots for each party member, else it will obey the slots field.")]
		public bool partyWise;

		[SettingsField(0, false, "Quick Match")]
		[Tooltip("The type of lobby to create")]
		public SteamLobbyType type;

		[SettingsField(0, false, "Quick Match")]
		[Tooltip("The number of slots to create the lobby with if created.")]
		public int slotsOnCreate;

		[Header("Search Arguments")]
		[SettingsField(0, false, "Quick Match")]
		[Tooltip("The distance from the searching user that should be considered when searching")]
		public LobbyDistanceFilter distance;

		[SettingsField(0, false, "Quick Match")]
		[Tooltip("Metadata values that should be used to sort the results e.g. values `closer` to these values will be weighted higher in the results")]
		public List<NearFilter> nearValues;

		[SettingsField(0, false, "Quick Match")]
		[Tooltip("Metadata values that should be compared as numeric values e.g., should follow typical maths rules for concepts such as less than, greater than, etc.")]
		public List<NumericFilter> numericFilters;

		[SettingsField(0, false, "Quick Match")]
		[Tooltip("Metadata values that should be compared as strings")]
		public List<StringFilter> stringFilters;

		private int _slots;

		private SteamLobbyData _inspector;

		private SteamLobbyDataEvents _events;

		private void Awake()
		{
		}

		public void Match()
		{
		}
	}
}
