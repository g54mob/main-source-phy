using UnityEngine;
using UnityEngine.Serialization;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamUserData), "Invite", null)]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamUserData))]
	public class SteamUserLobbyInvite : MonoBehaviour
	{
		[FormerlySerializedAs("CreateIfMissing")]
		[SettingsField(0, false, "Invite")]
		public bool createIfMissing;

		private SteamUserData _mSteamUserData;

		private void Awake()
		{
		}

		public void Invite(SteamLobbyData lobby)
		{
		}

		public void Invite(LobbyData lobby)
		{
		}
	}
}
