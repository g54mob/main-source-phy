using UnityEngine;

namespace ModIO.UI
{
	[RequireComponent(typeof(ModfileView))]
	public class CurrentBuildDisplay : MonoBehaviour, IModViewElement
	{
		private ModView m_view;

		GameObject IModViewElement.gameObject => base.gameObject;

		public void SetModView(ModView view)
		{
			if (!(m_view == view))
			{
				if (m_view != null)
				{
					m_view.onProfileChanged.RemoveListener(DisplayCurrentBuild);
				}
				m_view = view;
				if (m_view != null)
				{
					m_view.onProfileChanged.AddListener(DisplayCurrentBuild);
					DisplayCurrentBuild(m_view.profile);
				}
				else
				{
					DisplayCurrentBuild(null);
				}
			}
		}

		public void DisplayCurrentBuild(ModProfile modProfile)
		{
			Modfile modfile = null;
			if (modProfile != null)
			{
				modfile = modProfile.currentBuild;
			}
			base.gameObject.GetComponent<ModfileView>().modfile = modfile;
		}
	}
}
