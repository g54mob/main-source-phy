using ModIO;
using ModIO.UI;
using TMPro;
using UnityEngine;

public class DMModMetaDataDisplay : MonoBehaviour, IModViewElement
{
	private ModProfile m_modProfile;

	private ModView m_view;

	private TMP_Text m_textComponent;

	[SerializeField]
	private GameObject m_EnableOnMatch;

	GameObject IModViewElement.gameObject => base.gameObject;

	private void Awake()
	{
		m_textComponent = GetComponent<TMP_Text>();
	}

	protected virtual void OnEnable()
	{
		DisplayModMetaData(m_modProfile);
	}

	public void SetModView(ModView view)
	{
		if (!(m_view == view))
		{
			if (m_view != null)
			{
				m_view.onProfileChanged.RemoveListener(DisplayModMetaData);
			}
			m_view = view;
			if (m_view != null)
			{
				m_view.onProfileChanged.AddListener(DisplayModMetaData);
				DisplayModMetaData(m_view.profile);
			}
			else
			{
				DisplayModMetaData(null);
			}
		}
	}

	public void DisplayModMetaData(ModProfile modProfile)
	{
		m_modProfile = modProfile;
		string text = "";
		m_textComponent.text = text;
	}
}
