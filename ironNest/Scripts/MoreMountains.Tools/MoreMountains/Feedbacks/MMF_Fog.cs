using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you animate the density, color, end and start distance of your scene's fog")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.MMTools", null)]
	[FeedbackPath("Renderer/Fog")]
	public class MMF_Fog : MMF_Feedback
	{
		public enum Modes
		{
			OverTime = 0,
			Instant = 1
		}

		[CompilerGenerated]
		private sealed class _003CFogSequence_003Ed__36 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_Fog _003C_003E4__this;

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
			public _003CFogSequence_003Ed__36(int _003C_003E1__state)
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

		[MMFInspectorGroup("Fog", true, 24, false, false)]
		[Tooltip("whether the feedback should affect the sprite renderer instantly or over a period of time")]
		public Modes Mode;

		[Tooltip("how long the sprite renderer should change over time")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public float Duration;

		[Tooltip("if this is true, calling that feedback will trigger it, even if it's in progress. If it's false, it'll prevent any new Play until the current one is over")]
		public bool AllowAdditivePlays;

		[MMFInspectorGroup("Fog Density", true, 25, false, false)]
		[Tooltip("whether or not to modify the fog's density")]
		public bool ModifyFogDensity;

		[Tooltip("a curve to use to animate the fog's density over time")]
		public MMTweenType DensityCurve;

		[Tooltip("the value to remap the fog's density curve zero value to")]
		public float DensityRemapZero;

		[Tooltip("the value to remap the fog's density curve one value to")]
		public float DensityRemapOne;

		[Tooltip("the value to change the fog's density to when in instant mode")]
		public float DensityInstantChange;

		[MMFInspectorGroup("Fog Start Distance", true, 26, false, false)]
		[Tooltip("whether or not to modify the fog's start distance")]
		public bool ModifyStartDistance;

		[Tooltip("a curve to use to animate the fog's start distance over time")]
		public MMTweenType StartDistanceCurve;

		[Tooltip("the value to remap the fog's start distance curve zero value to")]
		public float StartDistanceRemapZero;

		[Tooltip("the value to remap the fog's start distance curve one value to")]
		public float StartDistanceRemapOne;

		[Tooltip("the value to change the fog's start distance to when in instant mode")]
		public float StartDistanceInstantChange;

		[MMFInspectorGroup("Fog End Distance", true, 27, false, false)]
		[Tooltip("whether or not to modify the fog's end distance")]
		public bool ModifyEndDistance;

		[Tooltip("a curve to use to animate the fog's end distance over time")]
		public MMTweenType EndDistanceCurve;

		[Tooltip("the value to remap the fog's end distance curve zero value to")]
		public float EndDistanceRemapZero;

		[Tooltip("the value to remap the fog's end distance curve one value to")]
		public float EndDistanceRemapOne;

		[Tooltip("the value to change the fog's end distance to when in instant mode")]
		public float EndDistanceInstantChange;

		[MMFInspectorGroup("Fog Color", true, 28, false, false)]
		[Tooltip("whether or not to modify the fog's color")]
		public bool ModifyColor;

		[Tooltip("the colors to apply to the sprite renderer over time")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public Gradient ColorOverTime;

		[Tooltip("the color to move to in instant mode")]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		public Color InstantColor;

		protected Coroutine _coroutine;

		protected Color _initialColor;

		protected float _initialStartDistance;

		protected float _initialEndDistance;

		protected float _initialDensity;

		public override bool HasRandomness => false;

		public override bool HasCustomInspectors => false;

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

		[IteratorStateMachine(typeof(_003CFogSequence_003Ed__36))]
		protected virtual IEnumerator FogSequence(float intensityMultiplier)
		{
			return null;
		}

		protected virtual void SetFogValues(float time, float intensityMultiplier)
		{
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomRestoreInitialValues()
		{
		}
	}
}
