using System.Collections.Generic;
using UnityEngine;

public class AudioEmitters : MonoBehaviour
{
	public static List<AudioEmitter> m_Emitters = new List<AudioEmitter>();

	public static void Clear()
	{
		StopAll();
		m_Emitters.Clear();
	}

	public static void Add(AudioEmitter emitter)
	{
		if (!m_Emitters.Contains(emitter))
		{
			m_Emitters.Add(emitter);
		}
	}

	public static void Remove(AudioEmitter emitter)
	{
		if (m_Emitters.Contains(emitter))
		{
			m_Emitters.Remove(emitter);
		}
	}

	public static void PlayAll()
	{
		foreach (AudioEmitter emitter in m_Emitters)
		{
			if (!emitter.IsPlaying())
			{
				emitter.Play();
			}
		}
	}

	public static void StopAll()
	{
		foreach (AudioEmitter emitter in m_Emitters)
		{
			emitter.Stop();
		}
	}
}
