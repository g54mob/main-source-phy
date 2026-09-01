using System;
using Steamworks;
using TMPro;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[AddComponentMenu(null)]
	[ModularComponent(typeof(SteamLobbyMemberData), "Chat Message", null)]
	[RequireComponent(typeof(SteamLobbyMemberData))]
	[HelpURL("https://heathen.group/kb/lobby/#chat")]
	public class SteamLobbyMemberChatMessage : MonoBehaviour
	{
		[ElementField("Chat Message", 0)]
		public GameObject expansionPanel;

		[SettingsField(0, false, "Chat Display")]
		[SerializeField]
		private string dateTimeFormat;

		[ElementField("Chat Message", 0)]
		[SerializeField]
		private TextMeshProUGUI datetime;

		[ElementField("Chat Message", 0)]
		[SerializeField]
		private TextMeshProUGUI message;

		private SteamLobbyMemberData _mData;

		public UserData User => default(UserData);

		public byte[] Data { get; private set; }

		public string Message { get; private set; }

		public DateTime ReceivedAt { get; private set; }

		public EChatEntryType Type { get; private set; }

		public bool IsExpanded
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		private void Awake()
		{
		}

		public void Initialise(LobbyChatMsg chatMessage)
		{
		}
	}
}
