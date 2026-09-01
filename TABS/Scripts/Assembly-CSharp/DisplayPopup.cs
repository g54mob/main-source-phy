using Landfall.TABS;
using Landfall.TABS_Input;
using UnityEngine;

public class DisplayPopup : MonoBehaviour
{
	[Multiline]
	[SerializeField]
	private string m_text;

	[SerializeField]
	private float m_fontSizeMax = 44f;

	[SerializeField]
	private string m_key;

	[SerializeField]
	private bool m_showOnStart;

	[SerializeField]
	protected UINavigationGroupManager navigationGroupManager;

	public void ShowPopup()
	{
		if (navigationGroupManager != null)
		{
			navigationGroupManager.SetAutoSelectInAllGroups(autoselect: false);
		}
		else
		{
			navigationGroupManager = Object.FindObjectOfType<UINavigationGroupManager>();
			navigationGroupManager.SetAutoSelectInAllGroups(autoselect: false);
		}
		if (!string.IsNullOrWhiteSpace(m_key))
		{
			if (PlayerPrefs.HasKey(m_key))
			{
				return;
			}
			PlayerPrefs.SetInt(m_key, 1);
		}
		ServiceLocator.GetService<ModalPanel>().PopUp(m_text, Close, m_fontSizeMax, true);
	}

	private void Update()
	{
		if (PlayerActions.Instance.m_back.WasPressed)
		{
			Close();
		}
	}

	private void Close()
	{
		if (navigationGroupManager != null)
		{
			navigationGroupManager.SetAutoSelectInAllGroups(autoselect: true);
		}
	}

	private void Start()
	{
		if (m_showOnStart)
		{
			ShowPopup();
		}
	}
}
