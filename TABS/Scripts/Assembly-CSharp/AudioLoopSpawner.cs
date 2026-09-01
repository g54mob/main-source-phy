using UnityEngine;

public class AudioLoopSpawner : MonoBehaviour
{
	private AudioSource source;

	private bool hasStartedLoop;

	private void Awake()
	{
		source = GetComponent<AudioSource>();
	}

	public void StartLoop(AudioClip clip)
	{
		if (Application.isPlaying && !hasStartedLoop)
		{
			AudioSource audioSource = new GameObject(clip.name + " loop").AddComponent<AudioSource>();
			audioSource.bypassEffects = source.bypassEffects;
			audioSource.bypassListenerEffects = source.bypassListenerEffects;
			audioSource.dopplerLevel = source.dopplerLevel;
			audioSource.maxDistance = source.maxDistance;
			audioSource.rolloffMode = source.rolloffMode;
			audioSource.minDistance = source.minDistance;
			audioSource.outputAudioMixerGroup = source.outputAudioMixerGroup;
			audioSource.panStereo = source.panStereo;
			audioSource.pitch = source.pitch;
			audioSource.priority = source.priority;
			audioSource.reverbZoneMix = source.reverbZoneMix;
			audioSource.spatialBlend = source.spatialBlend;
			audioSource.spatialize = source.spatialize;
			audioSource.spatializePostEffects = source.spatializePostEffects;
			audioSource.spread = source.spread;
			audioSource.volume = source.volume;
			audioSource.velocityUpdateMode = source.velocityUpdateMode;
			audioSource.transform.position = base.transform.position;
			audioSource.clip = clip;
			audioSource.loop = true;
			audioSource.Play();
			Debug.Log("Starting Loop!: " + clip.name);
			hasStartedLoop = true;
		}
	}
}
