using System;
using UnityEngine;

namespace TFBGames
{
	public class SettingsProfileManager : IService
	{
		public SettingsProfile CurrentSettingsProfile { get; private set; }

		public event Action<SettingsProfile> SettingsProfileChanged;

		public void CreateAppropriateSettingsProfile(ConsoleDisplayMode displayMode = ConsoleDisplayMode.Undefined)
		{
			ApplySettingsProfile(Resources.Load<SettingsProfile>("SettingsProfiles/Desktop"));
		}

		private void ApplySettingsProfile(SettingsProfile newSettingsProfile)
		{
			CurrentSettingsProfile = newSettingsProfile;
			if (CurrentSettingsProfile.ApplyResolution)
			{
				Screen.SetResolution(CurrentSettingsProfile.Resolution.width, CurrentSettingsProfile.Resolution.height, fullscreen: true);
			}
			this.SettingsProfileChanged?.Invoke(CurrentSettingsProfile);
		}

		void IService.OnRegister()
		{
		}

		void IService.OnAwake()
		{
			CreateAppropriateSettingsProfile();
		}

		void IService.OnStart()
		{
		}

		void IService.OnUpdate()
		{
		}

		void IService.OnFixedUpdate()
		{
		}

		void IService.OnLateUpdate()
		{
		}

		void IService.UnRegister()
		{
		}
	}
}
