using System;
using System.IO;
using UnityEngine;

public class VirtualFile : IVirtualObject
{
	public virtual bool IsUploadable { get; set; }

	public virtual bool IsDeletable { get; set; }

	public bool IsFolder
	{
		get
		{
			return false;
		}
	}

	public virtual bool HasSuffix
	{
		get
		{
			return true;
		}
	}

	public virtual string Name
	{
		get
		{
			return GetNameWithoutExtension(ObjectPath.EntityName);
		}
	}

	public virtual double Date { get; set; }

	public FileSystemPath ObjectPath { get; set; }

	public FileSystemPath ThumbnailPath { get; set; }

	public VirtualFolder Parent { get; set; }

	public Texture Thumbnail { get; set; }

	public Action<IVirtualObject> ObjectDeleted { get; set; }

	public Action<byte[]> FileOpened { get; set; }

	public VirtualFile(FileSystemPath path, FileSystemPath thumbnailPath)
	{
		ObjectPath = path;
		ThumbnailPath = thumbnailPath;
		if (ObjectPath.IsRoot || ObjectPath.IsChildOf(FileSystemPath.Root))
		{
			Date = StaticSettings.GetTimestamp(DateTime.Now);
		}
		else if (IOHelper.FileOrFolderExists(ObjectPath.Path))
		{
			Date = StaticSettings.GetTimestamp(File.GetLastWriteTimeUtc(ObjectPath.Path));
		}
		else if (IOHelper.FileExists(ThumbnailPath.Path))
		{
			Date = StaticSettings.GetTimestamp(File.GetLastWriteTimeUtc(thumbnailPath.Path));
		}
		else
		{
			Date = StaticSettings.GetTimestamp(DateTime.Now);
			if (!(this is WorkshopFile))
			{
				Debug.LogError("Failed to get timestamp for " + path.EntityName + " / " + path.Path + string.Empty);
			}
		}
		IsUploadable = true;
		IsDeletable = true;
	}

	public virtual void Delete()
	{
		InvokeObjectDeleted();
	}

	public virtual void Open()
	{
		byte[] array = GetBytes();
		if (array == null)
		{
			array = new byte[0];
		}
		if (FileOpened != null)
		{
			FileOpened(array);
		}
	}

	public virtual IVirtualObject Create(FileSystemPath path)
	{
		return null;
	}

	protected void InvokeObjectDeleted()
	{
		if (ObjectDeleted != null)
		{
			ObjectDeleted(this);
		}
	}

	protected virtual byte[] GetBytes()
	{
		return null;
	}

	private string GetNameWithoutExtension(string objectName)
	{
		if (string.IsNullOrEmpty(objectName))
		{
			return string.Empty;
		}
		string extension = objectName.GetExtension();
		if (string.IsNullOrEmpty(extension))
		{
			return objectName;
		}
		int length = objectName.LastIndexOf(extension);
		return objectName.Substring(0, length);
	}
}
