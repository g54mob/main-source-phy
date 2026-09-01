using System.IO;

public class LocalLevelFolder : LocalFolder
{
	public override bool IsUploadable
	{
		get
		{
			return !IsAutosaveFolder();
		}
	}

	public LocalLevelFolder(DirectoryInfo directoryInfo)
		: base(directoryInfo)
	{
	}

	private bool IsAutosaveFolder()
	{
		return ObjectPath.EntityName.ToLower().Equals("autosave");
	}
}
