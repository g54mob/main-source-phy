using Localisation;

public class LocalMachineCollection : LocalFileCollection
{
	public override string FilterExtension
	{
		get
		{
			return ".bsg";
		}
	}

	protected override string FolderName
	{
		get
		{
			return "/SavedMachines";
		}
	}

	public LocalMachineCollection()
	{
		ObjectName = LocalisationManager.GetTranslation(927);
	}
}
