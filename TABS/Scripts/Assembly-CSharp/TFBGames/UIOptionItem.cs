using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TFBGames
{
	public class UIOptionItem : UISettingsItem
	{
		private const float HORIZONTAL_SENSITIVITY = 0.5f;

		[SerializeField]
		private Button m_leftArrow;

		[SerializeField]
		private Button m_rightArrow;

		private UIScaleJiggle m_leftJiggle;

		private UIScaleJiggle m_rightJiggle;

		private bool m_listenInput;

		public override void HandleInput(PlayerActions actions)
		{
			if (actions.m_uiNavigationHorizontal.WasReleased)
			{
				m_listenInput = true;
			}
			if (m_listenInput && Mathf.Abs(actions.m_uiNavigationHorizontal.Value) > 0.5f)
			{
				if (actions.m_uiNavigationHorizontal.Value < 0f)
				{
					m_leftArrow.onClick.Invoke();
					m_leftJiggle.AddClickForce();
				}
				else
				{
					m_rightArrow.onClick.Invoke();
					m_rightJiggle.AddClickForce();
				}
				m_listenInput = false;
			}
		}

		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			m_listenInput = true;
		}

		public override void ChangeControlVisibility(bool enable)
		{
			base.ChangeControlVisibility(enable);
			if (m_leftArrow != null)
			{
				m_leftArrow.gameObject.SetActive(enable);
			}
			if (m_rightArrow != null)
			{
				m_rightArrow.gameObject.SetActive(enable);
			}
		}

		private void Start()
		{
			m_leftJiggle = m_leftArrow.GetComponent<UIScaleJiggle>();
			m_rightJiggle = m_rightArrow.GetComponent<UIScaleJiggle>();
		}
	}
}
