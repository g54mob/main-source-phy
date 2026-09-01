using System;
using Steamworks;

public class GamepadVirtualKeyboard
{
	public static bool m_Active;

	private static Action<string> m_Callback;

	public static bool IsSupported()
	{
		if (SteamManager.IsLoggedOn())
		{
			return Steamworks.SteamUtils.IsSteamInBigPictureMode;
		}
		return false;
	}

	public static void Init()
	{
		Steamworks.SteamUtils.OnGamepadTextInputDismissed += GamepadTextDismissed;
	}

	public static bool MaybeOpenVirtualKeyboard(string text, int charLimit, string title, bool multiline, Action<string> callback)
	{
		if (!SteamManager.IsLoggedOn() || !Steamworks.SteamUtils.IsSteamInBigPictureMode)
		{
			return false;
		}
		m_Active = Open(title, text, charLimit, multiline, callback);
		if (m_Active)
		{
			GameUI.DuckScreen();
		}
		return m_Active;
	}

	private static bool Open(string title, string existingText, int charLimit, bool multiline, Action<string> callback)
	{
		m_Callback = callback;
		return Steamworks.SteamUtils.ShowGamepadTextInput(GamepadTextInputMode.Normal, GamepadTextInputLineMode.SingleLine, title, charLimit, existingText);
	}

	private static void GamepadTextDismissed(bool success)
	{
		m_Active = false;
		GameUI.UnDuckScreen();
		GamepadManager.m_VirtualMouseUI.ResetMouseToCenter();
		if (success)
		{
			m_Callback?.Invoke(Steamworks.SteamUtils.GetEnteredGamepadText());
		}
		else
		{
			m_Callback?.Invoke(null);
		}
	}
}
