using System;
using Landfall.TABS.AI.Systems;
using Landfall.TABS.GameMode;
using Landfall.TABS.UI.Widgets.List;
using Landfall.TABS.WinConditions;
using TFBGames;
using Unity.Entities;
using UnityEngine;

namespace Landfall.TABS.UI.WinConditions
{
	public class WinConditionListController : MonoBehaviour
	{
		private TeamSystem m_teamSystem;

		private UIList m_uiList;

		private PanelSlider m_panelSlider;

		[SerializeField]
		private WinConditionsTeamPanel m_WinConditionsTeamPanel;

		[SerializeField]
		private InspectorPanel m_inspectorPanel;

		private WinConditionPropagator m_winConditionPropagator;

		public Team m_Team;

		public InspectorPanel InspectorPanel => m_inspectorPanel;

		private void Awake()
		{
			m_winConditionPropagator = ServiceLocator.GetService<GameModeService>().CurrentGameMode.WinConditionPropagator;
			m_teamSystem = World.Active.GetOrCreateManager<TeamSystem>();
			m_panelSlider = GetComponentInParent<PanelSlider>();
			m_uiList = GetComponent<UIList>();
			UIList uiList = m_uiList;
			uiList.OnItemAddedCallback = (UIList.OnItemAddedDelegate)Delegate.Combine(uiList.OnItemAddedCallback, (UIList.OnItemAddedDelegate)delegate(UIListItem item)
			{
				item.OnSubmitCallback.AddListener(delegate
				{
					OnClickedInspectorButton(item);
				});
			});
		}

		private WinCondition GetItemCondition(Guid guid)
		{
			return m_winConditionPropagator.GetWinConditionFromTeam(m_Team, guid);
		}

		private void StartHighlightUnit(UIListItem item)
		{
			if (GetItemCondition((Guid)item.UserData) is MustKillUnitWinCondition)
			{
				Unit killUnit = GetKillUnit((Guid)item.UserData);
				if (!(killUnit == null))
				{
					killUnit.SetHighlight(Color.white);
				}
			}
		}

		private void EndHighlightUnit(UIListItem item)
		{
			if (GetItemCondition((Guid)item.UserData) is MustKillUnitWinCondition)
			{
				Unit killUnit = GetKillUnit((Guid)item.UserData);
				if (!(killUnit == null))
				{
					killUnit.RemoveHighlight();
				}
			}
		}

		public Unit GetKillUnit(Guid guid)
		{
			WinCondition itemCondition = GetItemCondition(guid);
			foreach (Unit allUnit in m_teamSystem.GetAllUnits())
			{
				if (allUnit.RuntimeReference != null)
				{
					_ = allUnit.RuntimeReference.Guid;
					if (allUnit.RuntimeReference.Guid == itemCondition.GetUnitToKill().Guid)
					{
						return allUnit;
					}
				}
			}
			return null;
		}

		private void OnClickedInspectorButton(UIListItem item)
		{
			if (m_WinConditionsTeamPanel.CanInspectCondition())
			{
				if (m_panelSlider != null)
				{
					m_panelSlider.TweenInRightPanel();
				}
				m_inspectorPanel.PlayContentAnimatorIn();
				Guid winConditionGuid = (Guid)item.UserData;
				m_inspectorPanel.BindWinCondition(winConditionGuid, item);
			}
		}
	}
}
