using Landfall.TABS_Input;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace LevelCreator
{
	public abstract class DMUIPanel : MonoBehaviour
	{
		public DMUIManager.UIPanels m_panelType;

		[HideIf("m_autoSelectFirstSelectable")]
		public Selectable m_defaultObject;

		[SerializeField]
		private bool m_autoSelectFirstSelectable;

		[HideInInspector]
		public GameObject m_lastSelectedObject;

		public InputState m_inputState;

		[SerializeField]
		[BoxGroup("Animation")]
		private UIPanelAnimation m_openAnimation;

		[SerializeField]
		[BoxGroup("Animation")]
		private UIPanelAnimation m_closeAnimation;

		public bool HasFocus => InputManager.PeekState() == m_inputState;

		public virtual void OnOpen()
		{
			if (m_defaultObject != null)
			{
				m_defaultObject.Select();
			}
			else if (m_autoSelectFirstSelectable)
			{
				Utility.DelayAction(this, delegate
				{
					Selectable componentInChildren = GetComponentInChildren<Selectable>();
					if (componentInChildren != null)
					{
						componentInChildren.Select();
					}
				});
			}
			Show();
		}

		public virtual void OnClose()
		{
			Hide();
		}

		public void Show(bool overrideDuration = false, float duration = 0f)
		{
			Move(m_openAnimation, overrideDuration, duration);
		}

		public void Hide(bool overrideDuration = false, float duration = 0f)
		{
			Move(m_closeAnimation, overrideDuration, duration);
		}

		private void Move(UIPanelAnimation animation, bool overrideDuration = false, float duration = 0f)
		{
			float time = (overrideDuration ? duration : animation.m_duration);
			Vector2 to = animation.CalculateTarget();
			LeanTween.move(base.gameObject, to, time).setEase(animation.m_easeType);
		}

		protected virtual void Awake()
		{
			CreateInputState();
		}

		protected virtual void CreateInputState()
		{
			if (m_inputState == null)
			{
				m_inputState = new InputState(m_panelType.ToString());
				m_inputState.AddOnKeyDownListener(PlayerActions.Instance.m_menu, delegate
				{
					DMUIManager.Instance.PopPanel();
				});
			}
		}
	}
}
