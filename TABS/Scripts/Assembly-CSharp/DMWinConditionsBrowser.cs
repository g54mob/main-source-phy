using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS.UI.WinConditions;
using Landfall.TABS.WinConditions;
using Landfall.TABS_Input;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DMWinConditionsBrowser : MonoBehaviour
{
	[SerializeField]
	private TMP_Text m_conditionDescription;

	[SerializeField]
	private Transform m_conditionParent;

	[SerializeField]
	private GameObject m_conditionTemplate;

	[SerializeField]
	private WinConditionsComponent m_winConditionsComponent;

	[SerializeField]
	private InspectorPanel m_redPanel;

	[SerializeField]
	private InspectorPanel m_bluePanel;

	private WinConditionsIcons m_winConIcons;

	private WinConditionPropagator m_winConditionPropagator;

	private bool m_isRedTeam = true;

	public bool IsOpen => GetComponent<CanvasGroup>().interactable;

	private void Start()
	{
		m_winConIcons = GetComponent<WinConditionsIcons>();
		m_winConditionPropagator = ServiceLocator.GetService<GameModeService>().CurrentGameMode.WinConditionPropagator;
	}

	private void Update()
	{
		if (IsOpen && PlayerActions.Instance.m_back.WasPressed)
		{
			StartCoroutine(Delay());
		}
		IEnumerator Delay()
		{
			yield return null;
			m_winConditionsComponent.OpenConditionBrowserPanel(setOpen: false);
		}
	}

	public void Open()
	{
		GetComponent<CodeAnimation>().PlayIn();
		Build();
	}

	public void Close()
	{
		GetComponent<CodeAnimation>().PlayOut();
	}

	public void SetTeam(bool isRedTeam)
	{
		m_isRedTeam = isRedTeam;
	}

	private void Build()
	{
		foreach (Transform item in m_conditionParent)
		{
			UnityEngine.Object.Destroy(item.gameObject);
		}
		List<string> identifiers = m_winConditionPropagator.WinConditionFinder.GetIdentifiers();
		WinCondition[] winConditionsForTeam = m_winConditionPropagator.GetWinConditionsForTeam((!m_isRedTeam) ? Team.Blue : Team.Red);
		int num = 0;
		new List<string>();
		for (int i = 0; i < identifiers.Count; i++)
		{
			string text = identifiers[i];
			bool flag = false;
			WinCondition[] array = winConditionsForTeam;
			foreach (WinCondition winCondition in array)
			{
				if (winCondition.GetType().GetCustomAttribute<WinConditionIDAttribute>().IsExclusive && winCondition.GetType().Name == text)
				{
					flag = true;
				}
			}
			if (flag)
			{
				num++;
				continue;
			}
			string displayName = m_winConditionPropagator.WinConditionFinder.GetDisplayName(text);
			string[] args;
			string description = m_winConditionPropagator.WinConditionFinder.GetDescription(text, out args);
			Type conditionType = m_winConditionPropagator.WinConditionFinder.GetConditionType(text);
			GameObject obj = UnityEngine.Object.Instantiate(m_conditionTemplate, m_conditionParent);
			obj.SetActive(value: true);
			Image componentInChildrenExclusive = obj.GetComponentInChildrenExclusive<Image>();
			if (componentInChildrenExclusive != null)
			{
				componentInChildrenExclusive.sprite = GetUpdateConditionIcons(conditionType);
			}
			LocalizeText componentInChildren = obj.GetComponentInChildren<LocalizeText>();
			if (componentInChildren != null)
			{
				componentInChildren.LocaleID = displayName;
			}
			DMSetTextOnHover componentInChildren2 = obj.GetComponentInChildren<DMSetTextOnHover>();
			if (componentInChildren2 != null)
			{
				componentInChildren2.onEnterText = description;
			}
			Button component = obj.GetComponent<Button>();
			if (!(component != null))
			{
				continue;
			}
			int winConIndex = i - num;
			component.onClick.AddListener(delegate
			{
				if (m_isRedTeam)
				{
					m_redPanel.OnWinConditionChanged(winConIndex);
				}
				else
				{
					m_bluePanel.OnWinConditionChanged(winConIndex);
				}
				m_winConditionsComponent.m_animateMainPanels = false;
				m_winConditionsComponent.OpenConditionBrowserPanel(setOpen: false);
				m_winConditionsComponent.OpenVictoryConditionPanel(setOpen: true);
				m_winConditionsComponent.m_animateMainPanels = true;
			});
		}
		StartCoroutine(Delay());
		IEnumerator Delay()
		{
			yield return null;
			m_conditionParent.GetChild(0)?.GetComponent<Button>()?.Select();
		}
	}

	private Sprite GetUpdateConditionIcons(Type winCondition = null)
	{
		if (winCondition == typeof(MustKillUnitWinCondition))
		{
			return m_winConIcons.GetImage(WinConditionsIcons.ConditionType.MustKill);
		}
		if (winCondition == typeof(TimeLimitWinCondition))
		{
			return m_winConIcons.GetImage(WinConditionsIcons.ConditionType.TimeLimit);
		}
		if (winCondition == typeof(LastTeamStandingWinCondition))
		{
			return m_winConIcons.GetImage(WinConditionsIcons.ConditionType.LastTeamStanding);
		}
		return m_winConIcons.GetImage(WinConditionsIcons.ConditionType.Default);
	}
}
