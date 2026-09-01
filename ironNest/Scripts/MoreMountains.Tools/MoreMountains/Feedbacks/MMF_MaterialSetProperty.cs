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
	[FeedbackHelp("This feedback will let you set a property on the target renderer's material")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Renderer/Material Set Property")]
	public class MMF_MaterialSetProperty : MMF_Feedback
	{
		public enum PropertyTypes
		{
			Color = 0,
			Float = 1,
			Integer = 2,
			Texture = 3,
			TextureOffset = 4,
			TextureScale = 5,
			Vector = 6
		}

		[CompilerGenerated]
		private sealed class _003CInterpolationSequence_003Ed__40 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_MaterialSetProperty _003C_003E4__this;

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
			public _003CInterpolationSequence_003Ed__40(int _003C_003E1__state)
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

		[MMFInspectorGroup("Material", true, 12, true, false)]
		[Tooltip("the renderer to change the material on")]
		public Renderer TargetRenderer;

		[Tooltip("the ID of the material to target on the renderer")]
		public int MaterialID;

		[Tooltip("the ID of the property to set, as exposed by the Visual Effect Graph")]
		public string PropertyID;

		[Tooltip("the type of the property to set")]
		public PropertyTypes PropertyType;

		[Tooltip("if the property is a color, the new color to set")]
		[MMFEnumCondition("PropertyType", new int[] { 0 })]
		public Color NewColor;

		[Tooltip("if the property is a float, the new float to set")]
		[MMFEnumCondition("PropertyType", new int[] { 1 })]
		public float NewFloat;

		[Tooltip("if the property is an int, the new int to set")]
		[MMFEnumCondition("PropertyType", new int[] { 2 })]
		public int NewInt;

		[Tooltip("if the property is a texture, the new texture to set")]
		[MMFEnumCondition("PropertyType", new int[] { 3 })]
		public Texture NewTexture;

		[Tooltip("if the property is a texture offset, the new offset to set")]
		[MMFEnumCondition("PropertyType", new int[] { 4 })]
		public Vector2 NewOffset;

		[Tooltip("if the property is a texture scale, the new scale to set")]
		[MMFEnumCondition("PropertyType", new int[] { 5 })]
		public Vector2 NewScale;

		[Tooltip("if the property is a vector4, the new vector4 to set")]
		[MMFEnumCondition("PropertyType", new int[] { 6 })]
		public Vector4 NewVector;

		[Header("Interpolation")]
		[Tooltip("whether or not to interpolate the value over time. If set to false, the change will be instant")]
		public bool InterpolateValue;

		[Tooltip("the duration of the interpolation")]
		[MMFCondition("InterpolateValue", true)]
		public float Duration;

		[Tooltip("the curve over which to interpolate the value")]
		[MMFCondition("InterpolateValue", true)]
		public MMTweenType InterpolationCurve;

		protected int _propertyID;

		protected Color _initialColor;

		protected float _initialFloat;

		protected int _initialInt;

		protected Texture _initialTexture;

		protected Vector2 _initialOffset;

		protected Vector2 _initialScale;

		protected Vector4 _initialVector;

		protected Coroutine _coroutine;

		protected Color _newColor;

		protected Vector2 _newVector2;

		protected Vector2 _newVector4;

		public override bool HasRandomness => false;

		public override bool HasCustomInspectors => false;

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

		protected override void AutomateTargetAcquisition()
		{
		}

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		[IteratorStateMachine(typeof(_003CInterpolationSequence_003Ed__40))]
		protected virtual IEnumerator InterpolationSequence(float intensityMultiplier)
		{
			return null;
		}

		protected virtual void SetValueAtTime(float t, float intensityMultiplier)
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
