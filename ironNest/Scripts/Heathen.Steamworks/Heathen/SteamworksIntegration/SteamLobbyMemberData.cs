using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Heathen.SteamworksIntegration
{
	[AddComponentMenu("Steamworks/Lobby Member")]
	[HelpURL("https://kb.heathen.group/steam/features/lobby/unity-lobby/steam-lobby-member-data")]
	[RequireComponent(typeof(SteamUserData))]
	public class SteamLobbyMemberData : MonoBehaviour
	{
		private LobbyMemberData _mData;

		private SteamUserData _mUserData;

		private SteamLobbyMemberDataEvents _mEvents;

		[FormerlySerializedAs("m_Delegates")]
		[SerializeField]
		private List<string> Delegates;

		public LobbyMemberData Data
		{
			get
			{
				return default(LobbyMemberData);
			}
			set
			{
			}
		}

		public LobbyData Lobby => default(LobbyData);

		private void Awake()
		{
		}
	}
}
