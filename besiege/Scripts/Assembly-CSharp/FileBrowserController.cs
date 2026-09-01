using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEngine;

public abstract class FileBrowserController : IDisposable
{
	public enum OpenMode
	{
		Normal = 0,
		AdditiveOrSelectionOnly = 1
	}

	protected FileBrowserView view;

	protected AbstractObjectCollection collection;

	protected FileBrowserType currentBrowserType;

	protected UploadData cachedUploadData;

	public AbstractObjectCollection Collection
	{
		get
		{
			return collection;
		}
	}

	protected FileBrowserController(FileBrowserView browserView)
	{
		view = browserView;
	}

	public void Initialize(AbstractObjectCollection objectCollection, FileBrowserType fileBrowserType, bool open = true)
	{
		collection = objectCollection;
		collection.CollectionChanged = OnCollectionChanged;
		currentBrowserType = fileBrowserType;
		VirtualFolder root = collection.GetRoot();
		if (open)
		{
			OpenFolder(root);
		}
	}

	private void OnCollectionChanged()
	{
		RegenerateView();
	}

	public void DownloadObject(IVirtualObject virtualObject)
	{
		IWorkshopItem workshopItem = virtualObject as IWorkshopItem;
		if (workshopItem != null)
		{
			DownloadWorkshopItem(workshopItem.WorkshopItemId);
			view.Close();
		}
	}

	public void UploadObject(IVirtualObject virtualObject, UploadData uploadData = null)
	{
		if (virtualObject is VirtualFolder)
		{
			UploadFolder(virtualObject);
		}
		else
		{
			UploadFile(virtualObject);
		}
	}

	public void OpenFolder(VirtualFolder folder)
	{
		collection.ChangeFolder(folder);
	}

	public void CreateFolder(string folderName)
	{
		CreateFolderResult result = collection.CreateFolder(folderName);
		view.HandlerFolderCreationResult(result);
	}

	public void DeleteObject(IVirtualObject virtualObject)
	{
		collection.DeleteObject(virtualObject);
		if (ReferenceMaster.IsPlatformReady())
		{
			ToggleRemote(virtualObject, false);
		}
	}

	public void ToggleRemote(IVirtualObject virtualObject)
	{
		if (!ReferenceMaster.IsPlatformReady())
		{
			UnityEngine.Debug.LogWarning("[FileBrowserController] ToggleRemote Remote platform is not ready, returning.");
			return;
		}
		WorkshopManager instance = SingleInstance<WorkshopManager>.Instance;
		if (instance != null)
		{
			string remotePath = instance.GetRemotePath(virtualObject.ObjectPath.Path);
			ToggleRemote(virtualObject, !instance.IsRemoteFile(remotePath));
		}
	}

	public void ToggleRemote(IVirtualObject virtualObject, bool toggle)
	{
		if (virtualObject.IsFolder)
		{
			return;
		}
		WorkshopManager instance = SingleInstance<WorkshopManager>.Instance;
		if (instance != null)
		{
			string path = virtualObject.ObjectPath.Path;
			string path2 = virtualObject.ThumbnailPath.Path;
			if (toggle)
			{
				instance.WriteRemoteFileAsync(new FileInfo(path), File.Exists(path2));
			}
			else if (instance.RemoveRemoteFile(path))
			{
				instance.RemoveRemoteFile(virtualObject.ThumbnailPath.Path);
			}
		}
	}

	public IVirtualObject FindVirtualObject(string fileName)
	{
		string sanatizedName = StaticSettings.SanatizeFileName(fileName);
		return collection.CurrentFolder.GetObjects().FirstOrDefault((IVirtualObject x) => x.Name == sanatizedName && !x.IsFolder);
	}

	public void FindAndOpenObject(string fileName, bool overwrite, OpenMode mode)
	{
		IVirtualObject virtualObject = FindVirtualObject(fileName);
		string fileName2 = StaticSettings.SanatizeFileName(fileName);
		if (virtualObject == null)
		{
			if (view.IsSaveMenu)
			{
				CreateFile(fileName2, mode);
			}
		}
		else
		{
			OpenObject(virtualObject, overwrite, mode);
		}
	}

