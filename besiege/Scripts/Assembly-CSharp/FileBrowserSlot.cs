using System;
using System.IO;
using UnityEngine;

[AddComponentMenu("Besiege/Workshop/FileBrowserSlot")]
public class FileBrowserSlot : HoverableClickBehaviour
{
	private const int MaxFilenameLength = 10;

	private const float DoubleClickTime = 0.4f;

	public Action<FileBrowserSlot> DoubleClicked;

	public Action<FileBrowserSlot> Clicked;

	public Action<FileBrowserSlot> DeleteClicked;

	public Action<FileBrowserSlot> DeleteConfirmed;

	public Action<FileBrowserSlot> ToggleRemoteClicked;

	public Action<FileBrowserSlot> UploadClicked;

	public Action<FileBrowserSlot> DownloadClicked;

	public Action CursorEntered;

	public Action CursorExited;

	public Action<FileBrowserSlot> LoadAsSelectionClicked;

	public Action<FileBrowserSlot> VersionsClicked;

	[SerializeField]
	protected GameObject fileBackground;

	[SerializeField]
	protected GameObject folderBackground;

	[SerializeField]
	protected FileBrowserSlotThumbnail thumbnailComponent;

	protected IVirtualObject virtualObject;

	[SerializeField]
	protected TextMesh fileNameTextMesh;

	[SerializeField]
	protected TruncateTextMesh fileNameTruncator;

	[SerializeField]
	protected TextMesh suffixTextMesh;

	[SerializeField]
	protected AlignTextMesh suffix;

	[SerializeField]
	private SimpleUIButton loadAsSelectionButton;

	[SerializeField]
	protected SimpleUIButton deleteButton;

	[SerializeField]
	protected SimpleUIButton confirmDeleteButton;

	[SerializeField]
	protected GameObject confirmDeletionObject;

	[SerializeField]
	protected SimpleUIButton steamUploadButton;

	[SerializeField]
	protected SimpleUIButton wegameUploadButton;

	[SerializeField]
	protected SimpleUIButton modIOUploadButton;

	[SerializeField]
	private SimpleUIButton cloudButton;

	[SerializeField]
	private MeshRenderer cloudRenderer;

	[SerializeField]
	public Texture2D noCloudTex;

	[SerializeField]
	public Texture2D cloudTex;

	[SerializeField]
	private SimpleUIButton versionsButton;

	[SerializeField]
	protected GameObject notInstalledOverlay;

	[SerializeField]
	protected GameObject confirmDownloadObject;

	[SerializeField]
	protected SimpleUIButton confirmDownloadButton;

	private float lastClickTime;

	private FileBrowserView view;

	private Vector3 thumbStartScale;

	private WorkshopType workshopType;

	protected bool isActive;

	public IVirtualObject VirtualObject
	{
		get
		{
			return virtualObject;
		}
	}

	public FileBrowserSlotThumbnail Thumbnail
	{
		get
		{
			return thumbnailComponent;
		}
	}

	public virtual void Initialize(FileBrowserView view, IVirtualObject virtualObject, WorkshopType workshopType)
	{
		this.virtualObject = virtualObject;
		this.workshopType = workshopType;
		this.view = view;
		Setup();
	}

	public void Select()
	{
	}

	public void Deselect()
	{
		confirmDeletionObject.SetActive(false);
		if (confirmDownloadButton != null)
		{
			confirmDownloadObject.SetActive(false);
		}
	}

	public void SetActive()
	{
		if (!isActive)
		{
			isActive = true;
		}
	}

	protected void SetSlotName(string fileName)
	{
		WorkshopManager.VerifyString(fileName, delegate(WorkshopManager.VerifyStringResult result, string str)
		{
			if (fileNameTextMesh != null)
			{
				fileNameTextMesh.text = str;
				fileNameTruncator.Truncate();
				suffix.Align();
			}
		});
	}

	protected void SetFileSuffix(string suffix)
	{
		suffixTextMesh.text = suffix;
	}

