using TMPro;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamLobbyData), "Join", null)]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamLobbyData))]
	public class SteamLobbyJoin : MonoBehaviour
	{
		[SettingsField(0, true, null)]
		[Tooltip("If true and creating a Party it will leave any existing lobby first, if true when creating a session it will notify any existing party of the new session lobby.")]
		public bool partyWise;

		private SteamLobbyData _mInspector;

		private SteamLobbyDataEvents _mEvents;

		private void Awake()
		{
		}

		public void RequestJoin(SteamLobbyJoin toRequest)
		{
		}

		public void JoinFromIdString(string id)
		{
		}

		public void JoinFromIdInputField(TMP_InputField input)
		{
		}

		public void Join(LobbyData lobby)
		{
		}

		public void JoinOnRequestEvent(LobbyData lobby, UserData user)
		{
		}
	}
}
