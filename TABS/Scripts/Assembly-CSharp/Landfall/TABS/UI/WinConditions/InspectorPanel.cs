using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Landfall.TABS.GameMode;
using Landfall.TABS.UI.Widgets.List;
using Landfall.TABS.WinConditions;
using Landfall.TABS_Input;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS.UI.WinConditions
{
	public class InspectorPanel : MonoBehaviour
	{
		[SerializeField]
		private Team m_team;

		private PanelSlider m_panelSlider;

		[SerializeField]
		private LocalizeText m_descriptionText;

		[SerializeField]
		private ValueSlider m_conditionValueSlider;

		[SerializeField]
		private WinConditionsComponent m_WinConditionsComponent;

		private List<string> m_winConditionEntries = new List<string>();

		private WinConditionPropagator m_winConditionPropagator;

		private FieldController m_fieldController;

		[SerializeField]
		private UIList m_teamList;

		[SerializeField]
		private GameObject m_AddConditionButtonGameObject;

		private Button m_AddConditionButton;

		[SerializeField]
		private GameObject m_AddConditionGlyph;

		private TMP_Text m_addConditionButtonText;

		[SerializeField]
		private GameObject m_EditConditionGlyph;

		[SerializeField]
		private GameObject m_EditConditionButton;

		[SerializeField]
		private GameObject[] m_ValueSliderIcons;

		[SerializeField]
		private ReferenceRenderer m_RefRenderer;

		private WinConditionsIcons m_WinConditionIcons;

		private ActionGlyphText m_ActionGlyph;

		private Guid m_boundGuid;

		private UIListItem m_boundItem;

		private WinCondition m_currentlyInspectedCondition;

		private WinCondition m_cachedCondition;

		private bool m_UsingGamepad;

		private float m_TimeWinconditionTimeWhenOpened;

		private ReferenceRequest<Unit> m_UnitWhenConditionOpened;

		private bool m_isShowing;

		private bool m_IsAddingNewCondition;

		private bool m_DidReapplyRenderer;

		private RuntimeReferenceService m_RuntimeReferenceService;

		public Team Team => m_team;

		public LocalizeText DescriptionText => m_descriptionText;

		public ValueSlider ConditionValueSlider => m_conditionValueSlider;

		public WinCondition[] GetTeamConditions => m_winConditionPropagator.GetWinConditionsForTeam(m_team);

		public UIList TeamList => m_teamList;

		public bool IsAddingNewNewCondition => m_IsAddingNewCondition;

		private void Awake()
		{
			m_fieldController = GetComponentInChildren<FieldController>();
			m_panelSlider = GetComponentInParent<PanelSlider>();
			m_conditionValueSlider = GetComponentInChildren<ValueSlider>();
			m_winConditionPropagator = ServiceLocator.GetService<GameModeService>().CurrentGameMode.WinConditionPropagator;
			m_addConditionButtonText = m_AddConditionButtonGameObject.GetComponentInChildren<TMP_Text>();
			m_ActionGlyph = m_AddConditionButtonGameObject.GetComponentInChildren<ActionGlyphText>();
			m_RuntimeReferenceService = ServiceLocator.GetService<RuntimeReferenceService>();
			if (m_WinConditionsComponent == null)
			{
				m_WinConditionsComponent = UnityEngine.Object.FindObjectOfType<WinConditionsComponent>();
			}
			m_WinConditionIcons = GetComponent<WinConditionsIcons>();
			SetUpButtonListeners();
			FillWinConditionValues();
		}

		private void SetUpButtonListeners()
		{
			m_AddConditionButton = m_AddConditionButtonGameObject.GetComponent<Button>();
			if (m_AddConditionButton != null)
			{
				m_AddConditionButton.onClick.AddListener(ApplyWinCondition);
				m_AddConditionButton.onClick.AddListener(delegate
				{
					m_WinConditionsComponent.OpenVictoryConditionPanel(setOpen: false);
				});
			}
		}

		public void PlayContentAnimatorIn()
		{
			if (m_WinConditionsComponent != null)
			{
				m_WinConditionsComponent.OpenVictoryConditionPanel(setOpen: true);
			}
		}

		public void PlayContentAnimatorOut()
		{
			if (m_WinConditionsComponent != null)
			{
				m_WinConditionsComponent.OpenVictoryConditionPanel(setOpen: false);
			}
		}

		public void UpdateFromCurrentWinconditions()
		{
			if (m_WinConditionsComponent.IsActive)
			{
				m_teamList.RemoveAllItems();
				WinCondition[] winConditionsForTeam = m_winConditionPropagator.GetWinConditionsForTeam(m_team);
				foreach (WinCondition winCondition in winConditionsForTeam)
				{
					AddPremadeWincondition(winCondition);
				}
			}
		}

		public void BindWinCondition(Guid winConditionGuid, UIListItem listItem)
		{
			m_boundGuid = winConditionGuid;
			m_boundItem = listItem;
			m_isShowing = true;
			FillWinConditionValues();
			if (winConditionGuid == Guid.Empty)
			{
				m_IsAddingNewCondition = true;
				m_cachedCondition = null;
				InspectNewCondition(0);
				ShowAddButton();
				SetValueSliderIconsVisible(visible: true);
			}
			else
			{
				m_IsAddingNewCondition = false;
				SetInspectedCondition(winConditionGuid);
				SetValueSliderIconsVisible(visible: false);
			}
		}

		public void OnWinConditionChanged(int newIndex)
		{
			InspectNewCondition(newIndex);
			if (m_boundGuid != Guid.Empty)
			{
				ShowApplyAndDeleteButton();
			}
			else
			{
				ShowAddButton();
			}
		}

		private void InspectNewCondition(int newIndex)
		{
			FillWinConditionValues();
			string identifier = m_winConditionEntries[newIndex];
			WinCondition winCondition = m_winConditionPropagator.WinConditionFinder.CreateWinCondition(identifier);
			m_conditionValueSlider.SetValueIndex(newIndex, triggerOnChange: false);
			m_conditionValueSlider.SetIcon(GetUpdateConditionIcons(winCondition));
			m_currentlyInspectedCondition = winCondition;
			m_fieldController.BindObject(m_currentlyInspectedCondition);
			string[] args;
			string description = m_winConditionPropagator.WinConditionFinder.GetDescription(identifier, out args);
			m_descriptionText.Args = args;
			m_descriptionText.LocaleID = description;
			m_fieldController.UpdateContentHeight();
			if (winCondition is MustKillUnitWinCondition)
			{
				m_fieldController.TurnOnEditButton();
				m_WinConditionsComponent.UpdateBackButtons();
			}
			else
			{
				m_fieldController.TurnOffEditButton();
				m_WinConditionsComponent.UpdateBackButtons();
			}
		}

		private void SetInspectedCondition(Guid guid)
		{
			WinCondition winConditionFromTeam = m_winConditionPropagator.GetWinConditionFromTeam(m_team, guid);
			m_winConditionPropagator.RemoveWinCondition(m_team, winConditionFromTeam);
			m_cachedCondition = winConditionFromTeam;
			FillWinConditionValues();
			if (winConditionFromTeam == null)
			{
				Debug.LogError($"Could not find WinCondition with guid {guid}");
				return;
			}
			m_conditionValueSlider.SetValueIndex(GetConditionIndexFromID(winConditionFromTeam.GetType().Name), triggerOnChange: false);
			m_conditionValueSlider.SetIcon(GetUpdateConditionIcons(winConditionFromTeam));
			m_currentlyInspectedCondition = winConditionFromTeam;
			m_fieldController.BindObject(m_currentlyInspectedCondition);
			if (winConditionFromTeam is MustKillUnitWinCondition mustKillUnitWinCondition)
			{
				m_WinConditionsComponent.UpdateBackButtons();
				m_UnitWhenConditionOpened = mustKillUnitWinCondition.UnitToKill;
			}
			else
			{
				m_fieldController.TurnOffEditButton();
				m_WinConditionsComponent.UpdateBackButtons();
			}
			if (winConditionFromTeam is TimeLimitWinCondition timeLimitWinCondition)
			{
				m_TimeWinconditionTimeWhenOpened = timeLimitWinCondition.SecondsToSurvive;
			}
			string[] args;
			string description = m_currentlyInspectedCondition.GetDescription(out args);
			m_descriptionText.Args = args;
			m_descriptionText.LocaleID = description;
			m_fieldController.UpdateContentHeight();
			ShowApplyAndDeleteButton();
		}

		private int GetConditionIndexFromID(string id)
		{
			for (int i = 0; i < m_winConditionEntries.Count; i++)
			{
				if (m_winConditionEntries[i] == id)
				{
					return i;
				}
			}
			return -1;
		}

		public void BackToListView(bool addCachedCondition = true)
		{
			if (m_panelSlider != null)
			{
				m_panelSlider.TweenInLeftPanel();
			}
			if (m_boundGuid != Guid.Empty)
			{
				if (addCachedCondition)
				{
					m_winConditionPropagator.AddWinCondition(m_team, m_cachedCondition);
				}
				m_cachedCondition = null;
				m_boundGuid = Guid.Empty;
			}
			m_UnitWhenConditionOpened = null;
			m_isShowing = false;
		}

		public bool IsInspectingMustWinCondition()
		{
			return m_currentlyInspectedCondition is MustKillUnitWinCondition;
		}

		public bool IsInspectingTimeCondition()
		{
			return m_currentlyInspectedCondition is TimeLimitWinCondition;
		}

		public bool IsInspectingLastTeamStandingTeamCondition()
		{
			return m_currentlyInspectedCondition is LastTeamStandingWinCondition;
		}

		public bool CanApplyInspectedMustKillCondition()
		{
			if (!IsInspectingMustWinCondition())
			{
				return false;
			}
			return m_currentlyInspectedCondition.GetUnitToKill() != null;
		}

		public bool CanShowApplyButtonForInspectedCondition()
		{
			if (IsInspectingMustWinCondition())
			{
				if (CanApplyInspectedMustKillCondition())
				{
					return true;
				}
				return false;
			}
			if (IsInspectingLastTeamStandingTeamCondition() && m_IsAddingNewCondition)
			{
				return true;
			}
			return IsInspectingTimeCondition();
		}

		public void ApplyWinCondition()
		{
			if (m_boundGuid == Guid.Empty)
			{
				int selectedIndex = m_conditionValueSlider.SelectedIndex;
				string identifier = m_winConditionEntries[selectedIndex];
				WinCondition currentlyInspectedCondition = m_currentlyInspectedCondition;
				m_winConditionPropagator.AddWinCondition(m_team, currentlyInspectedCondition);
				UIListItem uIListItem = m_teamList.AddItem(m_winConditionPropagator.WinConditionFinder.GetDisplayName(identifier));
				uIListItem.UserData = currentlyInspectedCondition.Guid;
				m_boundItem = uIListItem;
				m_boundItem.UserData = currentlyInspectedCondition.Guid;
				string[] args;
				string descriptionForListItem = currentlyInspectedCondition.GetDescriptionForListItem(out args);
				m_boundItem.SetDisplayText(descriptionForListItem, args);
			}
			else
			{
				m_boundItem.UserData = m_currentlyInspectedCondition.Guid;
				string[] args2;
				string descriptionForListItem2 = m_currentlyInspectedCondition.GetDescriptionForListItem(out args2);
				m_boundItem.SetDisplayText(descriptionForListItem2, args2);
				m_winConditionPropagator.AddWinCondition(m_team, m_currentlyInspectedCondition);
			}
			m_boundItem.ConditionIcon.sprite = GetUpdateConditionIcons();
			m_isShowing = false;
			m_UnitWhenConditionOpened = null;
		}

		private Sprite GetUpdateConditionIcons(WinCondition winCondition = null)
		{
			if (winCondition == null)
			{
				winCondition = m_currentlyInspectedCondition;
			}
			if (winCondition is MustKillUnitWinCondition)
			{
				return m_WinConditionIcons.GetImage(WinConditionsIcons.ConditionType.MustKill);
			}
			if (winCondition is TimeLimitWinCondition)
			{
				return m_WinConditionIcons.GetImage(WinConditionsIcons.ConditionType.TimeLimit);
			}
			if (winCondition is LastTeamStandingWinCondition)
			{
				return m_WinConditionIcons.GetImage(WinConditionsIcons.ConditionType.LastTeamStanding);
			}
			return m_WinConditionIcons.GetImage(WinConditionsIcons.ConditionType.Default);
		}

		public void ApplyAndAbort()
		{
			m_DidReapplyRenderer = false;
			if (m_currentlyInspectedCondition != null)
			{
				if (m_currentlyInspectedCondition is TimeLimitWinCondition timeLimitWinCondition)
				{
					timeLimitWinCondition.SecondsToSurvive = m_TimeWinconditionTimeWhenOpened;
					m_currentlyInspectedCondition = timeLimitWinCondition;
				}
				if (m_currentlyInspectedCondition is MustKillUnitWinCondition mustKillUnitWinCondition)
				{
					if (m_UnitWhenConditionOpened != null && !mustKillUnitWinCondition.UnitToKill.Equals(m_UnitWhenConditionOpened))
					{
						m_currentlyInspectedCondition.OnRemovedFromgGUI();
						ReferenceRequest<Unit> unitReference = new ReferenceRequest<Unit>(m_UnitWhenConditionOpened.Guid.ToString());
						m_RefRenderer.AddIconedUnit(unitReference);
					}
					mustKillUnitWinCondition.UnitToKill = m_UnitWhenConditionOpened;
					m_currentlyInspectedCondition = mustKillUnitWinCondition;
				}
			}
			if (m_isShowing)
			{
				ApplyWinCondition();
				BackToListView(addCachedCondition: false);
			}
		}

		public void BackButtonActions()
		{
			if (!m_WinConditionsComponent.m_IsSelectingUnit)
			{
				StartCoroutine(Delay());
			}
			IEnumerator Delay()
			{
				yield return null;
				if (m_IsAddingNewCondition)
				{
					m_WinConditionsComponent.m_animateMainPanels = false;
					m_WinConditionsComponent.OpenVictoryConditionPanel(setOpen: false);
					m_WinConditionsComponent.OpenConditionBrowserPanel(setOpen: true);
					m_WinConditionsComponent.m_animateMainPanels = true;
					if (m_currentlyInspectedCondition is MustKillUnitWinCondition)
					{
						m_currentlyInspectedCondition.OnRemovedFromgGUI();
					}
				}
				else
				{
					m_WinConditionsComponent.OpenVictoryConditionPanel(setOpen: false);
					ApplyAndAbort();
				}
				m_WinConditionsComponent.UpdateBackButtons();
			}
		}

		public void UpdateAddButton()
		{
			if (m_AddConditionButtonGameObject != null)
			{
				bool active = CanShowApplyButtonForInspectedCondition();
				m_AddConditionButtonGameObject.SetActive(active);
				m_AddConditionGlyph.SetActive(active);
			}
		}

		public void RemoveInspectedWinCondition()
		{
			if (!(m_boundGuid == Guid.Empty))
			{
				m_currentlyInspectedCondition?.OnRemovedFromgGUI();
				m_teamList.RemoveItem(m_boundItem);
				m_boundItem = null;
				m_currentlyInspectedCondition = null;
				m_cachedCondition = null;
				m_boundGuid = Guid.Empty;
				m_isShowing = false;
				UpdateFromCurrentWinconditions();
			}
		}

		public void AddPremadeWincondition(WinCondition winCondition)
		{
			string identifier = winCondition.GetType().Name;
			UIListItem uIListItem = m_teamList.AddItem(m_winConditionPropagator.WinConditionFinder.GetDisplayName(identifier));
			uIListItem.UserData = winCondition.Guid;
			m_boundItem = uIListItem;
			m_boundItem.UserData = winCondition.Guid;
			string[] args;
			string descriptionForListItem = winCondition.GetDescriptionForListItem(out args);
			m_boundItem.SetDisplayText(descriptionForListItem, args);
			m_boundItem.ConditionIcon.sprite = GetUpdateConditionIcons(winCondition);
		}

		public void CheckWinConditionExists(UIListItem conditionItem)
		{
			if (!(conditionItem == null))
			{
				WinCondition winConditionFromTeam = m_winConditionPropagator.GetWinConditionFromTeam(m_team, (Guid)conditionItem.UserData);
				if (winConditionFromTeam == null || string.IsNullOrEmpty(winConditionFromTeam.GetBattleDescription(invertDescription: false)))
				{
					BindWinCondition((Guid)conditionItem.UserData, conditionItem);
					RemoveInspectedWinCondition();
				}
			}
		}

		private void FillWinConditionValues()
		{
			m_winConditionEntries.Clear();
			List<string> identifiers = m_winConditionPropagator.WinConditionFinder.GetIdentifiers();
			WinCondition[] winConditionsForTeam = m_winConditionPropagator.GetWinConditionsForTeam(m_team);
			m_conditionValueSlider.ClearOptions();
			List<string> list = new List<string>();
			for (int i = 0; i < identifiers.Count; i++)
			{
				bool flag = false;
				WinCondition[] array = winConditionsForTeam;
				foreach (WinCondition winCondition in array)
				{
					if (winCondition.GetType().GetCustomAttribute<WinConditionIDAttribute>().IsExclusive && winCondition.GetType().Name == identifiers[i])
					{
						flag = true;
					}
				}
				if (m_cachedCondition != null && m_cachedCondition.GetType().Name == identifiers[i])
				{
					flag = false;
				}
				if (!flag)
				{
					string displayName = m_winConditionPropagator.WinConditionFinder.GetDisplayName(identifiers[i]);
					list.Add(displayName);
					m_winConditionEntries.Add(identifiers[i]);
				}
			}
			m_conditionValueSlider.AddOptions(list);
		}

		private void ShowApplyAndDeleteButton()
		{
			if (m_addConditionButtonText != null && m_EditConditionButton != null)
			{
				bool flag = m_currentlyInspectedCondition is MustKillUnitWinCondition;
				if (m_ActionGlyph != null)
				{
					m_ActionGlyph.UpdateActionNames(flag ? "Add Victory Conditions From Inspector" : "Add Victory Conditions", "APPLY");
				}
				m_AddConditionButtonGameObject.SetActive(!(m_currentlyInspectedCondition is LastTeamStandingWinCondition));
				m_AddConditionGlyph.SetActive(!(m_currentlyInspectedCondition is LastTeamStandingWinCondition));
				bool active = PlayerActions.Instance.InputType == InputType.Controller && flag;
				m_EditConditionButton.gameObject.SetActive(value: false);
				m_EditConditionGlyph.SetActive(active);
			}
		}

		private void ShowAddButton()
		{
			UpdateAddButton();
			m_addConditionButtonText.gameObject.SetActive(m_IsAddingNewCondition);
			if (m_ActionGlyph != null)
			{
				m_ActionGlyph.UpdateActionNames("Add Victory Conditions From Inspector", "ADD");
			}
			bool active = PlayerActions.Instance.InputType == InputType.Controller && m_currentlyInspectedCondition is MustKillUnitWinCondition;
			m_EditConditionButton.SetActive(value: false);
			m_EditConditionGlyph.SetActive(active);
		}

		private void SetValueSliderIconsVisible(bool visible)
		{
		}
	}
}
