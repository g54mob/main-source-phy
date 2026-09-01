using System;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[Serializable]
	public class MMSequenceTrack
	{
		public int ID;

		public Color TrackColor;

		public KeyCode Key;

		public bool Active;

		[MMFReadOnly]
		public MMSequenceTrackStates State;

		[HideInInspector]
		public bool Initialized;

		public virtual void SetDefaults(int index)
		{
		}
	}
}
