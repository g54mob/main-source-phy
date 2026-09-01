using Steamworks;

namespace Heathen.SteamworksIntegration.API
{
	public static class BigPicture
	{
		public static class Client
		{
			public static bool IsInBigPicture => false;

			public static bool IsRunningOnDeck => false;

			public static bool ShowTextInput(EGamepadTextInputMode inputMode, EGamepadTextInputLineMode lineMode, string description, uint maxLength, string currentText)
			{
				return false;
			}

			public static bool ShowTextInput(EGamepadTextInputMode inputMode, EGamepadTextInputLineMode lineMode, string description, int maxLength, string currentText)
			{
				return false;
			}
		}
	}
}
