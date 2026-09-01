using InControl;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class NextUnitButton : SimpleButton, IPointerClickHandler, IEventSystemHandler
	{
		public bool m_increament = true;

		public UILayoutGroup m_layoutGroup;

		public Image m_arrow;

		private Color normalColor;

		private Color disabledColor;

		private bool shouldBeDisabled;

		private void Awake()
		{
			m_layoutGroup.onUpdatedListPos += OnUpdatedList;
			if (SceneSettings.UseSceneColorOverwrite)
			{
				normalColor = SceneSettings.GetBackgroundColor();
				disabledColor = SceneSettings.GetBackgroundColor();
			}
			else
			{
				normalColor = UIStyleManager.GetStyle().m_TextColor;
				disabledColor = UIStyleManager.GetStyle().m_DisabledColor;
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			for (int i = 0; i < m_layoutGroup.elementsShown; i++)
			{
				m_layoutGroup.MoveList(m_increament);
			}
		}

		private void OnUpdatedList()
		{
			bool flag = !m_layoutGroup.IsExpandedFaction();
			shouldBeDisabled = !m_layoutGroup.CanMoveListMore(m_increament);
			if (PlayerActions.Instance.InputType == InputType.Controller)
			{
				flag = true;
			}
			if (flag)
			{
				m_arrow.enabled = false;
				m_highlight.enabled = false;
				DisableButton();
				return;
			}
			m_arrow.enabled = true;
			m_highlight.enabled = true;
			if (shouldBeDisabled)
			{
				DisableButton();
			}
			else
			{
				EnableButton();
			}
		}

		private void LateUpdate()
		{
			Color b = normalColor;
			if (shouldBeDisabled)
			{
				b = disabledColor;
			}
			m_arrow.color = Color.Lerp(m_arrow.color, b, 20f * Time.unscaledDeltaTime);
		}

		private void OnEnable()
		{
			PlayerActions.Instance.OnLastInputTypeChanged += OnInputTypeChanged;
		}

		private void OnDisable()
		{
			PlayerActions.Instance.OnLastInputTypeChanged -= OnInputTypeChanged;
		}

		private void OnInputTypeChanged(BindingSourceType obj)
		{
			OnUpdatedList();
		}
	}
}
