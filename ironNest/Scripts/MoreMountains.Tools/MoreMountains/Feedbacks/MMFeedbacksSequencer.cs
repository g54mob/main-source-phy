using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Feedbacks/Sequencing/MMFeedbacksSequencer")]
	public class MMFeedbacksSequencer : MMSequencer
	{
		[Tooltip("the list of audio clips to play (one per track)")]
		public List<MMFeedbacks> Feedbacks;

		protected override void OnBeat()
		{
		}

		public override void PlayTrackEvent(int index)
		{
		}

		public override void EditorMaintenance()
		{
		}

		public virtual void SetupFeedbacks()
		{
		}
	}
}