	protected virtual void SetThumbnailPath(IVirtualObject virtualObject)
	{
		if (thumbStartScale == Vector3.zero)
		{
			thumbStartScale = thumbnailComponent.transform.localScale;
		}
		thumbnailComponent.transform.localScale = thumbStartScale * ((!virtualObject.IsFolder) ? 1f : 0.95f);
		thumbnailComponent.Initialize(virtualObject);
	}

	protected virtual void SetIsFolder(bool isFolder)
	{
		suffixTextMesh.gameObject.SetActive(!isFolder);
		fileBackground.SetActive(!isFolder);
		folderBackground.SetActive(isFolder);
	}

	private void ToggleUploadButton()
	{
		DisableUploadButtons();
		if (virtualObject.IsUploadable && ReferenceMaster.IsPlatformReady() && !FileBrowserView.saveMenuUpload)
		{
			ToggleCloudSyncButton();
			if (workshopType == WorkshopType.Steam)
			{
				ToggleSteamUploadButton();
			}
			else if (workshopType == WorkshopType.WeGame)
			{
				ToggleWegameUploadButton();
			}
			else if (workshopType == WorkshopType.ModIO)
			{
				ToggleModIOUploadButton();
			}
		}
	}

	private void DisableUploadButtons()
	{
		if (steamUploadButton != null)
		{
			steamUploadButton.gameObject.SetActive(false);
		}
		if (wegameUploadButton != null)
		{
			wegameUploadButton.gameObject.SetActive(false);
		}
		if (modIOUploadButton != null)
		{
			modIOUploadButton.gameObject.SetActive(false);
		}
		if (cloudButton != null)
		{
			cloudButton.gameObject.SetActive(false);
		}
	}

	private void ToggleModIOUploadButton()
	{
		if (!(modIOUploadButton == null))
		{
			modIOUploadButton.Click += UploadButtonClick;
			modIOUploadButton.gameObject.SetActive(true);
		}
	}

	private void ToggleWegameUploadButton()
	{
		if (!(wegameUploadButton == null))
		{
			wegameUploadButton.Click += UploadButtonClick;
			wegameUploadButton.gameObject.SetActive(true);
		}
	}

	private void ToggleSteamUploadButton()
	{
		if (!(steamUploadButton == null))
		{
			steamUploadButton.Click += UploadButtonClick;
			steamUploadButton.gameObject.SetActive(true);
		}
	}

	private void UploadButtonClick()
	{
		if (UploadClicked != null)
		{
			UploadClicked(this);
		}
	}

	private void ToggleCloudSyncButton()
	{
		if (!(cloudButton == null))
		{
			WorkshopManager instance = SingleInstance<WorkshopManager>.Instance;
			if (OptionsMaster.BesiegeConfig.CloudSaving && instance != null && !virtualObject.IsFolder)
			{
				string remotePath = instance.GetRemotePath(virtualObject.ObjectPath.Path);
				cloudRenderer.material.mainTexture = ((!instance.IsRemoteFile(remotePath)) ? noCloudTex : cloudTex);
				cloudButton.gameObject.SetActive(true);
				cloudButton.Click += ToggleRemoteClick;
			}
			else
			{
				cloudButton.gameObject.SetActive(false);
			}
		}
	}

	private void ToggleRemoteClick()
	{
		if (ToggleRemoteClicked != null)
		{
			ToggleRemoteClicked(this);
		}
		ToggleCloudSyncButton();
	}

	private void Setup()
	{
		SetupFileSuffix();
		SetSlotName(virtualObject.Name);
		SetupLoadAsSelectionButton();
		SetThumbnailPath(virtualObject);
		SetIsFolder(virtualObject.IsFolder);
		ToggleUploadButton();
		ToggleNotInstalledOverlay();
		SetupDeleteButton();
		SetupDownloadButton();
		SetupVersionsButton();
	}

	private void SetupLoadAsSelectionButton()
	{
		if (!(loadAsSelectionButton == null))
		{
			loadAsSelectionButton.gameObject.SetActive(!view.IsSaveMenu && view.Controller.ShowAdditiveOrSelectionOnlyButton(view.IsSaveMenu) && !virtualObject.IsFolder);
			loadAsSelectionButton.Click += OnLoadAsSelectionButtonClick;
		}
	}

