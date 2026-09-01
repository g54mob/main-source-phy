using TMPro;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamLobbyData), "Invite", null)]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamLobbyData))]
	public class SteamLobbyInvite : MonoBehaviour
	{
		private SteamLobbyData _mInspector;

		private void Awake()
		{
		}

		public void OpenOverlay()
		{
		}

		public void InviteUser(UserData user)
		{
		}

		public void InviteFromString(string id)
		{
		}

		public void InviteFromInput(TMP_InputField input)
		{
		}

		public void InviteFromUser(SteamUserData user)
		{
		}
	}
}
