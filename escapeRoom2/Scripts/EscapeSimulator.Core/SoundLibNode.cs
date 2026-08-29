using System.Collections.Generic;
using FMOD.Studio;

public class SoundLibNode
{
	public string name;

	public bool unfolded;

	public EventDescription fmodEvent;

	public List<SoundLibNode> children;
}
