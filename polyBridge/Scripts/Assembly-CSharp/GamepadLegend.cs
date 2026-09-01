using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamepadLegend : MonoBehaviour
{
	public GamepadLegendAction[] m_GamepadLegendActions;

	public GamepadLegendAction[] m_GamepadLegendActionsLeft;

	public Image m_Background;

	private List<Tuple<Sprite, string>> m_GamepadLegendActionsSaved = new List<Tuple<Sprite, string>>();

	public static readonly int HEIGHT = 35;

	public void Awake()
	{
	}

	public void OnEnable()
	{
	}

	public void OnDisable()
	{
		m_Background.gameObject.SetActive(value: false);
	}

	public void LateUpdate()
	{
		if (GamepadVirtualKeyboard.m_Active || GameUI.m_Instance.m_PreloadingObject.activeInHierarchy)
		{
			HideButtons();
			HideButtonsLeft();
		}
	}

	public void ShowBackground()
	{
		m_Background.gameObject?.SetActive(value: true);
	}

	public void HideBackground()
	{
		m_Background.gameObject?.SetActive(value: false);
	}

	public void ShowButtons(GamepadButtonType buttonType, string text)
	{
		m_GamepadLegendActions[0].Show(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType), text.Trim());
		m_GamepadLegendActions[1].Hide();
		m_GamepadLegendActions[2].Hide();
		m_GamepadLegendActions[3].Hide();
	}

	public void ShowButtons(GamepadButtonType buttonType1, string text1, GamepadButtonType buttonType2, string text2)
	{
		m_GamepadLegendActions[0].Show(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType1), text1.Trim());
		m_GamepadLegendActions[1].Show(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType2), text2.Trim());
		m_GamepadLegendActions[2].Hide();
		m_GamepadLegendActions[3].Hide();
	}

	public void ShowButtons(GamepadButtonType buttonType1, string text1, GamepadButtonType buttonType2, string text2, GamepadButtonType buttonType3, string text3)
	{
		m_GamepadLegendActions[0].Show(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType1), text1.Trim());
		m_GamepadLegendActions[1].Show(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType2), text2.Trim());
		m_GamepadLegendActions[2].Show(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType3), text3.Trim());
		m_GamepadLegendActions[3].Hide();
	}

	public void ShowButtons(GamepadButtonType buttonType1, string text1, GamepadButtonType buttonType2, string text2, GamepadButtonType buttonType3, string text3, GamepadButtonType buttonType4, string text4)
	{
		m_GamepadLegendActions[0].Show(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType1), text1.Trim());
		m_GamepadLegendActions[1].Show(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType2), text2.Trim());
		m_GamepadLegendActions[2].Show(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType3), text3.Trim());
		m_GamepadLegendActions[3].Show(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType4), text4.Trim());
	}

	public void ShowButtonsLeft(GamepadButtonType buttonType, string text)
	{
		m_GamepadLegendActionsLeft[0].Show(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType), text.Trim());
		m_GamepadLegendActionsLeft[1].Hide();
		m_GamepadLegendActionsLeft[2].Hide();
		m_GamepadLegendActionsLeft[3].Hide();
	}

	public void ShowButtonsLeft(GamepadButtonType buttonType1, GamepadButtonType buttonType2, string text1)
	{
		m_GamepadLegendActionsLeft[0].Show2(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType1), GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType2), text1.Trim());
		m_GamepadLegendActionsLeft[1].Hide();
		m_GamepadLegendActionsLeft[2].Hide();
		m_GamepadLegendActionsLeft[3].Hide();
	}

	public void ShowButtonsLeft(GamepadButtonType buttonType1, GamepadButtonType buttonType2, string text1, GamepadButtonType buttonType3, GamepadButtonType buttonType4, string text2)
	{
		m_GamepadLegendActionsLeft[0].Show2(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType1), GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType2), text1.Trim());
		m_GamepadLegendActionsLeft[1].Show2(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType3), GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType4), text2.Trim());
		m_GamepadLegendActionsLeft[2].Hide();
		m_GamepadLegendActionsLeft[3].Hide();
	}

	public void ShowButtonsLeft(GamepadButtonType buttonType1, GamepadButtonType buttonType2, string text1, GamepadButtonType buttonType3, string text2)
	{
		m_GamepadLegendActionsLeft[0].Show2(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType1), GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType2), text1.Trim());
		m_GamepadLegendActionsLeft[1].Show(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType3), text2.Trim());
		m_GamepadLegendActionsLeft[2].Hide();
		m_GamepadLegendActionsLeft[3].Hide();
	}

	public void ShowButtonsLeft(GamepadButtonType buttonType1, string text1, GamepadButtonType buttonType2, string text2)
	{
		m_GamepadLegendActionsLeft[0].Show(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType1), text1.Trim());
		m_GamepadLegendActionsLeft[1].Show(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType2), text2.Trim());
		m_GamepadLegendActionsLeft[2].Hide();
		m_GamepadLegendActionsLeft[3].Hide();
	}

	public void ShowButtonsLeft(GamepadButtonType buttonType1, string text1, GamepadButtonType buttonType2, string text2, GamepadButtonType buttonType3, string text3)
	{
		m_GamepadLegendActionsLeft[0].Show(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType1), text1.Trim());
		m_GamepadLegendActionsLeft[1].Show(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType2), text2.Trim());
		m_GamepadLegendActionsLeft[2].Show(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType3), text3.Trim());
		m_GamepadLegendActionsLeft[3].Hide();
	}

	public void ShowButtonsLeft(GamepadButtonType buttonType1, GamepadButtonType buttonType2, string text1, GamepadButtonType buttonType3, string text2, GamepadButtonType buttonType4, string text3)
	{
		m_GamepadLegendActionsLeft[0].Show2(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType1), GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType2), text1.Trim());
		m_GamepadLegendActionsLeft[1].Show(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType3), text2.Trim());
		m_GamepadLegendActionsLeft[2].Show(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType4), text3.Trim());
		m_GamepadLegendActionsLeft[3].Hide();
	}

	public void ShowButtonsLeft(GamepadButtonType buttonType1, string text1, GamepadButtonType buttonType2, string text2, GamepadButtonType buttonType3, string text3, GamepadButtonType buttonType4, string text4)
	{
		m_GamepadLegendActionsLeft[0].Show(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType1), text1.Trim());
		m_GamepadLegendActionsLeft[1].Show(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType2), text2.Trim());
		m_GamepadLegendActionsLeft[2].Show(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType3), text3.Trim());
		m_GamepadLegendActionsLeft[3].Show(GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), buttonType4), text4.Trim());
	}

	public void Hide()
	{
		HideButtons();
		HideButtonsLeft();
	}

	public void HideButtons()
	{
		GamepadLegendAction[] gamepadLegendActions = m_GamepadLegendActions;
		for (int i = 0; i < gamepadLegendActions.Length; i++)
		{
			gamepadLegendActions[i].Hide();
		}
	}

	public void HideButtonsLeft()
	{
		GamepadLegendAction[] gamepadLegendActionsLeft = m_GamepadLegendActionsLeft;
		for (int i = 0; i < gamepadLegendActionsLeft.Length; i++)
		{
			gamepadLegendActionsLeft[i].Hide();
		}
	}

	public Sprite GetIcon(GamepadButtonType gamepadButtonType)
	{
		return GameUI.m_Instance.m_GamepadIconSets.GetIcon(GamepadManager.GetGamepadType(), gamepadButtonType);
	}

	public void Save()
	{
		m_GamepadLegendActionsSaved.Clear();
		for (int i = 0; i < m_GamepadLegendActions.Length; i++)
		{
			if (m_GamepadLegendActions[i].gameObject.activeInHierarchy)
			{
				m_GamepadLegendActionsSaved.Add(new Tuple<Sprite, string>(m_GamepadLegendActions[i].m_Icon.sprite, m_GamepadLegendActions[i].m_Label.text));
			}
		}
	}

	public void Restore()
	{
		GamepadLegendAction[] gamepadLegendActions = m_GamepadLegendActions;
		for (int i = 0; i < gamepadLegendActions.Length; i++)
		{
			gamepadLegendActions[i].Hide();
		}
		for (int j = 0; j < m_GamepadLegendActionsSaved.Count; j++)
		{
			m_GamepadLegendActions[j].Show(m_GamepadLegendActionsSaved[j].Item1, m_GamepadLegendActionsSaved[j].Item2);
		}
	}
}
