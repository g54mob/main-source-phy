using DarkTonic.MasterAudio;
using UnityEngine;

public class SimAudio
{
	private static readonly float DEFAULT_FADE_TIME_SECONDS = 0.2f;

	public static void Play(string id, Vector3 position, bool useSimPitch = true)
	{
		if (useSimPitch)
		{
			MasterAudio.PlaySound3DAtVector3AndForget(id, position, 1f, AudioMixerManager.Pitch);
		}
		else
		{
			MasterAudio.PlaySound3DAtVector3AndForget(id, position);
		}
	}

	public static SoundGroupVariation PlaySound3DFollowTransform(string id, Transform transform)
	{
		return MasterAudio.PlaySound3DFollowTransform(id, transform)?.ActingVariation;
	}

	public static SoundGroupVariation Loop(string id, Vector3 position, float vol = 1f, float pitch = 1f)
	{
		return MasterAudio.PlaySound3DAtVector3(id, position, vol, pitch)?.ActingVariation;
	}

	public static void StopLoop(SoundGroupVariation instance, bool skipLinked = false)
	{
		if (instance != null)
		{
			if (!skipLinked)
			{
				instance.FadeOutNowAndStop(DEFAULT_FADE_TIME_SECONDS);
			}
			else
			{
				instance.Stop(stopEndDetection: false, skipLinked: true);
			}
		}
	}
}
