using System;
using UnityEngine;
using UnityEngine.Events;

namespace MoreMountains.Tools
{
	[Serializable]
	public class Beat
	{
		public enum Modes
		{
			Raw = 0,
			Normalized = 1,
			BufferedRaw = 2,
			BufferedNormalized = 3,
			Amplitude = 4,
			NormalizedAmplitude = 5,
			AmplitudeBuffered = 6,
			NormalizedAmplitudeBuffered = 7
		}

		public enum BeatValueModes
		{
			Remapped = 0,
			Live = 1
		}

		public string Name;

		public Modes Mode;

		public BeatValueModes BeatValueMode;

		[MMEnumCondition("Mode", new int[] { 0, 1, 2, 3 })]
		public Color BeatColor;

		public int BandID;

		public float Threshold;

		public float MinimumTimeBetweenBeats;

		[MMEnumCondition("BeatValueMode", new int[] { 0 })]
		public float RemappedAttack;

		[MMEnumCondition("BeatValueMode", new int[] { 0 })]
		public float RemappedDecay;

		[MMReadOnly]
		public bool BeatThisFrame;

		[MMReadOnly]
		public float CurrentValue;

		[HideInInspector]
		public float _previousValue;

		[HideInInspector]
		public float _lastBeatAt;

		[HideInInspector]
		public float _lastBeatValue;

		[HideInInspector]
		public bool _initialized;

		public UnityEvent OnBeat;

		public void InitializeIfNeeded(int id, int bandID)
		{
		}
	}
}
