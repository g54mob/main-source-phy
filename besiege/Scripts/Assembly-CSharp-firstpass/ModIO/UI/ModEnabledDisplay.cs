using UnityEngine;

namespace ModIO.UI
{
	[RequireComponent(typeof(StateToggleDisplay))]
	public class ModEnabledDisplay : MonoBehaviour, IModDisabledReceiver, IModEnabledReceiver, IModViewElement
	{
		private ModView m_view;

		private int m_modId;

		virtual GameObject IModViewElement.gameObject
		{
			get
			{
				return base.gameObject;
			}
		}

		public void SetModView(ModView view)
		{
			if (!(m_view == view))
			{
				if (m_view != null)
				{
					m_view.onProfileChanged.RemoveListener(DisplayModEnabled);
				}
				m_view = view;
				if (m_view != null)
				{
					m_view.onProfileChanged.AddListener(DisplayModEnabled);
					DisplayModEnabled(m_view.profile);
				}
				else
				{
					DisplayModEnabled(null);
				}
			}
		}

		public void DisplayModEnabled(ModProfile profile)
		{
			int modId = 0;
			if (profile != null)
			{
				modId = profile.id;
			}
			DisplayModEnabled(modId);
		}

		public void DisplayModEnabled(int modId)
		{
			bool isEnabled = LocalUser.EnabledModIds.Contains(modId);
			DisplayModEnabled(modId, isEnabled);
		}

		public void DisplayModEnabled(int modId, bool isEnabled)
		{
			m_modId = modId;
			StateToggleDisplay[] components = base.gameObject.GetComponents<StateToggleDisplay>();
			foreach (StateToggleDisplay stateToggleDisplay in components)
			{
				stateToggleDisplay.isOn = isEnabled;
			}
		}

		public void OnModEnabled(int modId)
		{
			if (modId == m_modId)
			{
				DisplayModEnabled(modId, true);
			}
		}

		public void OnModDisabled(int modId)
		{
			if (modId == m_modId)
			{
				DisplayModEnabled(modId, false);
			}
		}
	}
}
