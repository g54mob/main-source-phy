using UnityEngine;

public class MusicSelectionUI : MonoBehaviour
{
	public UIButtonExtended[] trackButtons;

	public int selectedTrack;

	protected int lastSelectedTrack = -1;

	private LevelSettingsScreen settings;

	private LevelEditor levelEditor;

	public bool useSkipper;

	public int maxTracks = 10;

	public TextMesh trackText;

	public void Init(LevelSettingsScreen settingsScreen)
	{
		settings = settingsScreen;
		levelEditor = LevelEditor.Instance;
		Reset();
	}

	public void Refresh()
	{
		selectedTrack = levelEditor.Settings.MusicID;
		if (useSkipper)
		{
			trackText.text = string.Empty + (selectedTrack + 1);
		}
		else
		{
			UpdateSelection(levelEditor.Settings.MusicID);
		}
	}

	public void Reset()
	{
		lastSelectedTrack = -1;
		if (useSkipper)
		{
			trackButtons[0].ResetDelegates();
			trackButtons[1].ResetDelegates();
			trackButtons[0].DownRef += DecreaseSelection;
			trackButtons[1].DownRef += IncreaseSelection;
			return;
		}
		for (int i = 0; i < trackButtons.Length; i++)
		{
			UIButtonExtended uIButtonExtended = trackButtons[i];
			uIButtonExtended.ResetDelegates();
			uIButtonExtended.DownRef += UpdateSelection;
		}
	}

	public void DecreaseSelection(UIButtonExtended button)
	{
		selectedTrack--;
		if (selectedTrack < 0)
		{
			selectedTrack = maxTracks - 1;
		}
		levelEditor.Settings.MusicID = selectedTrack;
		trackText.text = string.Empty + (selectedTrack + 1);
		settings.OnUpdateSettings();
	}

	public void IncreaseSelection(UIButtonExtended button)
	{
		selectedTrack++;
		if (selectedTrack >= maxTracks)
		{
			selectedTrack = 0;
		}
		levelEditor.Settings.MusicID = selectedTrack;
		trackText.text = string.Empty + (selectedTrack + 1);
		settings.OnUpdateSettings();
	}

	public void UpdateSelection(UIButtonExtended button)
	{
		int musicID = 0;
		for (int i = 0; i < trackButtons.Length; i++)
		{
			if (trackButtons[i] == button)
			{
				musicID = i;
			}
		}
		levelEditor.Settings.MusicID = musicID;
		settings.OnUpdateSettings();
	}

	public void UpdateSelection(int track)
	{
		for (int i = 0; i < trackButtons.Length; i++)
		{
			UIButtonExtended uIButtonExtended = trackButtons[i];
			uIButtonExtended.BG.SetActive(i == track);
		}
	}
}
