using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Feedbacks/Sequencing/MMSoundSequencer")]
	public class MMSoundSequencer : MMSequencer
	{
		[Tooltip("the list of audio clips to play (one per track)")]
		public List<AudioClip> Sounds;

		protected List<AudioSource> _audioSources;

		protected override void Initialization()
		{
		}

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
