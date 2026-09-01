using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/Renderer/MMSpriteRendererShaker")]
	[RequireComponent(typeof(SpriteRenderer))]
	public class MMSpriteRendererShaker : MMShaker
	{
		[MMInspectorGroup("SpriteRenderer", true, 39, false)]
		[Tooltip("the SpriteRenderer to affect when playing the feedback")]
		public SpriteRenderer BoundSpriteRenderer;

		[Tooltip("whether or not that SpriteRenderer should be turned off on start")]
		public bool StartsOff;

		[MMInspectorGroup("Color", true, 40, false)]
		[Tooltip("whether or not this shaker should modify color")]
		public bool ModifyColor;

		[Tooltip("the colors to apply to the SpriteRenderer over time")]
		public Gradient ColorOverTime;

		[MMInspectorGroup("Flip", true, 41, false)]
		[Tooltip("whether or not to flip the sprite on X")]
		public bool FlipX;

		[Tooltip("whether or not to flip the sprite on Y")]
		public bool FlipY;

		protected Color _initialColor;

		protected bool _originalModifyColor;

		protected float _originalShakeDuration;

		protected Gradient _originalColorOverTime;

		protected bool _originalFlipX;

		protected bool _originalFlipY;

		protected override void Initialization()
		{
		}

		protected virtual void Reset()
		{
		}

		protected override void Shake()
		{
		}

		protected override void GrabInitialValues()
		{
		}

		protected override void ResetTargetValues()
		{
		}

		protected override void ResetShakerValues()
		{
		}

		public override void StartListening()
		{
		}

		public override void StopListening()
		{
		}

		public virtual void OnMMSpriteRendererShakeEvent(float shakeDuration, bool modifyColor, Gradient colorOverTime, bool flipX, bool flipY, float feedbacksIntensity = 1f, MMChannelData channelData = null, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true, bool useRange = false, float eventRange = 0f, Vector3 eventOriginPosition = default(Vector3))
		{
		}
	}
}
