using System;
using System.Collections.Generic;
using InControl;
using Landfall.TABS_Input;
using UnityEngine;

namespace TFBGames
{
	public class InputSettingsVersionUpdater : MonoBehaviour, IVersionUpdater
	{
		[Serializable]
		public class ResetPlayerAction
		{
			public string ActionName;

			[Tooltip("Only reset on these specified platforms.")]
			public SettingsInstance.Platform Platform;
		}

		[Serializable]
		public class ResetAllPlayerActions
		{
			[Tooltip("Only reset if the game version saved in prefs is this or less.")]
			public int Version;

			[Tooltip("Only reset if on these specified platforms.")]
			public SettingsInstance.Platform Platform;
		}

		[SerializeField]
		[Tooltip("List of action names we want to reset to default bindings.")]
		protected List<ResetPlayerAction> m_resetPlayerActions;

		[SerializeField]
		[Tooltip("Reset all actions to default bindings.")]
		protected List<ResetAllPlayerActions> m_resetAllPlayerActions;

		public void DoUpdate(int oldVersion, int newVersion)
		{
			ResetPlayerActions();
			ResetAllActions(oldVersion);
		}

		private void ResetPlayerActions()
		{
			if (m_resetPlayerActions == null || m_resetPlayerActions.Count <= 0)
			{
				return;
			}
			SettingsInstance.Platform currentPlatform = GlobalSettingsHandler.CurrentPlatform;
			foreach (ResetPlayerAction resetPlayerAction in m_resetPlayerActions)
			{
				PlayerAction playerActionByName = PlayerActions.Instance.GetPlayerActionByName(resetPlayerAction.ActionName);
				if (playerActionByName != null && (resetPlayerAction.Platform & currentPlatform) != SettingsInstance.Platform.None)
				{
					playerActionByName.ResetBindings();
				}
			}
		}

		private void ResetAllActions(int oldVersion)
		{
			if (m_resetAllPlayerActions == null || m_resetAllPlayerActions.Count <= 0)
			{
				return;
			}
			SettingsInstance.Platform currentPlatform = GlobalSettingsHandler.CurrentPlatform;
			foreach (ResetAllPlayerActions resetAllPlayerAction in m_resetAllPlayerActions)
			{
				if ((resetAllPlayerAction.Platform & currentPlatform) != SettingsInstance.Platform.None && oldVersion <= resetAllPlayerAction.Version)
				{
					PlayerActions.Instance.Reset();
					PlayerActions.Instance.SaveBindings(savePlayerPrefsImmediately: false);
					break;
				}
			}
		}
	}
}
