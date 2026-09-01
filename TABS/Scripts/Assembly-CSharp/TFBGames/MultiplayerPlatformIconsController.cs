using System;
using System.Collections.Generic;
using UnityEngine;

namespace TFBGames
{
	public class MultiplayerPlatformIconsController : ServicePrefab
	{
		[Serializable]
		public class PlatformIconInfo
		{
			public MultiplayerPlatform Platform;

			public Sprite Sprite;
		}

		[SerializeField]
		[Tooltip("Default icon to use when no icon has been setup for a platform, or the current platform doesn't allow showing other platforms' icons.")]
		protected Sprite m_defaultIcon;

		[SerializeField]
		[Tooltip("Platforms icon info.")]
		protected PlatformIconInfo[] m_platforms;

		private readonly List<MultiplayerPlatform[]> m_samePlatforms = new List<MultiplayerPlatform[]> { new MultiplayerPlatform[2]
		{
			MultiplayerPlatform.Ps4,
			MultiplayerPlatform.Ps5
		} };

		public Sprite GetIcon(MultiplayerPlatform platform)
		{
			if (platform == MultiplayerPlatform.Switch)
			{
				return m_defaultIcon;
			}
			if (!DoesCurrentPlatformAllowIconFromPlatform(platform) || m_platforms == null || m_platforms.Length == 0)
			{
				return m_defaultIcon;
			}
			int i = 0;
			for (int num = m_platforms.Length; i < num; i++)
			{
				PlatformIconInfo platformIconInfo = m_platforms[i];
				if (platformIconInfo != null && platformIconInfo.Platform == platform)
				{
					return platformIconInfo.Sprite;
				}
			}
			return m_defaultIcon;
		}

		private bool DoesCurrentPlatformAllowIconFromPlatform(MultiplayerPlatform iconPlatform)
		{
			MultiplayerPlatform multiplayerPlatform = NetworkSessionHelper.GetMultiplayerPlatform();
			if (multiplayerPlatform == iconPlatform || ArePlatformsConsideredTheSame(multiplayerPlatform, iconPlatform))
			{
				return true;
			}
			return false;
		}

		private bool ArePlatformsConsideredTheSame(MultiplayerPlatform currentPlatform, MultiplayerPlatform iconPlatform)
		{
			int i = 0;
			for (int count = m_samePlatforms.Count; i < count; i++)
			{
				bool flag = false;
				bool flag2 = false;
				MultiplayerPlatform[] array = m_samePlatforms[i];
				int j = 0;
				for (int num = array.Length; j < num; j++)
				{
					MultiplayerPlatform num2 = array[j];
					if (num2 == currentPlatform)
					{
						flag = true;
					}
					if (num2 == iconPlatform)
					{
						flag2 = true;
					}
					if (flag && flag2)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
