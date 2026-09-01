using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[RequireComponent(typeof(MMFeedbacks))]
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/Feedbacks/MMFeedbacksShaker")]
	public class MMFeedbacksShaker : MMShaker
	{
		protected MMFeedbacks _mmFeedbacks;

		protected override void Initialization()
		{
		}

		public virtual void OnMMFeedbacksShakeEvent(MMChannelData channelData = null, bool useRange = false, float eventRange = 0f, Vector3 eventOriginPosition = default(Vector3))
		{
		}

		protected override void ShakeStarts()
		{
		}

		protected virtual void Reset()
		{
		}

		public override void StartListening()
		{
		}

		public override void StopListening()
		{
		}
	}
}
