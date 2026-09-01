public class WorkshopSkinFile : LocalSkinFile, IWorkshopItem
{
	public ulong WorkshopItemId { get; set; }

	public bool IsPublishedItem { get; set; }

	public bool IsInstalled
	{
		get
		{
			return true;
		}
	}

	public bool IsOwner { get; set; }

	public uint DlcDependencyMask { get; set; }

	public bool AreDlcRequirementsMet { get; set; }

	public override bool IsDeletable
	{
		get
		{
			return true;
		}
	}

	public override bool IsUploadable
	{
		get
		{
			return false;
		}
	}

	public WorkshopSkinFile(FileSystemPath objectPath, FileSystemPath thumbnailPath)
		: base(objectPath, thumbnailPath)
	{
	}

	public WorkshopSkinFile()
	{
	}

	public override void Delete()
	{
		base.Delete();
		if (WorkshopItemId != 0L)
		{
			WorkshopManager instance = SingleInstance<WorkshopManager>.Instance;
			instance.Unsubscribe(WorkshopItemId);
		}
	}
}
