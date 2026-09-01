using System;
using UnityEngine;

public class PopUpMessage
{
	public static void DisplayWarningOkOnly(string message)
	{
		Display_Internal(Localize.Get("UI_POPUP_WARNING_HEADER"), message, showOKButton: true, showCancelButton: false, useYesNoLables: false, null, null);
		GameUI.m_Instance.m_PopUpMessage.m_OkButton.gameObject.SetActive(value: true);
	}

	public static void DisplayInfoOkOnly(string message)
	{
		Display_Internal(Localize.Get("UI_POPUP_INFO"), message, showOKButton: true, showCancelButton: false, useYesNoLables: false, null, null);
		GameUI.m_Instance.m_PopUpMessage.m_OkButton.gameObject.SetActive(value: true);
	}

	public static void DisplayConfirmation(string message, bool useYesNoLabels, Action okCallback)
	{
		Display_Internal(Localize.Get("UI_POPUP_CONFIRM"), message, showOKButton: true, showCancelButton: true, useYesNoLabels, okCallback, null);
	}

	public static void DisplayConfirmation(string message, FileSlot slot, Action<FileSlot> okCallback)
	{
		Display_Internal(Localize.Get("UI_POPUP_CONFIRM"), message, showOKButton: true, showCancelButton: true, useYesNoLables: false, null, null);
		GameUI.m_Instance.m_PopUpMessage.m_FileSlot = slot;
		GameUI.m_Instance.m_PopUpMessage.m_OnOkFIleSlotCallback = okCallback;
	}

	public static void DisplayWarning(string message, bool useYesNoLables, Action okCallback)
	{
		Display_Internal(Localize.Get("UI_POPUP_WARNING_HEADER"), message, showOKButton: true, showCancelButton: true, useYesNoLables, okCallback, null);
	}

	public static void DisplayWarning(string message, bool useYesNoLabels, FileSlot slot, Action<FileSlot> okCallback)
	{
		Display_Internal(Localize.Get("UI_POPUP_WARNING_HEADER"), message, showOKButton: true, showCancelButton: true, useYesNoLabels, null, null);
		GameUI.m_Instance.m_PopUpMessage.m_FileSlot = slot;
		GameUI.m_Instance.m_PopUpMessage.m_OnOkFIleSlotCallback = okCallback;
	}

	public static void DisplayWarning(string message, bool useYesNoLabels, Action okCallback, Action cancelCallback)
	{
		Display_Internal(Localize.Get("UI_POPUP_WARNING_HEADER"), message, showOKButton: true, showCancelButton: true, useYesNoLabels, okCallback, cancelCallback);
	}

	public static void DisplayErrorOkOnly(string message)
	{
		Display_Internal(Localize.Get("UI_POPUP_ERROR_HEADER"), message, showOKButton: true, showCancelButton: false, useYesNoLables: false, null, null);
		GameUI.m_Instance.m_PopUpMessage.m_OkButton.gameObject.SetActive(value: true);
	}

	public static void DisplayOkOnly(string message)
	{
		Display_Internal(string.Empty, message, showOKButton: true, showCancelButton: false, useYesNoLables: false, null, null);
		GameUI.m_Instance.m_PopUpMessage.m_OkButton.gameObject.SetActive(value: true);
	}

	public static void DisplayOkOnly(string message, Action okCallback)
	{
		Display_Internal(string.Empty, message, showOKButton: true, showCancelButton: false, useYesNoLables: false, okCallback, null);
		GameUI.m_Instance.m_PopUpMessage.m_NeverShowAgainToggle.transform.parent.gameObject.SetActive(value: false);
	}

	public static void DisplayOkOnly(string message, Action okCallback, PopUpWarningCategory warningCategory)
	{
		Display_Internal(Localize.Get("UI_POPUP_WARNING_HEADER"), message, showOKButton: true, showCancelButton: false, useYesNoLables: false, okCallback, null);
		GameUI.m_Instance.m_PopUpMessage.m_Category = warningCategory;
		GameUI.m_Instance.m_PopUpMessage.m_NeverShowAgainToggle.transform.parent.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_PopUpMessage.m_NeverShowAgainToggle.isOn = Profiles.m_ActiveProfile.m_NeverShowAgain.Contains(warningCategory);
	}

