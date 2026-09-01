using System.Collections.Generic;

public class AudioPlayerListWithFloat
{
	public List<AudioPlayer> audioPlayerList = new List<AudioPlayer>();

	public float lastPlayed;

	public AudioPlayer GetLastAssigned()
	{
		audioPlayerList.Sort((AudioPlayer player01, AudioPlayer player02) => player01.source.time.CompareTo(player02.source.time));
		return audioPlayerList[audioPlayerList.Count - 1];
	}
}
