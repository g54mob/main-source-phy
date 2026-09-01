using System;
using System.IO;
using System.Linq;
using Steamworks;
using UnityEngine;

public class PublishedSteamFileController : FileBrowserController
{
	public PublishedSteamFileController(FileBrowserView browserView)
		: base(browserView)
	{
	}

	protected override void LoadFile(IVirtualObject virtualObject, OpenMode mode)
	{
		IWorkshopItem workshopItem = (IWorkshopItem)virtualObject;
		PublishedFileId_t publishedFileId = (PublishedFileId_t)workshopItem.WorkshopItemId;
		UploadItemToWorkshop(publishedFileId, cachedUploadData);
		view.Close();
	}

	private void UploadItemToWorkshop(PublishedFileId_t publishedFileId, UploadData uploadData)
	{
		SteamWorkshopManager steamWorkshopManager = SingleInstance<WorkshopManager>.Instance as SteamWorkshopManager;
		switch (ReferenceMaster.UIActive)
		{
		case ReferenceMaster.WorkshopItemType.Machine:
			steamWorkshopManager.CreateOrUpdateItem(publishedFileId, uploadData.Name, uploadData.Path, uploadData.ThumbnailPath, false, uploadData.Tags, WorkshopManager.ItemTypes.Machines, uploadData.Visibility, uploadData.DlcDependencyMask);
			break;
		case ReferenceMaster.WorkshopItemType.Skins:
			steamWorkshopManager.CreateOrUpdateItem(publishedFileId, uploadData.Name, uploadData.Path, uploadData.ThumbnailPath, true, uploadData.Tags, WorkshopManager.ItemTypes.Skins, uploadData.Visibility, uploadData.DlcDependencyMask);
			break;
		case ReferenceMaster.WorkshopItemType.Levels:
			steamWorkshopManager.CreateOrUpdateItem(publishedFileId, uploadData.Name, uploadData.Path, uploadData.ThumbnailPath, uploadData.IsFolder, uploadData.Tags, WorkshopManager.ItemTypes.Levels, uploadData.Visibility, uploadData.DlcDependencyMask);
			break;
		case ReferenceMaster.WorkshopItemType.Mods:
			steamWorkshopManager.CreateOrUpdateItem(publishedFileId, uploadData.Name, uploadData.Path, uploadData.ThumbnailPath, true, uploadData.Tags, WorkshopManager.ItemTypes.Mods, uploadData.Visibility, uploadData.DlcDependencyMask);
			break;
		}
	}

	protected override void SaveFile(IVirtualObject virtualObject, OpenMode mode)
	{
		view.CacheWorkshopItem(virtualObject as IWorkshopItem);
		UploadObject(virtualObject);
	}

	protected override void UploadFile(IVirtualObject virtualObject)
	{
		IWorkshopItem workshopItem = (IWorkshopItem)virtualObject;
		PublishedSteamFile publishedSteamFile = (PublishedSteamFile)workshopItem;
		string path = virtualObject.ObjectPath.Path;
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
		string[] source = publishedSteamFile.Tags.Split(',');
		WorkshopManager.ItemTypes uploadType = WorkshopManager.ItemTypes.Machines;
		if (source.Contains("Levels"))
		{
			uploadType = WorkshopManager.ItemTypes.Levels;
		}
		view.OpenUploadUpdateDialog(uploadType, fileNameWithoutExtension, path, virtualObject.Thumbnail as Texture2D, source.ToList());
	}

	protected override void UploadFolder(IVirtualObject virtualObject)
	{
		throw new NotImplementedException();
	}
}
