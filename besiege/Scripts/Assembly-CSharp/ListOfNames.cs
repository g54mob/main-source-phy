using System.IO;

public class ListOfNames : SingleInstance<ListOfNames>
{
	public string[] firstNames;

	public string[] surnames;

	public string[] sheepNames;

	public string[] chickenNames;

	private string folderName;

	public override string Name
	{
		get
		{
			return "ListOfNames";
		}
	}

	private void Awake()
	{
		folderName = "NpcNames";
		CheckDirectory();
		ReadFirstNames();
		ReadSurnames();
		ReadSheepNames();
		ReadChickenNames();
	}

	private void ReadFirstNames()
	{
		firstNames = GetNames("HumanFirstNames");
	}

	private void ReadSurnames()
	{
		surnames = GetNames("HumanSurnames");
	}

	private void ReadSheepNames()
	{
		sheepNames = GetNames("SheepNames");
	}

	private void ReadChickenNames()
	{
		chickenNames = GetNames("ChickenNames");
	}

	private string[] GetNames(string file)
	{
		string path = StaticSettings.DataPath + "/" + folderName + "/" + file + ".txt";
		if (!File.Exists(path))
		{
			return new string[1] { "UNKNOWN" };
		}
		return File.ReadAllText(path).Split('\n');
	}

	private void CheckDirectory()
	{
		string path = StaticSettings.DataPath + "/" + folderName;
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
	}
}
