using ModIO;
using ModIO.UI;
using UnityEngine;

public class DMVisibilityDisplay : MonoBehaviour, IModViewElement
{
	[SerializeField]
	private GameObject m_hiddenIcon;

	private ModView m_view;

	private ModProfile m_profile;

	GameObject IModViewElement.gameObject => base.gameObject;

	protected virtual void OnEnable()
	{
		DisplayProfile(m_profile);
	}

	public void SetModView(ModView view)
	{
		if (!(m_view == view))
		{
			if (m_view != null)
			{
				m_view.onProfileChanged.RemoveListener(DisplayProfile);
			}
			m_view = view;
			if (m_view != null)
			{
				m_view.onProfileChanged.AddListener(DisplayProfile);
				DisplayProfile(m_view.profile);
			}
			else
			{
				DisplayProfile(null);
			}
		}
	}

	public void DisplayProfile(ModProfile profile)
	{
		m_profile = profile;
		if (m_profile != null && m_hiddenIcon != null)
		{
			m_hiddenIcon.SetActive(profile.visibility == ModVisibility.Hidden);
		}
	}
}