	private void SetupDownloadButton()
	{
		if (!(confirmDownloadButton == null) && !IsPublishedWorkshopItem() && !IsInstalled())
		{
			confirmDownloadButton.Click += OnConfirmDownloadButtonClick;
		}
	}

	private void SetupDeleteButton()
	{
		if (FileBrowserView.saveMenuUpload)
		{
			deleteButton.gameObject.SetActive(false);
		}
		if (virtualObject.IsDeletable)
		{
			deleteButton.Click += OnDeleteButtonClick;
			confirmDeleteButton.Click += OnConfirmDeleteButtonClick;
		}
		else
		{
			deleteButton.gameObject.SetActive(false);
		}
	}

	private void SetupVersionsButton()
	{
		if (!(versionsButton == null))
		{
			versionsButton.gameObject.SetActive(view.Controller is MachineFileBrowserController && !FileBrowserView.saveMenuUpload && !virtualObject.IsFolder && !virtualObject.ObjectPath.Path.Contains("AutoSave") && Directory.Exists(Path.Combine(StaticSettings.MachineAutosavePath, virtualObject.Name)));
			versionsButton.Click += OnVersionsButtonClick;
		}
	}

	private bool IsPublishedWorkshopItem()
	{
		return virtualObject is IWorkshopItem && ((IWorkshopItem)virtualObject).IsPublishedItem;
	}

	private bool IsInstalled()
	{
		bool result = true;
		if (virtualObject is IWorkshopItem)
		{
			IWorkshopItem workshopItem = virtualObject as IWorkshopItem;
			result = workshopItem.IsInstalled;
		}
		return result;
	}

	private void ToggleNotInstalledOverlay()
	{
		if (!(notInstalledOverlay == null))
		{
			bool active = !IsPublishedWorkshopItem() && !IsInstalled();
			notInstalledOverlay.SetActive(active);
		}
	}

	private void SetupFileSuffix()
	{
		if (virtualObject.HasSuffix)
		{
			string suffixFromPath = GetSuffixFromPath(virtualObject.ObjectPath.ToString());
			SetFileSuffix(suffixFromPath);
		}
		else
		{
			SetFileSuffix(string.Empty);
		}
	}

	private void OnConfirmDeleteButtonClick()
	{
		confirmDeletionObject.SetActive(false);
		if (DeleteConfirmed != null)
		{
			DeleteConfirmed(this);
		}
	}

	protected virtual void OnDeleteButtonClick()
	{
		confirmDeletionObject.SetActive(true);
		if (DeleteClicked != null)
		{
			DeleteClicked(this);
		}
	}

	private void OnConfirmDownloadButtonClick()
	{
		confirmDownloadObject.SetActive(false);
		if (DownloadClicked != null)
		{
			DownloadClicked(this);
		}
	}

	private void OnLoadAsSelectionButtonClick()
	{
		if (LoadAsSelectionClicked != null)
		{
			LoadAsSelectionClicked(this);
		}
	}

	private void OnVersionsButtonClick()
	{
		if (VersionsClicked != null)
		{
			VersionsClicked(this);
		}
	}

	private string GetSuffixFromPath(string filePath)
	{
		string extension = Path.GetExtension(filePath);
		if (string.IsNullOrEmpty(extension))
		{
			return string.Empty;
		}
		return Path.GetExtension(filePath).Substring(1);
	}

	public override void OnClicked()
	{
		InvokeClicked();
		if (Time.realtimeSinceStartup < lastClickTime + 0.4f)
		{
			InvokeDoubleClicked();
		}
		lastClickTime = Time.realtimeSinceStartup;
	}

	protected override void OnCursorEnter()
	{
		if (CursorEntered != null)
		{
			CursorEntered();
		}
	}

	protected override void OnCursorExit()
	{
		if (CursorExited != null)
		{
			CursorExited();
		}
	}

	private void InvokeDoubleClicked()
	{
		if (!IsPublishedWorkshopItem() && !IsInstalled())
		{
			confirmDownloadObject.SetActive(true);
		}
		else if (DoubleClicked != null)
		{
			DoubleClicked(this);
		}
	}

	private void InvokeClicked()
	{
		if (Clicked != null)
		{
			Clicked(this);
		}
	}
}
