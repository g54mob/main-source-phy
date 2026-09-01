using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_SandboxTitleAndDescription : MonoBehaviour
{
	public Button m_EditTitleButton;

	public Button m_EditDescriptionButton;

	public TextMeshProUGUI m_TitleText;

	public TextMeshProUGUI m_DescriptionText;

	public void Start()
	{
		m_EditTitleButton.onClick.AddListener(OnEditTitle);
		m_EditDescriptionButton.onClick.AddListener(OnEditDescription);
	}

	private void OnEnable()
	{
		RefreshProperties();
	}

	public void RefreshProperties()
	{
		m_TitleText.text = SandboxSettings.m_Title;
		m_DescriptionText.text = SandboxSettings.m_Description;
	}

	public void SetTitle(string title)
	{
		m_TitleText.text = title;
	}

	public void SetDescription(string description)
	{
		m_DescriptionText.text = description;
	}

	public void OnEditTitle()
	{
		PopupInputField.Display(Localize.Get("UI_TITLE"), SandboxSettings.m_Title, Workshop.TITLE_CHAR_LIMIT, isFilename: false, isDirectory: false, OnSaveTitle);
	}

	public void OnEditDescription()
	{
		PopupInputField.DisplayLarge(Localize.Get("UI_DESCRIPTION"), SandboxSettings.m_Description, Workshop.DESCRIPTION_CHAR_LIMIT, isFilename: false, isDirectory: false, OnSaveDescription);
	}

	private void OnSaveTitle(string newTitle)
	{
		if (newTitle != null)
		{
			SandboxSettings.m_Title = newTitle.Trim();
			GameUI.m_Instance.m_SandboxTitleAndDescription.RefreshProperties();
		}
	}

	private void OnSaveDescription(string newDescription)
	{
		if (newDescription != null)
		{
			SandboxSettings.m_Description = newDescription.Trim();
			GameUI.m_Instance.m_SandboxTitleAndDescription.RefreshProperties();
		}
	}
}
