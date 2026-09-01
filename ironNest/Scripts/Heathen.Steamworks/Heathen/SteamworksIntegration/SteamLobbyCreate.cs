using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamLobbyData), "Create", null)]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamLobbyData))]
	public class SteamLobbyCreate : MonoBehaviour
	{
		public enum SteamLobbyType
		{
			Private = 0,
			FriendsOnly = 1,
			Public = 2,
			Invisible = 3
		}

		[SettingsField(0, true, null)]
		[Tooltip("If true and creating a Party it will leave any existing lobby first, if true when creating a session it will notify any existing party of the new session lobby.")]
		public bool partyWise;

		[SettingsField(0, false, "Create")]
		[Tooltip("How will this lobby be used? This is an optional feature. If set to Party or Session then features of the LobbyData object can be used in code to fetch the created lobby such as LobbyData.GetGroup(...)")]
		public SteamLobbyModeType usageHint;

		[SettingsField(0, false, "Create")]
		[Tooltip("The number of slots the newly created lobby should have")]
		public int slots;

		[SettingsField(0, false, "Create")]
		[Tooltip("The type of lobby to create")]
		public SteamLobbyType type;

		private SteamLobbyData _mInspector;

		private SteamLobbyDataEvents _mEvents;

		private void Awake()
		{
		}

		public void Create()
		{
		}
	}
}
