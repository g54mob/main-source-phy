using System;
using System.Collections.Generic;
using System.Linq;

public abstract class AbstractObjectCollection : IDisposable
{
	protected string RootPath;

	private readonly string[] IgnoreExtensions = new string[1] { ".meta" };

	public virtual bool HideFileField { get; private set; }

	public virtual string ObjectName { get; protected set; }

	public virtual string FilterExtension { get; private set; }

	public Action CollectionChanged { get; set; }

	public VirtualFolder CurrentFolder { get; protected set; }

	protected virtual bool IgnoreObjectFilter
	{
		get
		{
			return false;
		}
	}

	protected virtual bool IgnoreObjectOrder
	{
		get
		{
			return false;
		}
	}

	public AbstractObjectCollection()
	{
		RootPath = StaticSettings.DataPath;
		CurrentFolder = VirtualFolder.Empty;
		ObjectName = "Object";
	}

	public virtual void ChangeFolder(VirtualFolder folder)
	{
		if (CurrentFolder != null)
		{
			CurrentFolder.FolderChanged = null;
			CurrentFolder.FolderOpened = null;
			CurrentFolder = null;
		}
		CurrentFolder = folder;
		CurrentFolder.FolderChanged = OnFolderChanged;
		CurrentFolder.FolderOpened = OnFolderOpened;
		CurrentFolder.Open();
	}

	public virtual void OpenParentFolder()
	{
		if (CurrentFolder != null && CurrentFolder.Parent != null)
		{
			ChangeFolder(CurrentFolder.Parent);
		}
	}

	public virtual CreateFolderResult CreateFolder(string folderName)
	{
		return CreateFolderResult.CreateFailed;
	}

	public virtual CreateFileResult CreateFile(string fileName, out VirtualFile virtualObject)
	{
		virtualObject = null;
		return CreateFileResult.CreateFailed;
	}

	public IEnumerable<IVirtualObject> GetFilteredObjectsFrom(IEnumerable<IVirtualObject> collection)
	{
		IEnumerable<IVirtualObject> enumerable = collection;
		if (!IgnoreObjectFilter)
		{
			enumerable = FilterObjects(enumerable);
		}
		if (!IgnoreObjectOrder)
		{
			enumerable = OrderObjects(enumerable);
		}
		return enumerable;
	}

	public virtual IEnumerable<IVirtualObject> FilterObjects(IEnumerable<IVirtualObject> objects)
	{
		return objects.Where((IVirtualObject x) => !FilterFile(x, FilterExtension));
	}

	public virtual IEnumerable<IVirtualObject> OrderObjects(IEnumerable<IVirtualObject> objects)
	{
		return from x in objects
			orderby x.Name
			orderby !x.IsFolder
			select x;
	}

	public virtual void DeleteObject(IVirtualObject virtualObject)
	{
		virtualObject.Delete();
	}

	public abstract VirtualFolder GetRoot();

	public abstract void Dispose();

	public virtual void Refresh()
	{
	}

	protected bool FilterFile(IVirtualObject virtualObject, string extensionFilter)
	{
		if (virtualObject.IsFolder || !virtualObject.ObjectPath.IsFile)
		{
			return false;
		}
		IWorkshopItem workshopItem = virtualObject as IWorkshopItem;
		if (workshopItem != null && (!workshopItem.IsInstalled || workshopItem.IsPublishedItem))
		{
			return false;
		}
		string extension = virtualObject.ObjectPath.GetExtension();
		if (IgnoreExtensions.Contains(extension))
		{
			return true;
		}
		if (string.IsNullOrEmpty(extensionFilter))
		{
			return false;
		}
		return !extension.Equals(extensionFilter);
	}

	protected void InvokeCollectionChanged()
	{
		if (CollectionChanged != null)
		{
			CollectionChanged();
		}
	}

	protected void OnFolderChanged()
	{
		InvokeCollectionChanged();
	}

	private void OnFolderOpened()
	{
		InvokeCollectionChanged();
	}
}
