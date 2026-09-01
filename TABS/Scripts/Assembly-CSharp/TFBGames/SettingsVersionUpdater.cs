using System;
using System.Collections.Generic;
using UnityEngine;

namespace TFBGames
{
	public class SettingsVersionUpdater : MonoBehaviour, IVersionUpdater
	{
		public enum SettingsType
		{
			Options = 0,
			Slider = 1
		}

		[Serializable]
		public class ResetSettingsKeys
		{
			public string SettingsKey;

			[Tooltip("Only reset on these specified platforms.")]
			public SettingsInstance.Platform Platform;

			public SettingsType SettingsType;

			public string ResetValue;
		}

		[SerializeField]
		[Tooltip("List of Settings Keys to be reset with current update")]
		protected List<ResetSettingsKeys> resetSettingsKeys;

		public void DoUpdate(int oldVersion, int newVersion)
		{
			UpdateSettingsKeys();
		}

		private void UpdateSettingsKeys()
		{
			IPlayerPrefsPlatform service = ServiceLocator.GetService<IPlayerPrefsPlatform>();
			if (resetSettingsKeys == null || resetSettingsKeys.Count <= 0)
			{
				return;
			}
			SettingsInstance.Platform currentPlatform = GlobalSettingsHandler.CurrentPlatform;
			foreach (ResetSettingsKeys resetSettingsKey in resetSettingsKeys)
			{
				if ((resetSettingsKey.Platform & currentPlatform) == 0 || !service.HasKey(resetSettingsKey.SettingsKey))
				{
					continue;
				}
				switch (resetSettingsKey.SettingsType)
				{
				case SettingsType.Options:
				{
					if (int.TryParse(resetSettingsKey.ResetValue, out var result2))
					{
						service.SetInt(resetSettingsKey.SettingsKey, result2);
					}
					break;
				}
				case SettingsType.Slider:
				{
					if (float.TryParse(resetSettingsKey.ResetValue, out var result))
					{
						service.SetFloat(resetSettingsKey.SettingsKey, result);
					}
					break;
				}
				}
			}
		}
	}
}
