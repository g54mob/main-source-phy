using System;
using System.Collections.Generic;
using UnityEngine;

public class UploadDialog : MonoBehaviour
{
	public enum UploadDialogMode
	{
		NewUpload = 0,
		UpdateExisting = 1,
		NewUploadOrModify = 2
	}

	public Action<UploadDialogMode, UploadData> UploadClicked;

	public Action<UploadDialogMode, UploadData> ModifyClicked;

	[SerializeField]
	private SimpleUIButton uploadButton;

	[SerializeField]
	private SimpleUIButton modifyButton;

	[SerializeField]
	private SimpleUIButton closeButton;

	[SerializeField]
	private TagCheckboxManager tagCheckboxManager;

	[SerializeField]
	private OpenPhoto openPhoto;

	[SerializeField]
	private SaveInformationPanel saveInformationPanel;

	[SerializeField]
	private GameObject[] button1States;

	[SerializeField]
	private GameObject[] button2States;

	protected UploadDialogMode uploadMode;

	protected UploadData uploadData;

	public virtual void Initialize(UploadDialogMode uploadMode, UploadData uploadData, Texture thumbnailTexture = null)
	{
		this.uploadMode = uploadMode;
		this.uploadData = uploadData;
		ToggleButtons();
		ToggleTags();
		InitializeInfoPanel(thumbnailTexture);
	}

	private void ToggleTags()
	{
		if (uploadData.Tags != null)
		{
			tagCheckboxManager.SetTags(uploadData.Tags);
		}
	}

	private void InitializeInfoPanel(Texture thumbnailTexture)
	{
		if (saveInformationPanel != null)
		{
			saveInformationPanel.Initialize(uploadData.Name, uploadData.ThumbnailPath, thumbnailTexture, uploadMode);
		}
	}

	private void ToggleButtons()
	{
		if (uploadMode == UploadDialogMode.NewUpload)
		{
			SetButtonState(0);
			ToggleModifyButton(false);
		}
		else if (uploadMode == UploadDialogMode.UpdateExisting)
		{
			SetButtonState(1);
			ToggleModifyButton(true);
		}
		else if (uploadMode == UploadDialogMode.NewUploadOrModify)
		{
			SetButtonState(0);
			ToggleModifyButton(true);
		}
	}

	private void ToggleModifyButton(bool toggleOn)
	{
		modifyButton.gameObject.SetActive(toggleOn);
	}

	private void SetButtonState(int state)
	{
		for (int i = 0; i < button1States.Length; i++)
		{
			button1States[i].SetActive(i == state);
		}
		for (int j = 0; j < button2States.Length; j++)
		{
			button2States[j].SetActive(j == state);
		}
	}

	protected virtual void UpdateThumbnailPath(string thumbPath)
	{
		uploadData.ThumbnailPath = thumbPath;
		uploadData.UploadThumbnail = true;
	}

	public void SetTags(List<int> tags)
	{
		foreach (int tag in tags)
		{
			tagCheckboxManager.SetTag(tag, true);
		}
	}

	private void Awake()
	{
		if (saveInformationPanel != null)
		{
			SaveInformationPanel obj = saveInformationPanel;
			obj.OnThumbnailChanged = (Action<string>)Delegate.Combine(obj.OnThumbnailChanged, new Action<string>(UpdateThumbnailPath));
		}
		closeButton.Click += CloseButtonClick;
		uploadButton.Click += UploadButtonClick;
		modifyButton.Click += ModifyButtonClick;
	}

	private void ModifyButtonClick()
	{
		if (ModifyClicked != null)
		{
			UploadData arg = GetUploadData();
			ModifyClicked(uploadMode, arg);
		}
		Close();
	}

	private void UploadButtonClick()
	{
		if (UploadClicked != null)
		{
			UploadData arg = GetUploadData();
			UploadDialogMode uploadDialogMode = uploadMode;
			if (uploadDialogMode == UploadDialogMode.NewUploadOrModify)
			{
				uploadDialogMode = UploadDialogMode.NewUpload;
			}
			UploadClicked(uploadDialogMode, arg);
		}
		Close();
	}

	private void CloseButtonClick()
	{
		FileBrowserView.saveMenuUpload = false;
		Close();
	}

	private void Close()
	{
		Camera main = Camera.main;
		Blur component = main.GetComponent<Blur>();
		if (component != null)
		{
			component.enabled = false;
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private UploadData GetUploadData()
	{
		uploadData.Tags = tagCheckboxManager.GetTagSelected();
		uploadData.UploadThumbnail |= uploadMode == UploadDialogMode.NewUpload;
		return uploadData;
	}

	private void OnEnable()
	{
		StatMaster.SetInMenu(true);
	}

	private void OnDisable()
	{
		StatMaster.SetInMenu(false);
	}
}
