using System.Collections.Generic;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[AddComponentMenu("Steamworks/Voice Stream")]
	[HelpURL("https://kb.heathen.group/steam/features/voice")]
	public class VoiceStream : MonoBehaviour
	{
		public AudioSource outputSource;

		public SampleRateMethod sampleRateMethod;

		[Range(11025f, 48000f)]
		public uint customSampleRate;

		[Range(0f, 3f)]
		public float playbackDelay;

		private int _sampleRate;

		private Queue<float> _audioBuffer;

		public double encodingTime;

		private void Start()
		{
		}

		private void Update()
		{
		}

		public void PlayVoiceData(byte[] buffer)
		{
		}

		private void OnAudioRead(float[] data)
		{
		}
	}
}
