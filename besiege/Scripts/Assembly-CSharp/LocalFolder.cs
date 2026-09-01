using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LocalFolder<T> : LocalFolder where T : LocalFile
{
	protected override Type VirtualFileType
	{
		get
		{
			return typeof(T);
		}
	}

	public LocalFolder(FileSystemPath path, FileSystemPath thumbnailPath)
		: base(path, thumbnailPath)
	{
	}

	public LocalFolder(DirectoryInfo directoryInfo)
		: base(directoryInfo)
	{
	}
}
public class LocalFolder : VirtualFolder
{
	protected override Type VirtualFileType
	{
		get
		{
			return typeof(LocalFile);
		}
	}

	public LocalFolder(FileSystemPath path, FileSystemPath thumbnailPath)
		: base(path, thumbnailPath)
	{
	}

	public LocalFolder(DirectoryInfo directoryInfo)
	{
		ObjectPath = FileSystemPath.Parse(directoryInfo.FullName);
		if (!ObjectPath.IsDirectory)
		{
			ObjectPath = FileSystemPath.Parse(ObjectPath.Path + FileSystemPath.DirectorySeparator);
		}
		ResolveThumbnail();
	}

	public static LocalFolder FromDirectoryInfo(DirectoryInfo directoryInfo, Type folderType)
	{
		return (LocalFolder)Activator.CreateInstance(folderType, directoryInfo);
	}

	public override void Open()
	{
		Clear();
		IEnumerable<IVirtualObject> objects = GetObjects(ObjectPath);
		AddRange(objects);
		base.Open();
	}

	protected IEnumerable<IVirtualObject> GetObjects(FileSystemPath folderPath)
	{
		DirectoryInfo directoryInfo = new DirectoryInfo(folderPath.Path);
		if (!directoryInfo.Exists)
		{
			return null;
		}
		List<IVirtualObject> list = new List<IVirtualObject>();
		IEnumerable<IVirtualObject> folders = GetFolders(directoryInfo);
		IEnumerable<IVirtualObject> files = GetFiles(directoryInfo);
		list.AddRange(folders);
		list.AddRange(files);
		return list;
	}

	public override void Delete()
	{
		try
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(ObjectPath.Path);
			DeleteFolder(directoryInfo);
		}
		catch (Exception ex)
		{
			Debug.LogWarning("[LocalFolder]: Failed to Delete(): " + ex.Message + ", " + ex.StackTrace);
		}
		base.Delete();
	}

	private void DeleteFolder(DirectoryInfo directoryInfo)
	{
		if (directoryInfo.Exists)
		{
			SetAttributesNormal(directoryInfo);
			directoryInfo.Delete(true);
		}
		else
		{
			Debug.LogWarning("[LocalFolder]: trying to delete folder that doesn't exist.");
		}
	}

	public void SetAttributesNormal(DirectoryInfo dir)
	{
		DirectoryInfo[] directories = dir.GetDirectories();
		foreach (DirectoryInfo attributesNormal in directories)
		{
			SetAttributesNormal(attributesNormal);
		}
		FileInfo[] files = dir.GetFiles();
		foreach (FileInfo fileInfo in files)
		{
			fileInfo.Attributes = FileAttributes.Normal;
		}
	}

	private FileSystemPath GetThumbnailPath(FileSystemPath objectPath)
	{
		string directoryName = "Thumbnails";
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(objectPath.EntityName);
		return objectPath.ParentPath.AppendDirectory(directoryName).AppendFile(fileNameWithoutExtension + ".png");
	}

	private IVirtualObject GetFolderFromDirectoryInfo(DirectoryInfo dirInfo)
	{
		return FromDirectoryInfo(dirInfo, GetType());
	}

	private IVirtualObject GetFileFromFileInfo(FileInfo fileInfo)
	{
		return LocalFile.FromFileInfo(fileInfo, VirtualFileType);
	}

	private IEnumerable<IVirtualObject> GetFolders(DirectoryInfo dirInfo)
	{
		return from x in dirInfo.GetDirectories()
			where !IgnoreFolder(x)
			select GetFolderFromDirectoryInfo(x);
	}

	private IEnumerable<IVirtualObject> GetFiles(DirectoryInfo dirInfo)
	{
		return from x in dirInfo.GetFiles()
			select GetFileFromFileInfo(x);
	}

	private void ResolveThumbnail()
	{
		DirectoryInfo dirInfo = new DirectoryInfo(ObjectPath.Path);
		IEnumerable<IVirtualObject> files = GetFiles(dirInfo);
		ThumbnailPath = ResolveThumbnailPath(files);
		if (!ThumbnailPath.IsRoot)
		{
			return;
		}
		dirInfo = new DirectoryInfo(ObjectPath.Path + "/Thumbnails");
		if (dirInfo.Exists)
		{
			files = GetFiles(dirInfo);
			files = files.OrderByDescending((IVirtualObject x) => File.GetCreationTimeUtc(x.ObjectPath.Path));
			ThumbnailPath = ResolveThumbnailPath(files);
		}
		if (ThumbnailPath.IsRoot)
		{
			ThumbnailPath = ObjectPath;
		}
	}

	private bool IgnoreFolder(DirectoryInfo dirInfo)
	{
		if (dirInfo.Name == "Thumbnails")
		{
			return true;
		}
		return false;
	}
}