	public static void DisplayWithTitle(string title, string message, Action okCallback)
	{
		Display_Internal(title, message, showOKButton: true, showCancelButton: true, useYesNoLables: false, okCallback, null);
	}

	public static void Display(string message, Action okCallback)
	{
		Display_Internal(string.Empty, message, showOKButton: true, showCancelButton: true, useYesNoLables: false, okCallback, null);
	}

	public static void Display(string message, Action okCallback, Action cancelCallback)
	{
		Display_Internal(string.Empty, message, showOKButton: true, showCancelButton: true, useYesNoLables: false, okCallback, cancelCallback);
	}

	public static void DisplayLoading(string message)
	{
		Display_Internal(string.Empty, message, showOKButton: false, showCancelButton: false, useYesNoLables: false, null, null);
		GameUI.m_Instance.m_PopUpMessage.m_WaitAnimation.SetActive(value: true);
	}

	public static bool IsActive()
	{
		return GameUI.m_Instance.m_PopUpMessage.gameObject.activeInHierarchy;
	}

	public static void Close()
	{
		if (IsActive())
		{
			GameUI.m_Instance.m_PopUpMessage.Close();
			GameUI.m_Instance.m_GamepadLegend.HideButtons();
		}
	}

	private static void Display_Internal(string title, string message, bool showOKButton, bool showCancelButton, bool useYesNoLables, Action okCallback, Action cancelCallback)
	{
		if (IsActive())
		{
			Debug.LogWarningFormat("Tried to display popup message when popup is currently active");
			return;
		}
		GameUI.m_Instance.m_PopUpMessage.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_PopUpMessage.m_WaitAnimation.SetActive(value: false);
		GameUI.m_Instance.m_PopUpMessage.m_OkButton.gameObject.SetActive(showOKButton);
		GameUI.m_Instance.m_PopUpMessage.m_CancelButton.gameObject.SetActive(showCancelButton);
		GameUI.m_Instance.m_PopUpMessage.m_OKButtonText.text = (useYesNoLables ? Localize.Get("UI_YES") : Localize.Get("UI_OK"));
		GameUI.m_Instance.m_PopUpMessage.m_CancelButtonText.text = (useYesNoLables ? Localize.Get("UI_NO") : Localize.Get("TOOLTIP_CANCEL"));
		GameUI.m_Instance.m_PopUpMessage.m_BackButton.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_PopUpMessage.m_Title.text = (string.IsNullOrEmpty(title) ? string.Empty : title);
		GameUI.m_Instance.m_PopUpMessage.m_Message.text = message;
		GameUI.m_Instance.m_PopUpMessage.m_OnOkFIleSlotCallback = null;
		GameUI.m_Instance.m_PopUpMessage.m_OnOkCallback = okCallback;
		GameUI.m_Instance.m_PopUpMessage.m_OnCancelCallback = cancelCallback;
		GameUI.m_Instance.m_PopUpMessage.m_NeverShowAgainToggle.transform.parent.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		if (showOKButton && showCancelButton)
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.NORTH, useYesNoLables ? Localize.Get("UI_YES") : Localize.Get("UI_OK"), GamepadButtonType.EAST, useYesNoLables ? Localize.Get("UI_NO") : Localize.Get("TOOLTIP_CANCEL"));
		}
		else if (showOKButton && !showCancelButton)
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, useYesNoLables ? Localize.Get("UI_YES") : Localize.Get("UI_OK"));
		}
		else if (!showOKButton && showCancelButton)
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.EAST, useYesNoLables ? Localize.Get("UI_NO") : Localize.Get("TOOLTIP_CANCEL"));
		}
	}
}
