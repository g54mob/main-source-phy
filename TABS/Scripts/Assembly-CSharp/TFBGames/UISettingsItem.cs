using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TFBGames
{
	public class UISettingsItem : MonoBehaviour, IUISelectionItem, ISelectHandler, IEventSystemHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
	{
		[SerializeField]
		private SettingsHandler m_settingsHandler;

		[SerializeField]
		private Selectable m_selectable;

		private ISettingsItemHandler m_handler;

		public Selectable Selectable => m_selectable;

		protected virtual void Awake()
		{
			if (m_settingsHandler != null)
			{
				m_handler = m_settingsHandler;
			}
		}

		public virtual void OnSelect(BaseEventData eventData)
		{
			if (m_handler != null)
			{
				m_handler.SelectedItem(this);
				ToggleToolTip(openState: true);
			}
		}

		public virtual void OnDeselect(BaseEventData eventData)
		{
			if (m_handler != null)
			{
				m_handler.DeselectedItem(this);
				ToggleToolTip(openState: false);
			}
		}

		public virtual void HandleInput(PlayerActions actions)
		{
		}

		public virtual void ChangeControlVisibility(bool enable)
		{
		}

		public void SetSettingsItemHandler(ISettingsItemHandler settingsItemHandler)
		{
			m_handler = settingsItemHandler;
		}

		private void ToggleToolTip(bool openState)
		{
			MenuSettingsButton componentInParent = GetComponentInParent<MenuSettingsButton>();
			if (componentInParent != null && m_settingsHandler != null)
			{
				SettingsInstance settingsInstance = componentInParent.settingsInstance;
				if (componentInParent.settingsInstance.DisableChanges && !string.IsNullOrEmpty(componentInParent.settingsInstance.DisabledMessageKey))
				{
					m_settingsHandler.ToggleDisabledToolTip(settingsInstance, openState);
				}
				if (!string.IsNullOrEmpty(settingsInstance?.toolTip))
				{
					m_settingsHandler.ToggleToolTip(settingsInstance, openState);
				}
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			ToggleToolTip(openState: true);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			ToggleToolTip(openState: false);
		}
	}
}
