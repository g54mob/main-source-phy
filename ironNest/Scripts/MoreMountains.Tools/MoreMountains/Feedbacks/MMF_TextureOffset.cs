using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you control the texture offset of a target material over time.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Renderer/Texture Offset")]
	public class MMF_TextureOffset : MMF_Feedback
	{
		public enum Modes
		{
			OverTime = 0,
			Instant = 1
		}

		[CompilerGenerated]
		private sealed class _003CTransitionCo_003Ed__29 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_TextureOffset _003C_003E4__this;

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
			public _003CTransitionCo_003Ed__29(int _003C_003E1__state)
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

		[MMFInspectorGroup("Texture Scale", true, 63, true, false)]
		[Tooltip("the renderer on which to change texture offset on")]
		public Renderer TargetRenderer;

		[Tooltip("the material index")]
		public int MaterialIndex;

		[Tooltip("the property name, for example _MainTex_ST, or _MainTex if you don't have UseMaterialPropertyBlocks set to true")]
		public string MaterialPropertyName;

		[Tooltip("whether the feedback should affect the material instantly or over a period of time")]
		public Modes Mode;

		[Tooltip("how long the material should change over time")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public float Duration;

		[Tooltip("whether or not the values should be relative")]
		public bool RelativeValues;

		[Tooltip("if this is true, calling that feedback will trigger it, even if it's in progress. If it's false, it'll prevent any new Play until the current one is over")]
		public bool AllowAdditivePlays;

		[Tooltip("if this is true, this component will use material property blocks instead of working on an instance of the material.")]
		public bool UseMaterialPropertyBlocks;

		[MMFInspectorGroup("Intensity", true, 65, false, false)]
		[Tooltip("the curve to tween the offset on")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public AnimationCurve OffsetCurve;

		[Tooltip("the value to remap the offset curve's 0 to, randomized between its min and max - put the same value in both min and max if you don't want any randomness")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		[MMFVector(new string[] { "Min", "Max" })]
		public Vector2 RemapZero;

		[Tooltip("the value to remap the offset curve's 1 to, randomized between its min and max - put the same value in both min and max if you don't want any randomness")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		[MMFVector(new string[] { "Min", "Max" })]
		public Vector2 RemapOne;

		[Tooltip("the value to move the intensity to in instant mode")]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		public Vector2 InstantOffset;

		protected Vector2 _initialValue;

		protected Coroutine _coroutine;

		protected Vector2 _newValue;

		protected MaterialPropertyBlock _propertyBlock;

		protected Vector4 _propertyBlockVector;

		public override bool HasAutomatedTargetAcquisition => false;

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

		public override bool HasRandomness => false;

		protected override void AutomateTargetAcquisition()
		{
		}

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		[IteratorStateMachine(typeof(_003CTransitionCo_003Ed__29))]
		protected virtual IEnumerator TransitionCo(float intensityMultiplier)
		{
			return null;
		}

		protected virtual void SetMaterialValues(float time, float intensityMultiplier)
		{
		}

		protected virtual void ApplyValue(Vector2 newValue)
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
