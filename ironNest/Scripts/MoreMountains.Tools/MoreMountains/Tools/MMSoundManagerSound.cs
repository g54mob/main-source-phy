using System;
using UnityEngine;

namespace MoreMountains.Tools
{
	[Serializable]
	public struct MMSoundManagerSound
	{
		public int ID;

		public MMSoundManager.MMSoundManagerTracks Track;

		public AudioSource Source;

		public bool Persistent;

		public float PlaybackTime;

		public float PlaybackDuration;
	}
}
