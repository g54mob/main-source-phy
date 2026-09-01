using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Audio/MMAudioAnalyzer")]
	public class MMAudioAnalyzer : MonoBehaviour
	{
		public enum Modes
		{
			Global = 0,
			AudioSource = 1,
			Microphone = 2
		}

		[CompilerGenerated]
		private sealed class _003CAnalyze_003Ed__36 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMAudioAnalyzer _003C_003E4__this;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CAnalyze_003Ed__36(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[CompilerGenerated]
		private sealed class _003CRemapBeat_003Ed__42 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public Beat beat;

			private float _003CremapStartedAt_003E5__2;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CRemapBeat_003Ed__42(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[Header("Source")]
		[MMInformation("This component lets you pick an audio source (either global : the whole scene's audio, a unique source, or the microphone), and will cut it into chunks that you can then use to emit beat events, that other objects can consume and act upon. The sample interval is the frequency at which sound will be analyzed, the amount of spectrum samples will determine the accuracy of the sampling, the window defines the method used to reduce leakage, and the number of bands will determine in how many bands you want to cut the sound. The more bands, the more levers you'll have to play with afterwards.In general, for all of these settings, higher values mean better quality and lower performance. The buffer speed determines how fast buffered band levels readjust.", MMInformationAttribute.InformationType.Info, false)]
		[MMReadOnlyWhenPlaying]
		public Modes Mode;

		[MMEnumCondition("Mode", new int[] { 1 })]
		[MMReadOnlyWhenPlaying]
		public AudioSource TargetAudioSource;

		[MMEnumCondition("Mode", new int[] { 2 })]
		public int MicrophoneID;

		[Header("Sampling")]
		[MMReadOnlyWhenPlaying]
		public float SampleInterval;

		[MMDropdown(new object[]
		{
			2, 4, 8, 16, 32, 64, 128, 256, 512, 1024,
			2048, 4096, 8192
		})]
		[MMReadOnlyWhenPlaying]
		public int SpectrumSamples;

		[MMReadOnlyWhenPlaying]
		public FFTWindow Window;

		[Range(1f, 64f)]
		[MMReadOnlyWhenPlaying]
		public int NumberOfBands;

		public float BufferSpeed;

		[Header("Beat Events")]
		public Beat[] Beats;

		[HideInInspector]
		public float[] RawSpectrum;

		[HideInInspector]
		public float[] BandLevels;

		[HideInInspector]
		public float[] BufferedBandLevels;

		[HideInInspector]
		public float[] BandPeaks;

		[HideInInspector]
		public float[] LastPeaksAt;

		[HideInInspector]
		public float[] NormalizedBandLevels;

		[HideInInspector]
		public float[] NormalizedBufferedBandLevels;

		[HideInInspector]
		public float Amplitude;

		[HideInInspector]
		public float NormalizedAmplitude;

		[HideInInspector]
		public float BufferedAmplitude;

		[HideInInspector]
		public float NormalizedBufferedAmplitude;

		[HideInInspector]
		public bool Active;

		[HideInInspector]
		public bool PeaksPasted;

		protected const int _microphoneDuration = 5;

		protected string _microphone;

		protected float _microphoneStartedAt;

		protected const float _microphoneDelay = 0.03f;

		protected const float _microphoneFrequency = 24000f;

		protected WaitForSeconds _sampleIntervalWaitForSeconds;

		protected int _cachedNumberOfBands;

		public virtual void FindPeaks()
		{
		}

		public virtual void PastePeaks()
		{
		}

		public virtual void ClearPeaks()
		{
		}

		protected virtual void Awake()
		{
		}

		public virtual void Initialization()
		{
		}

		protected virtual void Update()
		{
		}

		[IteratorStateMachine(typeof(_003CAnalyze_003Ed__36))]
		protected virtual IEnumerator Analyze()
		{
			return null;
		}

		protected virtual void HandleBuffer()
		{
		}

		protected virtual void ComputeBandLevels()
		{
		}

		protected virtual void ComputeAmplitudes()
		{
		}

		protected virtual void HandleBeats()
		{
		}

		protected virtual void OnBeat(Beat beat, float rawValue)
		{
		}

		[IteratorStateMachine(typeof(_003CRemapBeat_003Ed__42))]
		protected virtual IEnumerator RemapBeat(Beat beat)
		{
			return null;
		}

		protected virtual void OnValidate()
		{
		}
	}
}
