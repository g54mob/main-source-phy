using System.IO;

public class WorkshopFolder : LocalFolder<WorkshopFile>, IWorkshopItem
{
	public ulong WorkshopItemId { get; set; }

	public bool IsPublishedItem { get; set; }

	public bool IsInstalled { get; set; }

	public bool IsOwner { get; set; }

	public uint DlcDependencyMask { get; set; }

	public bool AreDlcRequirementsMet { get; set; }

	public override bool IsDeletable
	{
		get
		{
			return WorkshopItemId != 0;
		}
	}

	public WorkshopFolder()
		: base(FileSystemPath.Root, FileSystemPath.Root)
	{
	}

	public WorkshopFolder(FileSystemPath path, FileSystemPath thumbnailPath)
		: base(path, thumbnailPath)
	{
	}

	public WorkshopFolder(DirectoryInfo directoryInfo)
		: base(directoryInfo)
	{
	}
}
