using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Localisation;

public abstract class SteamFileCollection : WorkshopFileCollection
{
	public SteamFileCollection()
	{
		ObjectName = LocalisationManager.GetTranslation(940);
	}

	public override CreateFileResult CreateFile(string fileName, out VirtualFile virtualObject)
	{
		throw new NotImplementedException();
	}

	public override CreateFolderResult CreateFolder(string folderName)
	{
		throw new NotImplementedException();
	}

	public override void DeleteObject(IVirtualObject virtualObject)
	{
		base.DeleteObject(virtualObject);
		if (virtualObject is IWorkshopItem)
		{
			ulong workshopItemId = ((IWorkshopItem)virtualObject).WorkshopItemId;
			SteamWorkshopManager steamWorkshopManager = SingleInstance<WorkshopManager>.Instance as SteamWorkshopManager;
			steamWorkshopManager.Unsubscribe(workshopItemId);
		}
	}

	private void ProcessPublishedItems(List<WorkshopManager.WorkshopItem> items)
	{
		foreach (SteamWorkshopManager.SteamItem item in items)
		{
			VirtualFile virtualObject = CreatePublishedSteamFileFromItem(item);
			base.CurrentFolder.AddObject(virtualObject);
		}
		InvokeCollectionChanged();
	}

	private VirtualFile CreatePublishedSteamFileFromItem(WorkshopManager.WorkshopItem item)
	{
		FileSystemPath path = FileSystemPath.Root.AppendFile(item.Title);
		PublishedSteamFile publishedSteamFile = new PublishedSteamFile(path, FileSystemPath.Root);
		publishedSteamFile.WorkshopItemId = item.WorkshopId;
		publishedSteamFile.IsPublishedItem = true;
		publishedSteamFile.IsOwner = item.IsOwner;
		publishedSteamFile.Author = item.Author;
		publishedSteamFile.DlcDependencyMask = item.DlcDependencyMask;
		publishedSteamFile.AreDlcRequirementsMet = item.AreDlcRequirementsMet;
		publishedSteamFile.Date = StaticSettings.GetTimestamp(item.SubscribeTime);
		publishedSteamFile.Tags = item.Tags;
		PublishedSteamFile publishedSteamFile2 = publishedSteamFile;
		SteamWorkshopManager.SteamItem steamItem = (SteamWorkshopManager.SteamItem)item;
		publishedSteamFile2.PreviewImageHandle = steamItem.preview;
		return publishedSteamFile2;
	}

	protected override VirtualFolder GetSubscribedItems(WorkshopManager.ItemTypes contentType)
	{
		return GetSubscribedSteamItems(contentType);
	}

	private void ProcessSubscribedItems(List<WorkshopManager.WorkshopItem> items)
	{
		foreach (SteamWorkshopManager.SteamItem item in items)
		{
			string text = item.Title;
			if (string.IsNullOrEmpty(text))
			{
				text = LocalisationManager.GetTranslation(3374);
			}
			item.Title = text;
			if (!item.IsInstalled)
			{
				continue;
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(item.RootFolder);
			string searchPattern = string.Empty;
			if (!string.IsNullOrEmpty(FilterExtension))
			{
				searchPattern = "*" + FilterExtension;
			}
			FileInfo fileInfo = directoryInfo.GetFiles(searchPattern).FirstOrDefault();
			if (fileInfo != null)
			{
				AddLocalWorkshopFile(base.CurrentFolder, fileInfo, (ulong)item.publishedFileId, item.SubscribeTime, item.IsOwner, item.Author, item.DlcDependencyMask, item.AreDlcRequirementsMet);
				continue;
			}
			DirectoryInfo[] directories = directoryInfo.GetDirectories();
			DirectoryInfo[] array = directories;
			foreach (DirectoryInfo directoryInfo2 in array)
			{
				WorkshopFolder workshopFolder = new WorkshopFolder(directoryInfo2);
				workshopFolder.WorkshopItemId = (ulong)item.publishedFileId;
				workshopFolder.IsInstalled = true;
				workshopFolder.DlcDependencyMask = item.DlcDependencyMask;
				workshopFolder.AreDlcRequirementsMet = item.AreDlcRequirementsMet;
				WorkshopFolder virtualObject = workshopFolder;
				base.CurrentFolder.AddObject(virtualObject);
			}
		}
		InvokeCollectionChanged();
	}

	protected override VirtualFolder GetPublishedItems(WorkshopManager.ItemTypes contentType)
	{
		base.CurrentFolder = new VirtualFolder<WorkshopFile>(FileSystemPath.Root, FileSystemPath.Root);
		if (!SteamManager.Initialized)
		{
			return base.CurrentFolder;
		}
		SteamWorkshopManager steamWorkshopManager = SingleInstance<WorkshopManager>.Instance as SteamWorkshopManager;
		steamWorkshopManager.GetPublishedWorkshopItemsAsync(contentType, ProcessPublishedItems);
		return base.CurrentFolder;
	}

	protected VirtualFolder GetSubscribedSteamItems(WorkshopManager.ItemTypes contentType)
	{
		base.CurrentFolder = new VirtualFolder<WorkshopFile>(FileSystemPath.Root, FileSystemPath.Root);
		if (!SteamManager.Initialized)
		{
			return base.CurrentFolder;
		}
		SteamWorkshopManager steamWorkshopManager = SingleInstance<WorkshopManager>.Instance as SteamWorkshopManager;
		steamWorkshopManager.GetSubscribedWorkshopItemsAsync(contentType, WorkshopManager.InstallType.Installed, ProcessSubscribedItems);
		return base.CurrentFolder;
	}

	private void AddLocalWorkshopFile(VirtualFolder folder, FileInfo fileInfo, ulong workshopItemId, uint subscribeTime, bool isOwner, ulong author, uint dlcDependencyMask, bool areDlcRequirementsMet)
	{
		WorkshopFile workshopFile = LocalFile.FromFileInfo<WorkshopFile>(fileInfo);
		workshopFile.WorkshopItemId = workshopItemId;
		workshopFile.IsInstalled = true;
		workshopFile.IsPublishedItem = false;
		workshopFile.IsOwner = isOwner;
		workshopFile.Author = author;
		workshopFile.DlcDependencyMask = dlcDependencyMask;
		workshopFile.AreDlcRequirementsMet = areDlcRequirementsMet;
		workshopFile.Date = StaticSettings.GetTimestamp(subscribeTime);
		workshopFile.ThumbnailPath = GetThumbnailFromWorkshopItem(workshopFile.ObjectPath);
		workshopFile.IsUploadable = false;
		folder.AddObject(workshopFile);
	}

	private FileSystemPath GetThumbnailFromWorkshopItem(FileSystemPath objectPath)
	{
		return objectPath.ChangeExtension(".png");
	}

	public override void Dispose()
	{
	}
}
