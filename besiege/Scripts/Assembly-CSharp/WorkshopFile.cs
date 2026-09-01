public class WorkshopFile : LocalFile, IWorkshopItem
{
	public ulong WorkshopItemId { get; set; }

	public bool IsPublishedItem { get; set; }

	public bool IsInstalled { get; set; }

	public bool IsOwner { get; set; }

	public ulong Author { get; set; }

	public uint DlcDependencyMask { get; set; }

	public bool AreDlcRequirementsMet { get; set; }

	public uint SubscribeTime { get; set; }

	public override bool IsDeletable
	{
		get
		{
			return WorkshopItemId != 0;
		}
	}

	public override bool IsUploadable
	{
		get
		{
			return false;
		}
	}

	public WorkshopFile()
	{
	}

	public WorkshopFile(FileSystemPath path, FileSystemPath thumbnailPath)
		: base(path, thumbnailPath)
	{
	}
}
