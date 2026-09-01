using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Gameplay/Newspaper Music Relay")]
public class NewspaperMusicRelay : MonoBehaviour
{
	[Tooltip("Music played while the end-of-mission newspaper and stats are visible.")]
	public AudioClip AudioClip;

	[Tooltip("Resume the previous record music when the stats screen is dismissed.")]
	public bool RestorePreviousMusic;

	[Tooltip("Resume the previous record music when the stats screen is dismissed.")]
	public bool PlayOnEnable;

	private RecordPlayerController recordPlayerController;

	private bool warnedMissingController;

	private void OnEnable()
	{
	}

	public void PlayConfiguredMusic()
	{
	}

	private bool ResolveRecordPlayer()
	{
		return false;
	}
}
