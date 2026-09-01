using UnityEngine;

public class FadeInOnStart : MonoBehaviour
{
	public FadeAudio fadeAudio;

	public float vol = 0.3f;

	public float fadeDuration = 0.5f;

	private void Start()
	{
		StartCoroutine(fadeAudio.FadeAudioVolume(vol, fadeDuration));
	}
}
