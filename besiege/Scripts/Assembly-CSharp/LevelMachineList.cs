using System.Collections.Generic;
using UnityEngine;

public class LevelMachineList : MonoBehaviour
{
	public GameObject entryTemplate;

	public UIButton saveButton;

	public UIButtonExtended allowModButton;

	private ThumbnailCreator thumbnailCreator;

	private Transform saveTransform;

	private GameObject saveGO;

	private LevelSettingsScreen settingsScreen;

	private LevelEditor levelEditor;

	private Vector3 entryPos;

	private List<LevelMachineEntry> entries;

	private int maxCount = 5;

	private float startX;

	private float deltaX;

	public void Init(LevelSettingsScreen settings)
	{
		settingsScreen = settings;
		entries = new List<LevelMachineEntry>();
		levelEditor = LevelEditor.Instance;
		entryPos = entryTemplate.transform.position;
		saveTransform = saveButton.transform;
		saveGO = saveButton.gameObject;
		startX = entryPos.x;
		deltaX = entryTemplate.transform.localScale.x * 1.5f + 0.065f;
		entryTemplate.SetActive(false);
		saveButton.Click += OnSave;
		allowModButton.Down += OnMod;
		thumbnailCreator = GetComponent<ThumbnailCreator>();
	}

	private void OnMod()
	{
		levelEditor.Settings.AllowModMachines = !levelEditor.Settings.allowModMachines;
		allowModButton.ToggleBG(levelEditor.Settings.allowModMachines);
	}

	private void OnSave()
	{
		if (PlayerData.hasLocalPlayer)
		{
			PlayerData localPlayer = PlayerData.localPlayer;
			if (!localPlayer.isSpectator)
			{
				ServerMachine machine = PlayerData.localPlayer.machine;
				MachineInfo info = machine.CreateMachineInfo();
				byte[] thumb = thumbnailCreator.CaptureImageBytes(TextureFormat.RGB24, false, false, true, 10);
				levelEditor.Settings.AllowedMachines.Add(new LevelSettings.LevelMachine(info, thumb));
				settingsScreen.OnUpdateSettings();
			}
		}
	}

	public void OnDelete(LevelSettings.LevelMachine entry)
	{
		levelEditor.Settings.AllowedMachines.Remove(entry);
		settingsScreen.OnUpdateSettings();
	}

	private Vector3 GetPos(int index)
	{
		return new Vector3(startX + deltaX * (float)index, entryPos.y, entryPos.z);
	}

	public void Refresh()
	{
		for (int i = 0; i < entries.Count; i++)
		{
			Object.Destroy(entries[i].gameObject);
		}
		entries.Clear();
		List<LevelSettings.LevelMachine> allowedMachines = levelEditor.Settings.AllowedMachines;
		for (int i = 0; i < allowedMachines.Count; i++)
		{
			LevelSettings.LevelMachine machineEntry = allowedMachines[i];
			GameObject gameObject = Object.Instantiate(entryTemplate);
			gameObject.SetActive(true);
			Transform transform = gameObject.transform;
			transform.SetParent(entryTemplate.transform.parent, true);
			transform.position = GetPos(i);
			LevelMachineEntry component = gameObject.GetComponent<LevelMachineEntry>();
			component.Init(this, machineEntry);
			entries.Add(component);
		}
		allowModButton.gameObject.SetActive(allowedMachines.Count > 0);
		allowModButton.ToggleBG(levelEditor.Settings.allowModMachines);
		saveGO.SetActive(!PlayerData.localPlayer.isSpectator && allowedMachines.Count < maxCount);
		if (saveGO.activeSelf)
		{
			saveTransform.position = GetPos(allowedMachines.Count) + 0.293f * Vector3.right;
			allowModButton.transform.position = saveTransform.position + 1.042f * Vector3.right;
		}
		else
		{
			allowModButton.transform.position = GetPos(allowedMachines.Count);
		}
	}
}
