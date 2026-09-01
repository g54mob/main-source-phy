using System;
using Landfall.TABS.UI.Widgets.Fields;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS.UI.WinConditions
{
	public class AddWinConditionButton : MonoBehaviour
	{
		[SerializeField]
		private WinConditionListController m_WinConditionController;

		private Button m_Button;

		private UIPointerEvents m_PointerEvents;

		private WinConditionListController m_ListController;

		private void Awake()
		{
			m_Button = GetComponent<Button>();
			m_PointerEvents = GetComponent<UIPointerEvents>();
		}

		private void Start()
		{
			if (m_Button != null)
			{
				m_Button.onClick.AddListener(delegate
				{
					if (m_WinConditionController != null)
					{
						m_WinConditionController.InspectorPanel.BindWinCondition(Guid.Empty, null);
					}
					else
					{
						Debug.LogError("Win controller ref missing from Add conditon button");
					}
				});
			}
			if (!(m_PointerEvents != null))
			{
				return;
			}
			m_PointerEvents.OnSubmitCallback.AddListener(delegate
			{
				if (m_WinConditionController != null)
				{
					m_WinConditionController.InspectorPanel.BindWinCondition(Guid.Empty, null);
				}
				else
				{
					Debug.LogError("Win controller ref missing from Add conditon button");
				}
			});
		}

		private void OnDestroy()
		{
			if (m_Button != null)
			{
				m_Button.onClick.RemoveAllListeners();
			}
			if (m_PointerEvents != null)
			{
				m_PointerEvents.OnSubmitCallback.RemoveAllListeners();
			}
		}
	}
}
