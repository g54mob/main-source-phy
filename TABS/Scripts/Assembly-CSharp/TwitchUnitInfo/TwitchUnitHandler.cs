using Landfall.TABC;
using Landfall.TABS;
using UnityEngine;

namespace TwitchUnitInfo
{
	[CreateAssetMenu(fileName = "TwitchUnitHandler", menuName = "Twitch/Twitch Unit Handler", order = 1)]
	public class TwitchUnitHandler : ScriptableObject
	{
		public GameObject TwitchUnitName;

		[HideInInspector]
		public TwitchUnitNameHandler NameHandler = new TwitchUnitNameHandler();

		private int minBitAmountToAddName = 1;

		public TwitchUnitNameMode UnitNameMode = TwitchUnitNameMode.OnlyViaEvents;

		private GlobalSettingsHandler settingsHandler;

		private TwitchNameBox NameBox;

		public Sprite SubIcon;

		public Sprite ModIcon;

		public Sprite VipIcon;

		public Sprite BitIcon;

		public Sprite BroadcastIcon;

		public bool IncludeLurkers = true;

		public const int NoLurkersNeededCount = 20;

		private string LastConnectedChannel;

		public void NewUnit(Unit spawnedUnit, TABCUnitUI spawnedUnitUI)
		{
			TwitchHandler service = ServiceLocator.GetService<TwitchHandler>();
			if (!service || !service.isConnected)
			{
				return;
			}
			if (!string.IsNullOrEmpty(LastConnectedChannel) && LastConnectedChannel != service.IRC.channelName)
			{
				NameHandler.Clear();
			}
			LastConnectedChannel = service.IRC.channelName;
			if (!NameBox)
			{
				NameBox = Object.FindObjectOfType<TwitchNameBox>();
			}
			if (!NameBox || NameBox.CurrentTwitchMode == TwitchMode.Off)
			{
				spawnedUnitUI.TwitchUserIcon.enabled = false;
				return;
			}
			if (GetWantsLurkers() != NameHandler.IsCurrentListGeneratedWithLurkers)
			{
				NameHandler.Clear();
			}
			if (!NameHandler.HasNames())
			{
				ViewerTypes viewerType = ViewerTypes.viewer;
				if ((bool)NameBox)
				{
					if (NameBox.CurrentUserFilter == TwitchNameBox.UserFilter.Subs)
					{
						viewerType = ViewerTypes.subscriber;
					}
					else if (NameBox.CurrentUserFilter == TwitchNameBox.UserFilter.VIPs)
					{
						viewerType = ViewerTypes.vip;
					}
					else if (NameBox.CurrentUserFilter == TwitchNameBox.UserFilter.Mods)
					{
						viewerType = ViewerTypes.mod;
					}
				}
				NameHandler.GenerateUnitNameArray(viewerType, GetWantsLurkers());
			}
			TwitchUserData twitchUserData;
			if (NameBox.CurrentTwitchMode == TwitchMode.Select)
			{
				twitchUserData = default(TwitchUserData);
				ViewerTypes type = ViewerTypes.viewer;
				twitchUserData.name = NameBox.GetNextSelectedName(ref type, out twitchUserData.color);
				twitchUserData.type = type;
			}
			else
			{
				twitchUserData = NameHandler.GetNextViewer();
			}
			if (!string.IsNullOrEmpty(twitchUserData.name) && (bool)spawnedUnitUI)
			{
				spawnedUnitUI.Nameplate.text = twitchUserData.name;
				spawnedUnitUI.Nameplate.color = twitchUserData.color;
				if (twitchUserData.type == ViewerTypes.subscriber)
				{
					spawnedUnitUI.TwitchUserIcon.sprite = SubIcon;
					spawnedUnitUI.TwitchUserIcon.gameObject.SetActive(value: true);
				}
				else if (twitchUserData.type == ViewerTypes.vip)
				{
					spawnedUnitUI.TwitchUserIcon.sprite = VipIcon;
					spawnedUnitUI.TwitchUserIcon.gameObject.SetActive(value: true);
				}
				else if (twitchUserData.type == ViewerTypes.mod)
				{
					spawnedUnitUI.TwitchUserIcon.sprite = ModIcon;
					spawnedUnitUI.TwitchUserIcon.gameObject.SetActive(value: true);
				}
				else if (twitchUserData.type == ViewerTypes.bit)
				{
					spawnedUnitUI.TwitchUserIcon.sprite = BitIcon;
					spawnedUnitUI.TwitchUserIcon.gameObject.SetActive(value: true);
				}
				else if (twitchUserData.type == ViewerTypes.broadcaster)
				{
					spawnedUnitUI.TwitchUserIcon.sprite = BroadcastIcon;
					spawnedUnitUI.TwitchUserIcon.gameObject.SetActive(value: true);
				}
			}
		}

		public bool GetWantsLurkers()
		{
			TwitchHandler service = ServiceLocator.GetService<TwitchHandler>();
			if (!service)
			{
				return false;
			}
			int count = service.ActiveChatters.hashes.Count;
			if (IncludeLurkers)
			{
				return count < 20;
			}
			return false;
		}

		public void SetLurkers(bool lurkers)
		{
			IncludeLurkers = lurkers;
		}
	}
}
