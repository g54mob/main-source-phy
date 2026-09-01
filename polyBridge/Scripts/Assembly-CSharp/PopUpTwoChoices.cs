using UnityEngine;

public class PopUpTwoChoices
{
	public static void Display(string message, string choiceA, string choiceB, Panel_PopUpTwoChoices.OnChoiceDelegate callbackA, Panel_PopUpTwoChoices.OnChoiceDelegate callbackB)
	{
		Display(message, choiceA, choiceB, callbackA, callbackB, PopUpWarningCategory.NONE);
		GameUI.m_Instance.m_PopUpTwoChoices.m_NeverShowAgainToggle.transform.parent.gameObject.SetActive(value: false);
	}

	public static void Display(string message, string choiceA, string choiceB, Panel_PopUpTwoChoices.OnChoiceDelegate callbackA, Panel_PopUpTwoChoices.OnChoiceDelegate callbackB, PopUpWarningCategory warningCategory)
	{
		if (IsActive())
		{
			Debug.LogWarningFormat("Tried to display TwoChoices popup when another is currently active");
			return;
		}
		GameUI.m_Instance.m_PopUpTwoChoices.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_PopUpTwoChoices.m_Category = warningCategory;
		GameUI.m_Instance.m_PopUpTwoChoices.m_Message.text = message;
		GameUI.m_Instance.m_PopUpTwoChoices.m_ChoiceAText.text = choiceA;
		GameUI.m_Instance.m_PopUpTwoChoices.m_ChoiceBText.text = choiceB;
		GameUI.m_Instance.m_PopUpTwoChoices.m_ChoiceCallbackA = callbackA;
		GameUI.m_Instance.m_PopUpTwoChoices.m_ChoiceCallbackB = callbackB;
		GameUI.m_Instance.m_PopUpTwoChoices.m_NeverShowAgainToggle.transform.parent.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_PopUpTwoChoices.m_NeverShowAgainToggle.isOn = Profiles.m_ActiveProfile.m_NeverShowAgain.Contains(warningCategory);
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.EAST, Localize.Get("TOOLTIP_CANCEL"));
	}

	public static bool IsActive()
	{
		return GameUI.m_Instance.m_PopUpTwoChoices.gameObject.activeInHierarchy;
	}
}
