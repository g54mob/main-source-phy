using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Camera/MMPostProcessingMovingFilter")]
	public class MMPostProcessingMovingFilter : MonoBehaviour
	{
		public enum TimeScales
		{
			Unscaled = 0,
			Scaled = 1
		}

		[Header("Settings")]
		public int Channel;

		public TimeScales TimeScale;

		public MMTweenType Curve;

		public bool Active;

		[MMVector(new string[] { "On", "Off" })]
		public Vector2 FilterOffset;

		public bool AddToInitialPosition;

		[Header("Tests")]
		public float TestDuration;

		[MMInspectorButton("PostProcessingToggle")]
		public bool PostProcessingToggleButton;

		[MMInspectorButton("PostProcessingTriggerOff")]
		public bool PostProcessingTriggerOffButton;

		[MMInspectorButton("PostProcessingTriggerOn")]
		public bool PostProcessingTriggerOnButton;

		protected bool _lastReachedState;

		protected float _duration;

		protected float _lastMovementStartedAt;

		protected Vector3 _initialPosition;

		protected Vector3 _positionToRestore;

		protected Vector3 _newPosition;

		protected virtual void Start()
		{
		}

		protected virtual void Initialization()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void MoveTowardsCurrentTarget()
		{
		}

		public virtual void RestoreInitialPosition()
		{
		}

		public virtual void OnMMPostProcessingMovingFilterEvent(MMTweenType curve, bool active, bool toggle, float duration, int channel = 0, bool stop = false, bool restore = false)
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}

		protected virtual void PostProcessingToggle()
		{
		}

		protected virtual void PostProcessingTriggerOff()
		{
		}

		protected virtual void PostProcessingTriggerOn()
		{
		}
	}
}
