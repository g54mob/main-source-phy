using System;
using System.Collections;
using System.Collections.Generic;
using Landfall.TABS.UI.Widgets.List;
using Landfall.TABS.UI.WinConditions;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TFBGames
{
	public class WinConditionsTeamPanel : MonoBehaviour
	{
		[SerializeField]
		private GameObject m_Background;

		[SerializeField]
		private GameObject m_ConditionView;

		[SerializeField]
		private GameObject m_ConditionsListContainer;

		[SerializeField]
		private GameObject m_GamepadGlyphContainer;

		[SerializeField]
		private Button m_AddConditionButton;

		[SerializeField]
		private Button m_AddConditionButtonMouse;

		[SerializeField]
		private Button m_RemoveAllButtonMouse;

		[SerializeField]
		private Button m_BackButton;

		[SerializeField]
		private WinConditionsBrowser m_ConditionBrowser;

		[SerializeField]
		private WinConditionContentInspector m_WinConditionContentInspector;

		[SerializeField]
		private InspectorPanel m_WinConditionInspectorPanel;

		[SerializeField]
		private WinConditionsComponent m_WinConditionsComponent;

		[SerializeField]
		private Image m_RemoveCircleFill;

		[SerializeField]
		private HighlightColorChange[] m_ButtonImageBorders;

		private const float LONG_HOLD_TO_REMOVE_TIME = 1f;

		private const float SHORT_PRESS_REMOVE_TIME = 0.25f;

		private float m_TimeSinceRemoveHold;

		private bool m_IsActive;

		private bool m_IsFocused;

		private bool m_Rebuild;

		private PlayerActions m_PlayerActions;

		private IList<Selectable> selectablesInChildren;

		public bool IsFocused => m_IsFocused;

		private void Awake()
		{
			m_PlayerActions = PlayerActions.Instance;
		}

		private void Update()
		{
			if (m_PlayerActions == null || !m_IsActive || !m_WinConditionsComponent.IsActive)
			{
				return;
			}
			if (m_PlayerActions.m_back.WasPressed)
			{
				StartCoroutine(Delay());
			}
			if (m_Rebuild && m_IsFocused)
			{
				RebuildNavigation();
			}
			if (m_PlayerActions.m_AddVictoryConditions.WasPressed && !m_WinConditionContentInspector.IsOpen && m_IsFocused)
			{
				m_AddConditionButton.onClick.Invoke();
				m_ConditionBrowser.Open();
				EventSystem.current.SetSelectedGameObject(null);
				m_IsFocused = false;
			}
			if (m_PlayerActions.m_RemoveVictoryConditions.WasReleased && !m_WinConditionContentInspector.IsOpen)
			{
				GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
				if (currentSelectedGameObject != null)
				{
					UIListItem componentInParent = currentSelectedGameObject.GetComponentInParent<UIListItem>();
					if (componentInParent != null && m_WinConditionInspectorPanel != null && m_TimeSinceRemoveHold < 0.25f)
					{
						m_WinConditionInspectorPanel.BindWinCondition((Guid)componentInParent.UserData, componentInParent);
						m_WinConditionInspectorPanel.RemoveInspectedWinCondition();
						m_Rebuild = true;
					}
				}
				m_TimeSinceRemoveHold = 0f;
				if (m_RemoveCircleFill != null)
				{
					m_RemoveCircleFill.fillAmount = 0f;
				}
			}
			if (m_PlayerActions.m_RemoveVictoryConditions.WasPressed)
			{
				m_TimeSinceRemoveHold = 0f;
				if (m_RemoveCircleFill != null)
				{
					m_RemoveCircleFill.fillAmount = 0f;
				}
			}
			if (m_PlayerActions.m_RemoveVictoryConditions.IsPressed && m_IsActive && m_ConditionsListContainer != null && m_ConditionsListContainer.transform.childCount > 0)
			{
				m_TimeSinceRemoveHold += Time.unscaledDeltaTime;
				if (m_RemoveCircleFill != null)
				{
					m_RemoveCircleFill.fillAmount += Time.deltaTime * 1f;
				}
			}
			IEnumerator Delay()
			{
				yield return 2;
				if (m_BackButton != null && m_WinConditionContentInspector.IsOpen)
				{
					m_WinConditionsComponent.m_animateMainPanels = !m_WinConditionInspectorPanel.IsAddingNewNewCondition;
					m_BackButton.onClick.Invoke();
					m_WinConditionsComponent.m_animateMainPanels = true;
				}
			}
		}

		public void SetAsActiveTeam(bool isActiveTeam)
		{
			m_IsActive = isActiveTeam;
			m_Rebuild = isActiveTeam;
			m_GamepadGlyphContainer.SetActive(isActiveTeam);
			m_ConditionView.SetActive(isActiveTeam);
			m_WinConditionInspectorPanel.gameObject.SetActive(isActiveTeam);
			if (isActiveTeam)
			{
				m_Background.transform.SetAsFirstSibling();
			}
			else
			{
				m_Background.transform.SetAsLastSibling();
			}
			Focused(isActiveTeam);
		}

		public void UpdateWinConditionsList()
		{
			if (m_WinConditionInspectorPanel == null)
			{
				return;
			}
			m_WinConditionInspectorPanel.UpdateFromCurrentWinconditions();
			List<UIListItem> items = m_WinConditionInspectorPanel.TeamList.Items;
			for (int num = items.Count - 1; num > -1; num--)
			{
				UIListItem uIListItem = items[num];
				if (uIListItem != null)
				{
					m_WinConditionInspectorPanel.CheckWinConditionExists(uIListItem);
				}
			}
		}

		public void Focused(bool paused)
		{
			m_IsFocused = paused;
			m_Rebuild = paused;
			if (m_AddConditionButtonMouse != null)
			{
				m_AddConditionButtonMouse.interactable = paused;
			}
			if (m_RemoveAllButtonMouse != null)
			{
				m_RemoveAllButtonMouse.interactable = paused;
			}
			if (m_ButtonImageBorders != null)
			{
				HighlightColorChange[] buttonImageBorders = m_ButtonImageBorders;
				for (int i = 0; i < buttonImageBorders.Length; i++)
				{
					buttonImageBorders[i].SetInteractable(paused);
				}
			}
			if (!(m_ConditionsListContainer != null))
			{
				return;
			}
			selectablesInChildren = GetSelectablesFromConditionList(m_ConditionsListContainer);
			if (selectablesInChildren == null)
			{
				return;
			}
			foreach (Selectable selectablesInChild in selectablesInChildren)
			{
				selectablesInChild.interactable = paused;
			}
		}

		public bool CanInspectCondition()
		{
			if (!m_WinConditionContentInspector.IsOpen)
			{
				return IsFocused;
			}
			return false;
		}

		public void ReOpenBrowser()
		{
			m_ConditionBrowser.Open();
			m_ConditionBrowser.WasReopened = true;
		}

		public void RemoveSelectedCondition(UIListItem listItem)
		{
			if (IsFocused && m_IsActive)
			{
				if (listItem != null && m_WinConditionInspectorPanel != null)
				{
					m_WinConditionInspectorPanel.BindWinCondition((Guid)listItem.UserData, listItem);
					m_WinConditionInspectorPanel.RemoveInspectedWinCondition();
				}
				m_WinConditionsComponent.UpdateBackButtons();
				m_Rebuild = true;
			}
		}

		private void RebuildNavigation()
		{
			if (m_ConditionsListContainer != null)
			{
				selectablesInChildren = GetSelectablesFromConditionList(m_ConditionsListContainer);
			}
			if (selectablesInChildren != null && selectablesInChildren.Count > 0)
			{
				UIHelpers.CreateExplicitLinearNavigation(selectablesInChildren, horizontal: false);
				if (m_WinConditionsComponent != null && m_WinConditionsComponent.AutoHighlightListItem)
				{
					Selectable selectable = selectablesInChildren[0];
					if (selectable != null)
					{
						selectable.Select();
					}
				}
			}
			m_Rebuild = false;
		}

		private IList<Selectable> GetSelectablesFromConditionList(GameObject container)
		{
			UIListItem[] componentsInChildren = container.GetComponentsInChildren<UIListItem>();
			if (componentsInChildren == null || componentsInChildren.Length == 0)
			{
				return null;
			}
			IList<Selectable> list = new List<Selectable>();
			UIListItem[] array = componentsInChildren;
			foreach (UIListItem uIListItem in array)
			{
				if (uIListItem.Selectable != null)
				{
					list.Add(uIListItem.Selectable);
				}
			}
			return list;
		}

		public void RemoveAllConditions()
		{
			if (m_WinConditionContentInspector.IsOpen || m_ConditionsListContainer == null || m_ConditionsListContainer.transform.childCount == 0 || !m_IsActive)
			{
				return;
			}
			UIListItem[] componentsInChildren = m_ConditionsListContainer.GetComponentsInChildren<UIListItem>();
			if (componentsInChildren == null || componentsInChildren.Length < 1)
			{
				return;
			}
			for (int num = componentsInChildren.Length - 1; num > -1; num--)
			{
				UIListItem uIListItem = componentsInChildren[num];
				if (uIListItem != null && m_WinConditionInspectorPanel != null)
				{
					m_WinConditionInspectorPanel.BindWinCondition((Guid)uIListItem.UserData, uIListItem);
					m_WinConditionInspectorPanel.RemoveInspectedWinCondition();
				}
			}
			m_WinConditionsComponent.UpdateBackButtons();
			m_Rebuild = true;
		}
	}
}
