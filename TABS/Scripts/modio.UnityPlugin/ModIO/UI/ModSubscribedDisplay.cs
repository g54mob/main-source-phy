using System.Collections.Generic;
using UnityEngine;

namespace ModIO.UI
{
	[RequireComponent(typeof(StateToggleDisplay))]
	public class ModSubscribedDisplay : MonoBehaviour, IModViewElement, IModSubscriptionsUpdateReceiver
	{
		private ModView m_view;

		private int m_modId;

		GameObject IModViewElement.gameObject => base.gameObject;

		public void SetModView(ModView view)
		{
			if (!(m_view == view))
			{
				if (m_view != null)
				{
					m_view.onProfileChanged.RemoveListener(DisplayModSubscribed);
				}
				m_view = view;
				if (m_view != null)
				{
					m_view.onProfileChanged.AddListener(DisplayModSubscribed);
					DisplayModSubscribed(m_view.profile);
				}
				else
				{
					DisplayModSubscribed(null);
				}
			}
		}

		private void OnEnable()
		{
			if (m_view == null)
			{
				SetModView(GetComponentInParent<ModView>());
			}
			DisplayModSubscribed(m_view.profile);
		}

		public void Refresh()
		{
			DisplayModSubscribed(m_modId);
		}

		public void DisplayModSubscribed(ModProfile profile)
		{
			int modId = 0;
			if (profile != null)
			{
				modId = profile.id;
			}
			DisplayModSubscribed(modId);
		}

		public void DisplayModSubscribed(int modId)
		{
			bool isSubscribed = LocalUser.EnabledModIds.Contains(modId);
			DisplayModSubscribed(modId, isSubscribed);
		}

		public void DisplayModSubscribed(int modId, bool isSubscribed)
		{
			m_modId = modId;
			StateToggleDisplay[] components = base.gameObject.GetComponents<StateToggleDisplay>();
			for (int i = 0; i < components.Length; i++)
			{
				components[i].isOn = isSubscribed;
			}
		}

		public void OnModSubscriptionsUpdated(IList<int> addedSubscriptions, IList<int> removedSubscriptions)
		{
			if (addedSubscriptions.Contains(m_modId))
			{
				DisplayModSubscribed(m_modId, isSubscribed: true);
			}
			else if (removedSubscriptions.Contains(m_modId))
			{
				DisplayModSubscribed(m_modId, isSubscribed: false);
			}
		}
	}
}
