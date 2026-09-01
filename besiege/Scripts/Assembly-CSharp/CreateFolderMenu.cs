using System;
using Localisation;
using Selectors;
using UnityEngine;

public class CreateFolderMenu : MonoBehaviour
{
	public Action<string> CreateFolderConfirmed;

	[SerializeField]
	private SimpleUIButton createFolderButton;

	[SerializeField]
	private TextHolder folderTextfield;

	[SerializeField]
	private DynamicText errorDynamicText;

	public void HandleCreateFolderResult(CreateFolderResult result)
	{
		switch (result)
		{
		case CreateFolderResult.Success:
			base.gameObject.SetActive(false);
			break;
		case CreateFolderResult.FolderExists:
			SetCreateErrorText(LocalisationManager.GetTranslation(3275));
			break;
		case CreateFolderResult.CreateFailed:
			SetCreateErrorText(LocalisationManager.GetTranslation(3276));
			break;
		}
	}

	private void Awake()
	{
		createFolderButton.Click += CreateFolderButtonClick;
	}

	private void CreateFolderButtonClick()
	{
		if (CreateFolderConfirmed != null)
		{
			CreateFolderConfirmed(folderTextfield.ValueText);
			folderTextfield.ValueText = string.Empty;
		}
	}

	private void SetCreateErrorText(string errorText)
	{
		errorDynamicText.SetText(errorText);
	}
}
