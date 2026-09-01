using Landfall.TABS.UI.WinConditions;
using Landfall.TABS_Input;
using UnityEngine;

namespace TFBGames
{
	public class WinConditionsBrowser : MonoBehaviour
	{
		[SerializeField]
		private WinConditionsComponent m_WinConditionsComponent;

		[SerializeField]
		private CodeAnimation m_ConditionPanelCodeAnimation;

		private ValueSlider m_ValueSlider;

		private InspectorPanel m_InspectorPanel;

		private WinConditionContentInspector m_WinConditionContentInspector;

		private bool m_IsOpen;

		private PlayerActions m_PlayerActions;

		private const int OPEN_INPUT_BUFFER = 5;

		private bool m_SetToOpen;

		private int m_OpenTriggeredOnFrame;

		private bool m_WasReopened;

		public bool WasReopened
		{
			set
			{
				m_WasReopened = value;
			}
		}

		public bool IsOpen
		{
			get
			{
				return m_IsOpen;
			}
			set
			{
				m_IsOpen = value;
			}
		}

		public CodeAnimation ConditionPanelCodeAnimation => m_ConditionPanelCodeAnimation;

		private void Awake()
		{
			m_PlayerActions = PlayerActions.Instance;
			m_WinConditionContentInspector = GetComponentInParent<WinConditionContentInspector>();
			m_ValueSlider = GetComponentInChildren<ValueSlider>();
			m_InspectorPanel = GetComponentInParent<InspectorPanel>();
		}

		private void Update()
		{
			if (m_SetToOpen && Time.frameCount >= m_OpenTriggeredOnFrame + 5)
			{
				m_IsOpen = true;
				m_SetToOpen = false;
				m_InspectorPanel.UpdateAddButton();
			}
			if (m_IsOpen && m_WinConditionContentInspector.IsOpen && m_PlayerActions != null)
			{
				HandleScrollInput();
				bool num = m_InspectorPanel.CanShowApplyButtonForInspectedCondition();
				bool flag = !m_InspectorPanel.IsAddingNewNewCondition && m_InspectorPanel.IsInspectingTimeCondition() && m_PlayerActions.m_AddVictoryConditions.WasPressed;
				if ((num && m_PlayerActions.m_AddVictoryConditionsFromInspector.WasPressed) || flag)
				{
					m_InspectorPanel.ApplyWinCondition();
					m_WinConditionsComponent.OpenVictoryConditionPanel(setOpen: false);
				}
			}
		}

		private void HandleScrollInput()
		{
			if (m_InspectorPanel.IsAddingNewNewCondition)
			{
				_ = m_WasReopened;
			}
		}

		public void Open()
		{
			m_OpenTriggeredOnFrame = Time.frameCount;
			m_SetToOpen = true;
		}
	}
}
