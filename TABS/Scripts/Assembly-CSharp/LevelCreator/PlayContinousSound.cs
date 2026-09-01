using System.Collections;
using UnityEngine;

namespace LevelCreator
{
	public class PlayContinousSound : MonoBehaviour
	{
		public Transform m_followTransform;

		private bool m_loop;

		private float m_fadeOutTime;

		private AudioPlayer m_player;

		public void Play(ContinousSoundData soundData)
		{
			if (!m_loop)
			{
				m_loop = true;
				StartCoroutine(PlayInternal(soundData));
			}
		}

		public void Stop(float fadeOutTime = 2f)
		{
			if (m_loop)
			{
				m_fadeOutTime = fadeOutTime;
				m_loop = false;
				if (this != null && m_player != null && m_fadeOutTime > 0f)
				{
					StartCoroutine(m_player.FadeOut(m_fadeOutTime));
				}
			}
		}

		private IEnumerator PlayInternal(ContinousSoundData soundData)
		{
			m_player = PlaySound(soundData.soundRef);
			SetAudioSourcePlaybackTime(m_player, 0f);
			while (m_loop)
			{
				if (m_player != null && m_player.source != null)
				{
					if (m_player.source.time >= soundData.loopEnd)
					{
						SetAudioSourcePlaybackTime(m_player, soundData.loopStart);
					}
					if (!m_player.source.isPlaying)
					{
						m_player = PlaySound(soundData.soundRef);
					}
				}
				else
				{
					m_player = PlaySound(soundData.soundRef);
				}
				yield return null;
			}
			SetAudioSourcePlaybackTime(m_player, soundData.loopEnd);
			yield return null;
			if (base.gameObject != null)
			{
				Object.Destroy(base.gameObject, m_fadeOutTime);
			}
		}

		private AudioPlayer PlaySound(string soundRef)
		{
			if (m_followTransform == null)
			{
				return Utility.PlaySound(soundRef, 1f, base.transform.position);
			}
			return Utility.PlaySound(soundRef, 1f, m_followTransform);
		}

		private void SetAudioSourcePlaybackTime(AudioPlayer player, float time)
		{
			if (player != null && player.source != null && player.source.clip != null && player.source.clip.length >= time)
			{
				player.source.time = time;
			}
		}

		private void Update()
		{
			if (m_followTransform != null)
			{
				base.transform.position = m_followTransform.position;
			}
			else
			{
				Stop(0f);
			}
		}
	}
}
