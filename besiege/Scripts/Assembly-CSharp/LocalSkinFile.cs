public class LocalSkinFile : LocalFile
{
	public override string Name
	{
		get
		{
			return CleanedSkinName(base.Name);
		}
	}

	public override bool HasSuffix
	{
		get
		{
			return false;
		}
	}

	public BlockSkinLoader.SkinPack SkinPack { get; set; }

	public override bool IsDeletable
	{
		get
		{
			return SkinPack.type != PackType.Official;
		}
	}

	public override bool IsUploadable
	{
		get
		{
			return SkinPack.type != PackType.Official && !SkinPack.hasInvalidSkins;
		}
	}

	public LocalSkinFile()
		: base(FileSystemPath.Root, FileSystemPath.Root)
	{
	}

	public LocalSkinFile(FileSystemPath path, FileSystemPath thumbnailPath)
		: base(path, thumbnailPath)
	{
	}

	private string CleanedSkinName(string objectName)
	{
		string text = objectName.ToUpper().TrimEnd();
		while (true)
		{
			if (text.EndsWith("SKIN"))
			{
				text = text.Replace("SKIN", string.Empty);
				text = text.TrimEnd();
				break;
			}
			if (text.EndsWith("PACK"))
			{
				text = text.Replace("PACK", string.Empty);
				text = text.TrimEnd();
				continue;
			}
			if (text.EndsWith("PACKAGE"))
			{
				text = text.Replace("PACKAGE", string.Empty);
				text = text.TrimEnd();
				continue;
			}
			if (text.EndsWith("包"))
			{
				text = ((!text.EndsWith("图像包")) ? text.Replace("包", string.Empty) : text.Replace("图像包", string.Empty));
			}
			break;
		}
		return text;
	}

	public override void Delete()
	{
		if (SkinPack != null)
		{
			SkinPack.Delete();
		}
		InvokeObjectDeleted();
	}
}
