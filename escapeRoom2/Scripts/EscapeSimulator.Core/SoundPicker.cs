using System.Collections.Generic;
using FMOD;
using FMOD.Studio;

public class SoundPicker
{
	public string selectedFile;

	public SoundLibNode presetSelection;

	public EventInstance playingInstance;

	public FMOD.Sound playingSound;

	public Channel playingChannel;

	public List<string> files;

	public SoundLibNode presetsRoot;
}
