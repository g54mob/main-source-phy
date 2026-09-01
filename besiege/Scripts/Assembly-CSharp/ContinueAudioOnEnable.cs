using UnityEngine;

public class ContinueAudioOnEnable : MonoBehaviour
{
	public AudioSource sfx;

	[SerializeField]
	[HideInInspector]
	private int samples;

	private void OnEnable()
	{
		sfx.timeSamples = samples;
	}

	private void Update()
	{
		samples = sfx.timeSamples;
	}
}
