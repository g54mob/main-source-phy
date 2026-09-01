using System.Collections.Generic;
using UnityEngine;

namespace ModIO.UI
{
	[RequireComponent(typeof(StateToggleDisplay))]
	public class ModSubscribedDisplay : MonoBehaviour, IModSubscriptionsUpdateReceiver, IModViewElement
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
			bool isSubscribed = LocalUser.SubscribedModIds.Contains(modId);
			DisplayModSubscribed(modId, isSubscribed);
		}

		public void DisplayModSubscribed(int modId, bool isSubscribed)
		{
			m_modId = modId;
			StateToggleDisplay[] components = base.gameObject.GetComponents<StateToggleDisplay>();
			foreach (StateToggleDisplay stateToggleDisplay in components)
			{
				stateToggleDisplay.isOn = isSubscribed;
			}
		}

		public void OnModSubscriptionsUpdated(IList<int> addedSubscriptions, IList<int> removedSubscriptions)
		{
			if (addedSubscriptions.Contains(m_modId))
			{
				DisplayModSubscribed(m_modId, true);
			}
			else if (removedSubscriptions.Contains(m_modId))
			{
				DisplayModSubscribed(m_modId, false);
			}
		}
	}
}
