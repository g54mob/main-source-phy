using System;
using UnityEngine;

public class PopupInputField
{
	private const int DEFAULT_CHAR_LIMIT = 32;

	public static void DisplayLarge(string title, string defaultText, int characterLimit, bool isFilename, bool isDirectory, Action<string> okDelegate)
	{
		DisplayInternal(title, defaultText, characterLimit, isLarge: true, isFilename, isDirectory, okDelegate);
	}

	public static void Display(string title, string defaultText, bool isFilename, bool isDirectory, Action<string> okDelegate)
	{
		DisplayInternal(title, defaultText, 32, isLarge: false, isFilename, isDirectory, okDelegate);
	}

	public static void Display(string title, string defaultText, int characterLimit, bool isFilename, bool isDirectory, Action<string> okDelegate)
	{
		DisplayInternal(title, defaultText, characterLimit, isLarge: false, isFilename, isDirectory, okDelegate);
	}

	private static void DisplayInternal(string title, string defaultText, int characterLimit, bool isLarge, bool isFilename, bool isDirectory, Action<string> okDelegate)
	{
		GameUI.m_Instance.m_PopUpInputField.m_OnOkDelegate = okDelegate;
		if ((GameInput.GetActiveGameDevice() == GameDevice.Gamepad || Game.IsRunningOnSteamDeck()) && (GamepadVirtualKeyboard.MaybeOpenVirtualKeyboard(defaultText, characterLimit, title, isLarge, delegate(string x)
		{
			OnVirtualKeyboardDone(x, isFilename, isDirectory);
		}) || Game.IsRunningOnSteamDeck()))
		{
			return;
		}
		if (GameUI.m_Instance.m_PopUpInputField.gameObject.activeInHierarchy)
		{
			Debug.LogWarningFormat("Tried to display popup input field when popup is currently active");
			return;
		}
		GameUI.m_Instance.m_PopUpInputField.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_PopUpInputField.FilterForFilenames(isFilename);
		GameUI.m_Instance.m_PopUpInputField.FilterForDirectories(isDirectory);
		GameUI.m_Instance.m_PopUpInputField.m_Title.text = title;
		GameUI.m_Instance.m_PopUpInputField.m_InputField.text = defaultText;
		GameUI.m_Instance.m_PopUpInputField.m_InputField.characterLimit = characterLimit;
		if (isLarge)
		{
			GameUI.m_Instance.m_PopUpInputField.SetLargeSize();
		}
		else
		{
			GameUI.m_Instance.m_PopUpInputField.SetRegularSize();
		}
		GameUI.m_Instance.m_PopUpInputField.m_InputField.Select();
		GameUI.m_Instance.m_PopUpInputField.m_InputField.ActivateInputField();
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.NORTH, Localize.Get("UI_OK"), GamepadButtonType.EAST, Localize.Get("TOOLTIP_CANCEL"));
	}

	public static bool IsActive()
	{
		return GameUI.m_Instance.m_PopUpInputField.gameObject.activeInHierarchy;
	}

	private static void OnVirtualKeyboardDone(string text, bool isFilename, bool isDirectory)
	{
		string text2 = text;
		if (isFilename)
		{
			text2 = Utils.RemoveInvalidCharsFromFilename(text2);
		}
		if (isDirectory)
		{
			text2 = Utils.RemoveInvalidCharsFromPath(text2);
		}
		GameUI.m_Instance.m_PopUpInputField.m_OnOkDelegate?.Invoke(text2);
	}
}
