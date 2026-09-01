using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[AddComponentMenu("Steamworks/Voice Recorder")]
	[HelpURL("https://kb.heathen.group/steam/features/voice")]
	public class VoiceRecorder : MonoBehaviour
	{
		[Serializable]
		public class ByteArrayEvent : UnityEvent<byte[]>
		{
		}

		[Range(0f, 1f)]
		public float bufferLength;

		[ReadOnly(true)]
		[SerializeField]
		private bool isRecording;

		public UnityEvent onStoppedOnChatRestricted;

		public ByteArrayEvent onVoiceStream;

		private float _packetCounter;

		public bool IsRecording
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		private void Start()
		{
		}

		private void Update()
		{
		}

		public void StartRecording()
		{
		}

		public void StopRecording()
		{
		}
	}
}
