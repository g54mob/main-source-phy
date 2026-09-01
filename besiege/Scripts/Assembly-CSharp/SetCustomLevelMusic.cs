using UnityEngine;

public class SetCustomLevelMusic : MonoBehaviour
{
	public MusicController musicController;

	public AudioClip customTrack;

	public float customVolume = 0.65f;

	public bool ignoreConfig;

	private void Start()
	{
		if (customTrack != null)
		{
			SingleInstance<MusicController>.Instance.PlayCustomTrack(customTrack, customVolume, 1f, ignoreConfig);
		}
	}

	private void OnEnabled()
	{
	}
}