	public void OpenObject(IVirtualObject virtualObject, bool overwrite, OpenMode mode)
	{
		if (view == null || virtualObject == null)
		{
			UnityEngine.Debug.LogError(string.Concat("Error while opening virtual object (view=", view, " virtualObject=", virtualObject, ")!"));
			return;
		}
		view.SelectObject(virtualObject);
		IWorkshopItem workshopItem = virtualObject as IWorkshopItem;
		if (workshopItem != null && !workshopItem.AreDlcRequirementsMet && !(workshopItem is WorkshopSkinFile))
		{
			view.OpenDlcsMissingPopup(workshopItem.DlcDependencyMask, 4441);
		}
		else if (virtualObject is VirtualFolder)
		{
			OpenFolder((VirtualFolder)virtualObject);
		}
		else if (virtualObject is VirtualFile)
		{
			if (view.IsSaveMenu)
			{
				FileInfo fileInfo = new FileInfo(virtualObject.ObjectPath.Path);
				if (!overwrite && fileInfo.Exists)
				{
					OnFileSaved(mode, FileSaveResult.FileAlreadyExists);
				}
				else
				{
					SaveFile(virtualObject, mode);
				}
			}
			else
			{
				LoadFile(virtualObject, mode);
			}
		}
		else
		{
			virtualObject.Open();
		}
	}

	public void OpenFolderInExplorer()
	{
		string fileName = collection.CurrentFolder.ObjectPath.Path.TrimEnd('\\', '/');
		Process.Start(fileName);
	}

	public void OpenWorkshop(WorkshopType workshopType)
	{
		FileBrowserType browserType;
		switch (workshopType)
		{
		case WorkshopType.Steam:
			browserType = GetSteamBrowserType(view.IsSaveMenu);
			break;
		case WorkshopType.WeGame:
			browserType = GetWeGameBrowserType(view.IsSaveMenu);
			break;
		case WorkshopType.ModIO:
			browserType = GetModIOBrowserType(view.IsSaveMenu);
			break;
		default:
			browserType = FileBrowserType.LocalLevels;
			break;
		}
		view.Open(browserType, view.IsSaveMenu);
	}

	private FileBrowserType GetModIOBrowserType(bool isSave = false)
	{
		if (isSave)
		{
			switch (currentBrowserType)
			{
			case FileBrowserType.LocalLevels:
				return FileBrowserType.PublishedModIOLevels;
			case FileBrowserType.LocalMachines:
				return FileBrowserType.PublishedModIOMachines;
			case FileBrowserType.Skins:
				return FileBrowserType.Skins;
			default:
				return FileBrowserType.WeGameLevels;
			}
		}
		switch (currentBrowserType)
		{
		case FileBrowserType.LocalLevels:
			return FileBrowserType.ModIOLevels;
		case FileBrowserType.LocalMachines:
			return FileBrowserType.ModIOMachines;
		case FileBrowserType.Skins:
			return FileBrowserType.ModIOSkins;
		default:
			return FileBrowserType.ModIOMachines;
		}
	}

	private FileBrowserType GetWeGameBrowserType(bool isSave = false)
	{
		if (isSave)
		{
			switch (currentBrowserType)
			{
			case FileBrowserType.LocalLevels:
				return FileBrowserType.PublishedWeGameLevels;
			case FileBrowserType.LocalMachines:
				return FileBrowserType.PublishedWeGameMachines;
			case FileBrowserType.Skins:
				return FileBrowserType.Skins;
			default:
				return FileBrowserType.WeGameLevels;
			}
		}
		switch (currentBrowserType)
		{
		case FileBrowserType.LocalLevels:
			return FileBrowserType.WeGameLevels;
		case FileBrowserType.LocalMachines:
			return FileBrowserType.WeGameMachines;
		case FileBrowserType.Skins:
			return FileBrowserType.Skins;
		default:
			return FileBrowserType.WeGameLevels;
		}
	}

