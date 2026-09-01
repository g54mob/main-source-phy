using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Serialization;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you change the material of the target renderer everytime it's played.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Renderer/Material")]
	public class MMF_Material : MMF_Feedback
	{
		public enum Methods
		{
			Sequential = 0,
			Random = 1
		}

		[CompilerGenerated]
		private sealed class _003CTransitionMaterial_003Ed__30 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_Material _003C_003E4__this;

			public Material originalMaterial;

			public Material newMaterial;

			public int materialIndex;

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
			public _003CTransitionMaterial_003Ed__30(int _003C_003E1__state)
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

		[MMFInspectorGroup("Target Material", true, 61, true, false)]
		[Tooltip("the renderer to change material on")]
		public Renderer TargetRenderer;

		[FormerlySerializedAs("MaterialIndexes")]
		[Tooltip("the list of material indexes we want to change on the target renderer. If left empty, will only target the material at index 0")]
		public int[] RendererMaterialIndexes;

		[MMFInspectorGroup("Material Change", true, 33, false, false)]
		[Tooltip("the selected method")]
		public Methods Method;

		[MMFEnumCondition("Method", new int[] { 0 })]
		[Tooltip("whether or not the sequential order should loop")]
		public bool Loop;

		[MMFEnumCondition("Method", new int[] { 1 })]
		[Tooltip("whether or not to always pick a new material in random mode")]
		public bool AlwaysNewMaterial;

		[Tooltip("the initial index to start with")]
		public int InitialIndex;

		[Tooltip("the list of materials to pick from")]
		public List<Material> Materials;

		[MMFInspectorGroup("Interpolation", true, 35, false, false)]
		public bool InterpolateTransition;

		public float TransitionDuration;

		public AnimationCurve TransitionCurve;

		protected int _currentIndex;

		protected float _startedAt;

		protected Coroutine[] _coroutines;

		protected Material[] _tempMaterials;

		protected Material[] _initialMaterials;

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

		public virtual float GetTime()
		{
			return 0f;
		}

		public virtual float GetDeltaTime()
		{
			return 0f;
		}

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected virtual void InitializeMaterials()
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void ApplyMaterial(Material material)
		{
		}

		protected virtual void LerpMaterial(Material fromMaterial, Material toMaterial, float t, int materialIndex)
		{
		}

		[IteratorStateMachine(typeof(_003CTransitionMaterial_003Ed__30))]
		protected virtual IEnumerator TransitionMaterial(Material originalMaterial, Material newMaterial, int materialIndex)
		{
			return null;
		}

		protected virtual int DetermineNextIndex()
		{
			return 0;
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomRestoreInitialValues()
		{
		}
	}
}
