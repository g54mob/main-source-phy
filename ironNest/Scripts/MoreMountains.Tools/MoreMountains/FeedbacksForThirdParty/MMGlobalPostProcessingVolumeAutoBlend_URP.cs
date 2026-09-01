using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Rendering;

namespace MoreMountains.FeedbacksForThirdParty
{
	[RequireComponent(typeof(Volume))]
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/PostProcessing/MMGlobalPostProcessingVolumeAutoBlend_URP")]
	public class MMGlobalPostProcessingVolumeAutoBlend_URP : MonoBehaviour
	{
		public enum TimeScales
		{
			Scaled = 0,
			Unscaled = 1
		}

		public enum BlendTriggerModes
		{
			OnEnable = 0,
			Script = 1
		}

		[Header("Blend")]
		public BlendTriggerModes BlendTriggerMode;

		public float BlendDuration;

		public AnimationCurve Curve;

		[Header("Weight")]
		[Range(0f, 1f)]
		public float InitialWeight;

		[Range(0f, 1f)]
		public float FinalWeight;

		[Header("Behaviour")]
		public TimeScales TimeScale;

		public bool DisableVolumeOnZeroWeight;

		public bool DisableSelfAfterEnd;

		public bool Interruptable;

		public bool StartFromCurrentValue;

		[Header("Tests")]
		[MMFInspectorButton("Blend")]
		public bool TestBlend;

		[MMFInspectorButton("BlendBack")]
		public bool TestBlendBackwards;

		protected float _initial;

		protected float _destination;

		protected float _startTime;

		protected bool _blending;

		protected Volume _volume;

		protected float GetTime()
		{
			return 0f;
		}

		protected virtual void Awake()
		{
		}

		protected virtual void OnEnable()
		{
		}

		public virtual void Blend()
		{
		}

		public virtual void BlendBack()
		{
		}

		protected virtual void StartBlending()
		{
		}

		public virtual void StopBlending()
		{
		}

		protected virtual void Update()
		{
		}

		public virtual void RestoreInitialValues()
		{
		}
	}
}
