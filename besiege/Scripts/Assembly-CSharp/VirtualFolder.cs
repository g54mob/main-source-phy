using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class VirtualFolder<T> : VirtualFolder
{
	protected override Type VirtualFileType
	{
		get
		{
			return typeof(T);
		}
	}

	public VirtualFolder(FileSystemPath path, FileSystemPath thumbnailPath)
	{
		ObjectPath = path;
		ThumbnailPath = thumbnailPath;
	}
}
public class VirtualFolder : IVirtualObject
{
	public Action FolderChanged;

	public Action FolderOpened;

	private Dictionary<string, IVirtualObject> folderObjects = new Dictionary<string, IVirtualObject>();

	public static VirtualFolder Empty
	{
		get
		{
			return new VirtualFolder(FileSystemPath.Root, FileSystemPath.Root);
		}
	}

	public virtual bool IsUploadable { get; set; }

	public virtual bool IsDeletable { get; set; }

	public bool IsFolder
	{
		get
		{
			return true;
		}
	}

	public bool HasSuffix
	{
		get
		{
			return false;
		}
	}

	public string Name
	{
		get
		{
			return ObjectPath.EntityName;
		}
	}

	public double Date { get; set; }

	public FileSystemPath ObjectPath { get; set; }

	public FileSystemPath ThumbnailPath { get; set; }

	public VirtualFolder Parent { get; set; }

	public Texture Thumbnail { get; set; }

	public Action<IVirtualObject> ObjectDeleted { get; set; }

	protected virtual Type VirtualFileType
	{
		get
		{
			return typeof(VirtualFile);
		}
	}

	public VirtualFolder()
	{
		IsDeletable = true;
	}

	public VirtualFolder(FileSystemPath path, FileSystemPath thumbnailPath)
	{
		if (!(path == FileSystemPath.Root) || !(thumbnailPath == FileSystemPath.Root))
		{
			ObjectPath = path;
			ThumbnailPath = thumbnailPath;
			if (ObjectPath.IsRoot)
			{
				Date = StaticSettings.GetTimestamp(DateTime.Now);
			}
			else if (IOHelper.FolderExists(ObjectPath.Path))
			{
				Date = StaticSettings.GetTimestamp(File.GetLastWriteTimeUtc(ObjectPath.Path));
			}
			else if (IOHelper.FileExists(ThumbnailPath.Path))
			{
				Date = StaticSettings.GetTimestamp(File.GetLastWriteTimeUtc(ThumbnailPath.Path));
			}
			else
			{
				Date = StaticSettings.GetTimestamp(DateTime.Now);
			}
		}
	}

	public FileSystemPath ResolveThumbnailPath()
	{
		FileSystemPath result = FileSystemPath.Root;
		try
		{
			IVirtualObject virtualObject = GetObjects().First((IVirtualObject x) => IsObjectImage(x));
			result = virtualObject.ObjectPath;
		}
		catch (InvalidOperationException)
		{
		}
		return result;
	}

	public FileSystemPath ResolveThumbnailPath(IEnumerable<IVirtualObject> objects)
	{
		FileSystemPath result = FileSystemPath.Root;
		IVirtualObject virtualObject = objects.FirstOrDefault((IVirtualObject x) => IsObjectImage(x));
		if (virtualObject != null)
		{
			result = virtualObject.ObjectPath;
		}
		return result;
	}

	public IVirtualObject Create(FileSystemPath path)
	{
		throw new NotImplementedException();
	}

	public IEnumerable<IVirtualObject> GetObjects()
	{
		return folderObjects.Values;
	}

	public void AddObject(IVirtualObject virtualObject)
	{
		if (!folderObjects.ContainsKey(virtualObject.ObjectPath.Path))
		{
			folderObjects.Add(virtualObject.ObjectPath.Path, virtualObject);
		}
		else
		{
			folderObjects[virtualObject.ObjectPath.Path] = virtualObject;
		}
		virtualObject.Parent = this;
		virtualObject.ObjectDeleted = OnObjectDeleted;
		WorkshopFile workshopFile = virtualObject as WorkshopFile;
		if (workshopFile != null && !workshopFile.IsInstalled && virtualObject.ObjectPath.IsFile)
		{
			workshopFile.IsInstalled = true;
			workshopFile.DlcDependencyMask = 0u;
			workshopFile.AreDlcRequirementsMet = true;
			workshopFile.IsUploadable = false;
		}
	}

	public void AddRange(IEnumerable<IVirtualObject> virtualObjects)
	{
		if (virtualObjects == null)
		{
			return;
		}
		foreach (IVirtualObject virtualObject in virtualObjects)
		{
			AddObject(virtualObject);
		}
	}

	public void Clear()
	{
		Dictionary<string, IVirtualObject> dictionary = new Dictionary<string, IVirtualObject>();
		foreach (KeyValuePair<string, IVirtualObject> folderObject in folderObjects)
		{
			if (folderObject.Value.Parent == this && folderObject.Value.ObjectPath.ParentPath == ObjectPath)
			{
				dictionary.Add(folderObject.Key, folderObject.Value);
				continue;
			}
			folderObject.Value.Parent = null;
			folderObject.Value.ObjectDeleted = null;
		}
		folderObjects = dictionary;
	}

	protected bool IsObjectImage(IVirtualObject virtualObject)
	{
		string extension = virtualObject.ObjectPath.GetExtension();
		return IsObjectImage(extension);
	}

	protected bool IsObjectImage(string extension)
	{
		string[] source = new string[2] { ".png", ".jpg" };
		return source.Contains(extension);
	}

	private void OnObjectDeleted(IVirtualObject virtualObject)
	{
		folderObjects.Remove(virtualObject.ObjectPath.Path);
		if (FolderChanged != null)
		{
			FolderChanged();
		}
	}

	public void OnDelete()
	{
	}

	public void OnOpen()
	{
	}

	public virtual void Delete()
	{
		OnDelete();
		if (ObjectDeleted != null)
		{
			ObjectDeleted(this);
		}
	}

	public virtual void Open()
	{
		OnOpen();
		if (FolderOpened != null)
		{
			FolderOpened();
		}
	}
}
