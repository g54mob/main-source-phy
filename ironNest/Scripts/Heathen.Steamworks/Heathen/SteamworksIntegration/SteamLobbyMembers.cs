using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamLobbyData), "Members", "attributes")]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamLobbyData))]
	public class SteamLobbyMembers : MonoBehaviour
	{
		[Serializable]
		public class Attributes
		{
			[Header("Configuration")]
			[Tooltip("If true the local user's display will be shown otherwise we skip the local user")]
			public bool showSelf;

			[Header("Elements")]
			[Tooltip("This game object will be instantiated for each member that joins and managed by the component")]
			public SteamLobbyMemberData template;

			[Tooltip("The container where member templates will be spawned as members join or removed from when members leave.")]
			public Transform content;
		}

		public Attributes attributes;

		private SteamLobbyData _mInspector;

		private List<SteamLobbyMemberData> _mSpawnedMembers;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		private void HandleLobbyChanged(LobbyData arg0)
		{
		}

		private void GlobalChatUpdate(LobbyData lobby, UserData user, EChatMemberStateChange state)
		{
		}

		private void AddMember(LobbyMemberData data)
		{
		}

		private void RemoveMember(UserLobbyLeaveData data)
		{
		}
	}
}
