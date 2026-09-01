using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Gameplay/Record Item")]
public class RecordItem : MonoBehaviour
{
	[Header("Tracks")]
	[Tooltip("Ordered list of AudioClips on this record.\n\nPlayback order:\n- Tracks play in order from index 0.\n- If looping is enabled, playback returns to index 0 after the last track.\n- If looping is disabled, playback stops after the last track completes.\n- Leave empty to produce a silent record (no audio will play).")]
	public AudioClip[] tracks;

	[Header("Looping")]
	[Tooltip("If true, the record loops back to track 0 after the final track ends.\nIf false, playback stops after the last track completes and the player\nmust press Play again to restart from track 0.\n\nSafe default: true.")]
	public bool loop;

	[Header("Crossfade")]
	[Tooltip("If true, this record uses smooth crossfade transitions between tracks.\n\nThe fade duration and overlap timing are controlled by the\nRecordPlayerController's Crossfade Settings — not per-track.\n\nWhen looping is enabled, the crossfade also applies to the wrap from\nthe last track back to track 0.\n\nWhen looping is disabled, no crossfade is applied to the final track\nending (playback simply stops).\n\nIf false, tracks switch instantly with a hard cut.\n\nSafe default: false.")]
	public bool useCrossfade;
}
