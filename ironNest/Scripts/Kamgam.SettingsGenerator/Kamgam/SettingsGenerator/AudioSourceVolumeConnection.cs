using System.Collections.Generic;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class AudioSourceVolumeConnection : Connection<float>
	{
		[Tooltip("How the input should be mapped to 0f..1f.\nUseful if you have a range in percent (from 0 to 100) but need output ranging from 0f to 1f.")]
		public Vector2 InputRange;

		public List<AudioSource> AudioSources;

		public AudioSourceVolumeConnection(Vector2 inputRange, IList<AudioSource> audioSources)
		{
		}

		public void AddAudioSources(IList<AudioSource> audioSources)
		{
		}

		public void RemoveAudioSources(IList<AudioSource> audioSources)
		{
		}

		public override float Get()
		{
			return 0f;
		}

		public override void Set(float value)
		{
		}

		public void DefragAudioSources()
		{
		}
	}
}
