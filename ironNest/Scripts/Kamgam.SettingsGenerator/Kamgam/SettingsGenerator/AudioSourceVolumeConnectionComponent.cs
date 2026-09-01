using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kamgam.SettingsGenerator
{
	public class AudioSourceVolumeConnectionComponent : MonoBehaviour
	{
		public SettingsProvider SettingsProvider;

		public string ID;

		[SerializeField]
		[FormerlySerializedAs("InputRange")]
		[Tooltip("How the input should be mapped to the required output of 0f..1f (X = min, Y = max).\nUseful if you have a range in percent (from 0 to 100) but need output ranging from 0f to 1f.")]
		protected Vector2 _inputRange;

		[SerializeField]
		[FormerlySerializedAs("AudioSources")]
		protected AudioSource[] _audioSources;

		public Vector2 InputRange
		{
			get
			{
				return default(Vector2);
			}
			set
			{
			}
		}

		public List<AudioSource> AudioSources
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public AudioSourceVolumeConnection Connection => null;

		public void Start()
		{
		}

		public void OnDestroy()
		{
		}

		public void AddAudioSources(IList<AudioSource> audioSources)
		{
		}

		public void RemoveAudioSources(IList<AudioSource> audioSources)
		{
		}

		public void Reset()
		{
		}
	}
}
