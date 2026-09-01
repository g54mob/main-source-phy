using UnityEngine;

namespace ModIO.UI
{
	[RequireComponent(typeof(UserView))]
	public class ModSubmittorDisplay : MonoBehaviour, IModViewElement
	{
		private ModView m_view;

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
					m_view.onProfileChanged.RemoveListener(DisplayModSubmittor);
				}
				m_view = view;
				if (m_view != null)
				{
					m_view.onProfileChanged.AddListener(DisplayModSubmittor);
					DisplayModSubmittor(m_view.profile);
				}
				else
				{
					DisplayModSubmittor(null);
				}
			}
		}

		public void DisplayModSubmittor(ModProfile modProfile)
		{
			UserProfile profile = null;
			if (modProfile != null)
			{
				profile = modProfile.submittedBy;
			}
			base.gameObject.GetComponent<UserView>().profile = profile;
		}
	}
}
