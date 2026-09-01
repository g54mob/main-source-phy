public class LevelMachineEntry : ClickBehaviour
{
	public UIButton deleteButton;

	public ThumbnailComponent thumbnailCode;

	private LevelMachineList list;

	private LevelSettings.LevelMachine entry;

	private void Awake()
	{
		if (deleteButton != null)
		{
			deleteButton.Click += OnDelete;
		}
	}

	public void Init(LevelMachineList machineList, LevelSettings.LevelMachine machineEntry)
	{
		list = machineList;
		entry = machineEntry;
		thumbnailCode.Initialize(machineEntry.thumbBytes, false);
	}

	private void OnDelete()
	{
		list.OnDelete(entry);
	}
}
