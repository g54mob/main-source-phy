using Steamworks;

public class PublishedSteamFile : WorkshopFile
{
	public string Tags { get; set; }

	public UGCHandle_t PreviewImageHandle { get; set; }

	public override bool HasSuffix
	{
		get
		{
			return false;
		}
	}

	public override bool IsDeletable
	{
		get
		{
			return false;
		}
	}

	public PublishedSteamFile(FileSystemPath path, FileSystemPath thumbnailPath)
		: base(path, thumbnailPath)
	{
	}
}