	private FileBrowserType GetSteamBrowserType(bool isSave = false)
	{
		if (isSave)
		{
			switch (currentBrowserType)
			{
			case FileBrowserType.LocalLevels:
				return FileBrowserType.PublishedSteamLevels;
			case FileBrowserType.LocalMachines:
				return FileBrowserType.PublishedSteamMachines;
			case FileBrowserType.Skins:
				return FileBrowserType.Skins;
			default:
				return FileBrowserType.SteamLevels;
			}
		}
		switch (currentBrowserType)
		{
		case FileBrowserType.LocalLevels:
			return FileBrowserType.SteamLevels;
		case FileBrowserType.LocalMachines:
			return FileBrowserType.SteamMachines;
		case FileBrowserType.Skins:
			return FileBrowserType.Skins;
		default:
			return FileBrowserType.SteamLevels;
		}
	}

	public void OpenParentFolder()
	{
		collection.OpenParentFolder();
	}

	private void RegenerateView()
	{
		view.Generate(collection.CurrentFolder);
	}

	public void RefreshView()
	{
		collection.Refresh();
	}

	public virtual bool ShowAdditiveOrSelectionOnlyButton(bool isSaveMenu)
	{
		return false;
	}

	protected abstract void LoadFile(IVirtualObject virtualObject, OpenMode mode);

	protected abstract void SaveFile(IVirtualObject virtualObject, OpenMode mode);

	protected abstract void UploadFile(IVirtualObject virtualObject);

	protected abstract void UploadFolder(IVirtualObject virtualObject);

	protected virtual void DownloadWorkshopItem(ulong workshopFileId)
	{
		if (SingleInstance<WorkshopManager>.hasInstance())
		{
			WorkshopManager instance = SingleInstance<WorkshopManager>.Instance;
			instance.Download(workshopFileId);
		}
	}

	protected void CreateThumbnail(FileInfo fileInfo, bool useMainCamera, bool isMachineSelection = false)
	{
		string thumbnailPath = StaticSettings.GetThumbnailPath(fileInfo);
		if (!string.IsNullOrEmpty(thumbnailPath) && !InputManager.LeftHotShiftKey())
		{
			view.CreateThumbnail(thumbnailPath, useMainCamera, isMachineSelection);
		}
	}

	protected virtual void CreateFile(string fileName, OpenMode mode)
	{
		VirtualFile virtualObject;
		if (collection.CreateFile(fileName, out virtualObject) == CreateFileResult.Success)
		{
			SaveFile(virtualObject, mode);
			view.SelectObject(virtualObject);
		}
	}

	protected void OnFileLoaded(FileLoadResult result)
	{
		switch (result)
		{
		case FileLoadResult.Success:
			view.SetRestoreFolder();
			view.Close();
			break;
		case FileLoadResult.SuccessAdditive:
			view.Close();
			break;
		}
	}

	protected void OnFileSaved(OpenMode mode, FileSaveResult result)
	{
		switch (result)
		{
		case FileSaveResult.FileAlreadyExists:
			view.ToggleOverwriteButton(true, mode == OpenMode.AdditiveOrSelectionOnly);
			break;
		case FileSaveResult.Success:
			view.SetRestoreFolder(true);
			view.Close();
			break;
		}
	}

	public void Dispose()
	{
		collection.CollectionChanged = null;
		collection.Dispose();
		collection = null;
	}

	public void SetCachedUploadData(UploadData uploadData)
	{
		cachedUploadData = uploadData;
	}

	protected UploadData GenerateUploadData(IVirtualObject virtualObject)
	{
		WorkshopManager.ItemTypes itemType = WorkshopManager.ItemTypes.Machines;
		switch (ReferenceMaster.UIActive)
		{
		case ReferenceMaster.WorkshopItemType.Skins:
			itemType = WorkshopManager.ItemTypes.Skins;
			break;
		case ReferenceMaster.WorkshopItemType.Levels:
			itemType = WorkshopManager.ItemTypes.Levels;
			break;
		case ReferenceMaster.WorkshopItemType.Mods:
			itemType = WorkshopManager.ItemTypes.Mods;
			break;
		}
		string name = virtualObject.Name;
		UploadData uploadData = new UploadData();
		uploadData.Name = name;
		uploadData.Path = virtualObject.ObjectPath.ToString();
		uploadData.IsFolder = virtualObject.IsFolder;
		uploadData.ThumbnailPath = virtualObject.ThumbnailPath.ToString();
		uploadData.ItemType = itemType;
		return uploadData;
	}
}
