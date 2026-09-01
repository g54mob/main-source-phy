using System;
using System.Collections.Generic;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Heathen.SteamworksIntegration
{
	[ModularComponent(typeof(SteamUserData), "Status", "settings")]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamUserData))]
	public class SteamUserStatus : MonoBehaviour
	{
		[Serializable]
		public class Options
		{
			[Serializable]
			public class References
			{
				public Sprite icon;

				public bool setIconColor;

				public Color iconColor;

				[Tooltip("You can use %gameName% and it will be replaced with the name of the game the player is currently playing. This is only relevant for In This Game and In Another Game options.")]
				public SteamText message;

				public bool setMessageColor;

				public Color messageColor;

				public void Set(Image image, TextMeshProUGUI label, FriendGameInfo_t? gameInfo)
				{
				}
			}

			[FormerlySerializedAs("InThisGame")]
			public References inThisGame;

			[FormerlySerializedAs("InAnotherGame")]
			public References inAnotherGame;

			[FormerlySerializedAs("Online")]
			public References online;

			[FormerlySerializedAs("Offline")]
			public References offline;

			[FormerlySerializedAs("Busy")]
			public References busy;

			[FormerlySerializedAs("Away")]
			public References away;

			[FormerlySerializedAs("Snooze")]
			public References snooze;

			[FormerlySerializedAs("LookingToTrade")]
			public References lookingToTrade;

			[FormerlySerializedAs("LookingToPlay")]
			public References lookingToPlay;
		}

		[Serializable]
		public class Settings
		{
			public Options configuration;

			[Header("Elements")]
			public List<Image> images;

			public List<TextMeshProUGUI> labels;
		}

		public Settings settings;

		private SteamUserData _mData;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		private void InternalRichPresenceUpdate(UserData friend, AppData app)
		{
		}

		private void InternalPersonaStateChange(UserData friend, EPersonaChange flag)
		{
		}

		public void Refresh()
		{
		}
	}
}
