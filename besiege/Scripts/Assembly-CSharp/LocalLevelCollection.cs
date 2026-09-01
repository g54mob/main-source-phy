using System;
using Localisation;

public class LocalLevelCollection : LocalFileCollection
{
	public override string FilterExtension
	{
		get
		{
			return ".blv";
		}
	}

	protected override string FolderName
	{
		get
		{
			return "/CustomLevels";
		}
	}

	protected override Type FolderType
	{
		get
		{
			return typeof(LocalLevelFolder);
		}
	}

	public LocalLevelCollection()
	{
		ObjectName = LocalisationManager.GetTranslation(2102);
	}
}
