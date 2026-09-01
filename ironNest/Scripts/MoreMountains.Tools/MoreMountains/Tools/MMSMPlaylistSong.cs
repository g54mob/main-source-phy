using System;
using UnityEngine;

namespace MoreMountains.Tools
{
	[Serializable]
	public class MMSMPlaylistSong
	{
		[Tooltip("the name of the song, used only for organizational purposes in the inspector")]
		public string Name;

		[Tooltip("the clip to play when this song plays")]
		public AudioClip Clip;

		[Tooltip("the amount of time this song's been played")]
		[MMReadOnly]
		public int PlayCount;

		[Tooltip("the many options to control this song")]
		public MMSoundManagerPlayOptions Options;

		public virtual void Initialization()
		{
		}
	}
}
