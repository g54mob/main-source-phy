using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you control a 2D light's intensity, color, falloff, shadow strength and volumetric intensity over time, or instantly.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Lights/Light2D_URP")]
	public class MMF_Light2D_URP : MMF_Feedback
	{
		public enum Modes
		{
			OverTime = 0,
			Instant = 1,
			ShakerEvent = 2,
			ToDestination = 3
		}

		[CompilerGenerated]
		private sealed class _003CLightSequence_003Ed__61 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_Light2D_URP _003C_003E4__this;

			public float intensityMultiplier;

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
			public _003CLightSequence_003Ed__61(int _003C_003E1__state)
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

		[MMFInspectorGroup("Light", true, 37, true, false)]
		[Tooltip("the light to affect when playing the feedback")]
		public Light2D BoundLight;

		[Tooltip("whether the feedback should affect the light instantly or over a period of time")]
		public Modes Mode;

		[Tooltip("how long the light should change over time")]
		[MMFEnumCondition("Mode", new int[] { 0, 2, 3 })]
		public float Duration;

		[Tooltip("whether or not that light should be turned off on start")]
		public bool StartsOff;

		[Tooltip("if this is true, the light will be disabled when this feedbacks is stopped")]
		public bool DisableOnStop;

		[Tooltip("whether or not the values should be relative or not")]
		[MMFEnumCondition("Mode", new int[] { 0, 2, 1 })]
		public bool RelativeValues;

		[Tooltip("whether or not to reset shaker values after shake")]
		[MMFEnumCondition("Mode", new int[] { 2 })]
		public bool ResetShakerValuesAfterShake;

		[Tooltip("whether or not to reset the target's values after shake")]
		[MMFEnumCondition("Mode", new int[] { 2 })]
		public bool ResetTargetValuesAfterShake;

		[Tooltip("whether or not to broadcast a range to only affect certain shakers")]
		[MMFEnumCondition("Mode", new int[] { 2 })]
		public bool OnlyBroadcastInRange;

		[Tooltip("the range of the event, in units")]
		[MMFEnumCondition("Mode", new int[] { 2 })]
		public float EventRange;

		[Tooltip("the transform to use to broadcast the event as origin point")]
		[MMFEnumCondition("Mode", new int[] { 2 })]
		public Transform EventOriginTransform;

		[Tooltip("if this is true, calling that feedback will trigger it, even if it's in progress. If it's false, it'll prevent any new Play until the current one is over")]
		public bool AllowAdditivePlays;

		[MMFInspectorGroup("Color", true, 38, true, false)]
		[Tooltip("whether or not to modify the color of the light")]
		public bool ModifyColor;

		[Tooltip("the colors to apply to the light over time")]
		[MMFEnumCondition("Mode", new int[] { 0, 2 })]
		public Gradient ColorOverTime;

		[Tooltip("the color to move to in instant mode")]
		[MMFEnumCondition("Mode", new int[] { 1, 2 })]
		public Color InstantColor;

		[Tooltip("the color to move to in destination mode")]
		[MMFEnumCondition("Mode", new int[] { 3 })]
		public Color ToDestinationColor;

		[MMFInspectorGroup("Intensity", true, 39, true, false)]
		[Tooltip("whether or not to modify the intensity of the light")]
		public bool ModifyIntensity;

		[Tooltip("the curve to tween the intensity on")]
		[MMFEnumCondition("Mode", new int[] { 0, 2, 3 })]
		public AnimationCurve IntensityCurve;

		[Tooltip("the value to remap the intensity curve's 0 to")]
		[MMFEnumCondition("Mode", new int[] { 0, 2 })]
		public float RemapIntensityZero;

		[Tooltip("the value to remap the intensity curve's 1 to")]
		[MMFEnumCondition("Mode", new int[] { 0, 2 })]
		public float RemapIntensityOne;

		[Tooltip("the value to move the intensity to in instant mode")]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		public float InstantIntensity;

		[Tooltip("the value to move the intensity to in ToDestination mode")]
		[MMFEnumCondition("Mode", new int[] { 3 })]
		public float ToDestinationIntensity;

		[MMFInspectorGroup("Falloff", true, 40, true, false)]
		[Tooltip("whether or not to modify the falloff of the light")]
		public bool ModifyFalloff;

		[Tooltip("the falloff to apply to the light over time")]
		[MMFEnumCondition("Mode", new int[] { 0, 2, 3 })]
		public AnimationCurve FalloffCurve;

		[Tooltip("the value to remap the falloff curve's 0 to")]
		[MMFEnumCondition("Mode", new int[] { 0, 2 })]
		public float RemapFalloffZero;

		[Tooltip("the value to remap the falloff curve's 0 to")]
		[MMFEnumCondition("Mode", new int[] { 0, 2 })]
		public float RemapFalloffOne;

		[Tooltip("the value to move the intensity to in instant mode")]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		public float InstantFalloff;

		[Tooltip("the value to move the intensity to in ToDestination mode")]
		[MMFEnumCondition("Mode", new int[] { 3 })]
		public float ToDestinationFalloff;

		[MMFInspectorGroup("Shadow Strength", true, 41, true, false)]
		[Tooltip("whether or not to modify the shadow strength of the light")]
		public bool ModifyShadowStrength;

		[Tooltip("the shadow strength to apply to the light over time")]
		[MMFEnumCondition("Mode", new int[] { 0, 2, 3 })]
		public AnimationCurve ShadowStrengthCurve;

		[Tooltip("the value to remap the shadow strength's curve's 0 to")]
		[MMFEnumCondition("Mode", new int[] { 0, 2 })]
		public float RemapShadowStrengthZero;

		[Tooltip("the value to remap the shadow strength's curve's 1 to")]
		[MMFEnumCondition("Mode", new int[] { 0, 2 })]
		public float RemapShadowStrengthOne;

		[Tooltip("the value to move the shadow strength to in instant mode")]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		public float InstantShadowStrength;

		[Tooltip("the value to move the shadow strength to in ToDestination mode")]
		[MMFEnumCondition("Mode", new int[] { 3 })]
		public float ToDestinationShadowStrength;

		[MMFInspectorGroup("Volumetric Intensity", true, 39, true, false)]
		[Tooltip("whether or not to modify the volumetric intensity of the light")]
		public bool ModifyVolumetricIntensity;

		[Tooltip("the curve to tween the volumetric intensity on")]
		[MMFEnumCondition("Mode", new int[] { 0, 2, 3 })]
		public AnimationCurve VolumetricIntensityCurve;

		[Tooltip("the value to remap the volumetric intensity curve's 0 to")]
		[MMFEnumCondition("Mode", new int[] { 0, 2 })]
		public float RemapVolumetricIntensityZero;

		[Tooltip("the value to remap the volumetric intensity curve's 1 to")]
		[MMFEnumCondition("Mode", new int[] { 0, 2 })]
		public float RemapVolumetricIntensityOne;

		[Tooltip("the value to move the volumetric intensity to in instant mode")]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		public float InstantVolumetricIntensity;

		[Tooltip("the value to move the volumetric intensity to in ToDestination mode")]
		[MMFEnumCondition("Mode", new int[] { 3 })]
		public float ToDestinationVolumetricIntensity;

		protected float _initialFalloff;

		protected float _initialShadowStrength;

		protected float _initialIntensity;

		protected float _initialVolumetricIntensity;

		protected Coroutine _coroutine;

		protected Color _initialColor;

		protected Color _targetColor;

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

		public override bool HasChannel => false;

		public override bool HasRandomness => false;

		public override bool HasAutomatedTargetAcquisition => false;

		protected override void AutomateTargetAcquisition()
		{
		}

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		[IteratorStateMachine(typeof(_003CLightSequence_003Ed__61))]
		protected virtual IEnumerator LightSequence(float intensityMultiplier)
		{
			return null;
		}

		protected virtual void SetLightValues(float time, float intensityMultiplier)
		{
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void Turn(bool status)
		{
		}

		protected override void CustomRestoreInitialValues()
		{
		}
	}
}
