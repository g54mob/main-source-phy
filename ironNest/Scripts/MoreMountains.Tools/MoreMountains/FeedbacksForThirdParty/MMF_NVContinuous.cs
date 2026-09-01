using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackPath("Haptics/Haptic Continuous")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.NiceVibrations", null)]
	[FeedbackHelp("Add this feedback to play a continuous haptic of the specified amplitude and frequency over a certain duration. This feedback will also let you randomize these, and modulate them over time.")]
	public class MMF_NVContinuous : MMF_Feedback
	{
		[CompilerGenerated]
		private sealed class _003CRealtimeModulationCo_003Ed__17 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_NVContinuous _003C_003E4__this;

			private float _003Cjourney_003E5__2;

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
			public _003CRealtimeModulationCo_003Ed__17(int _003C_003E1__state)
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

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Haptic Amplitude", true, 31, false, false)]
		[Tooltip("the minimum amplitude at which this clip should play (amplitude will be randomized between MinAmplitude and MaxAmplitude)")]
		[Range(0f, 1f)]
		public float MinAmplitude;

		[Tooltip("the maximum amplitude at which this clip should play (amplitude will be randomized between MinAmplitude and MaxAmplitude)")]
		[Range(0f, 1f)]
		public float MaxAmplitude;

		[MMFInspectorGroup("Haptic Frequency", true, 32, false, false)]
		[Tooltip("the minimum frequency at which this clip should play (frequency will be randomized between MinFrequency and MaxFrequency)")]
		[Range(0f, 1f)]
		public float MinFrequency;

		[Tooltip("the maximum frequency at which this clip should play (frequency will be randomized between MinFrequency and MaxFrequency)")]
		[Range(0f, 1f)]
		public float MaxFrequency;

		[MMFInspectorGroup("Duration", true, 33, false, false)]
		[Tooltip("the minimum duration at which this clip should play (duration will be randomized between MinDuration and MaxDuration)")]
		public float MinDuration;

		[Tooltip("the maximum duration at which this clip should play (duration will be randomized between MinDuration and MaxDuration)")]
		public float MaxDuration;

		[MMFInspectorGroup("Real-time Modulation", true, 34, false, false)]
		[Tooltip("whether or not to modulate the haptic signal at runtime")]
		public bool UseRealTimeModulation;

		[Tooltip("if UseRealTimeModulation:true, the curve along which to modulate amplitude for this continuous haptic, over its total duration")]
		[MMFCondition("UseRealTimeModulation", true)]
		public AnimationCurve AmplitudeMultiplication;

		[Tooltip("if UseRealTimeModulation:true, the curve along which to modulate frequency for this continuous haptic, over its total duration")]
		[MMFCondition("UseRealTimeModulation", true)]
		public AnimationCurve ShiftFrequency;

		[MMFInspectorGroup("Settings", true, 16, false, false)]
		[Tooltip("a set of settings you can tweak to specify how and when exactly this haptic should play")]
		public MMFeedbackNVSettings HapticSettings;

		protected Coroutine _coroutine;

		protected float _duration;

		public override float FeedbackDuration
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		[IteratorStateMachine(typeof(_003CRealtimeModulationCo_003Ed__17))]
		protected virtual IEnumerator RealtimeModulationCo()
		{
			return null;
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}
	}
}
