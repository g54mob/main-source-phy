using UnityEngine;

namespace TFBGames
{
	public class SystemKeyboardProvider : ServicePrefab
	{
		private ISystemKeyboard keyboard;

		public ISystemKeyboard Keyboard => keyboard;

		public override void OnRegister()
		{
			base.OnRegister();
			keyboard = GetPlatformKeyboard();
			if (keyboard == null)
			{
				Debug.LogWarning("Keyboard for platform not available.");
			}
		}

		public override void UnRegister()
		{
			base.UnRegister();
		}

		public void OpenKeyboard(KeyboardType keyboardType, string defaultText, string title, string description, int maxLength)
		{
			keyboard?.Show(keyboardType, defaultText, title, description, maxLength);
		}

		private ISystemKeyboard GetPlatformKeyboard()
		{
			if (keyboard != null)
			{
				return keyboard;
			}
			return null;
		}
	}
}
