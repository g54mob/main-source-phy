using DarkTonic.MasterAudio;
using UnityEngine;

public class AudioEmitter : MonoBehaviour
{
	[SoundGroup]
	public string m_AudioGroup = "[None]";

	[Range(0f, 1f)]
	public float m_Volume = 1f;

	private SoundGroupVariation m_ActingVariation;

	private void Awake()
	{
		AudioEmitters.Add(this);
	}

	private void OnDestroy()
	{
		if (IsPlaying())
		{
			m_ActingVariation.Stop();
		}
		AudioEmitters.Remove(this);
	}

	public bool IsPlaying()
	{
		if (m_ActingVariation != null)
		{
			return m_ActingVariation.IsPlaying;
		}
		return false;
	}

	public void Play()
	{
		m_ActingVariation = MasterAudio.PlaySound3DAtVector3(m_AudioGroup, base.transform.position, m_Volume)?.ActingVariation;
	}

	public void Stop()
	{
		if (m_ActingVariation != null)
		{
			m_ActingVariation.FadeOutNowAndStop(0.5f);
			m_ActingVariation = null;
		}
	}
}
