using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Feedbacks/Sequencing/MMAudioSourceSequencer")]
	public class MMAudioSourceSequencer : MMSequencer
	{
		[Tooltip("the list of audio sources to play (one per track)")]
		public List<AudioSource> AudioSources;

		protected override void OnBeat()
		{
		}

		public override void PlayTrackEvent(int index)
		{
		}

		public override void EditorMaintenance()
		{
		}

		public virtual void SetupSounds()
		{
		}
	}
}
