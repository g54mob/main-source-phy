using System;
using UnityEngine;

namespace MoreMountains.Tools
{
	[Serializable]
	public class MMPlaylistSong
	{
		public AudioSource TargetAudioSource;

		[MMVector(new string[] { "Min", "Max" })]
		public Vector2 Volume;

		[MMVector(new string[] { "RMin", "RMax" })]
		public Vector2 InitialDelay;

		[MMVector(new string[] { "RMin", "RMax" })]
		public Vector2 CrossFadeDuration;

		[MMVector(new string[] { "RMin", "RMax" })]
		public Vector2 Pitch;

		[Range(-1f, 1f)]
		public float StereoPan;

		[Range(0f, 1f)]
		public float SpatialBlend;

		public bool Loop;

		[MMReadOnly]
		public bool Playing;

		[MMReadOnly]
		public bool Fading;

		[MMHidden]
		public bool _initialized;

		public virtual void Initialization()
		{
		}
	}
}
