using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Animation/MMStopMotionAnimation")]
	public class MMStopMotionAnimation : MonoBehaviour
	{
		public enum FramerateModes
		{
			Manual = 0,
			Automatic = 1
		}

		[Header("General Settings")]
		public bool StopMotionEnabled;

		public int AnimationLayerID;

		[Header("Framerate")]
		public FramerateModes FramerateMode;

		[MMEnumCondition("FramerateMode", new int[] { 1 })]
		public float FramesPerSecond;

		[MMEnumCondition("FramerateMode", new int[] { 1 })]
		public float PollFrequency;

		[MMEnumCondition("FramerateMode", new int[] { 0 })]
		public float ManualTimeBetweenFrames;

		[MMEnumCondition("FramerateMode", new int[] { 0 })]
		public float ManualAnimatorSpeed;

		public float timet;

		protected float _currentClipFPS;

		protected float _currentClipLength;

		protected float _lastPollAt;

		protected Animator _animator;

		protected AnimationClip _currentClip;

		protected virtual void Awake()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void StopMotion()
		{
		}

		protected virtual void Poll()
		{
		}
	}
}
