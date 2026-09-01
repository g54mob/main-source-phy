using System;
using UnityEngine;
using UnityEngine.Events;

namespace TABSCredits
{
	[Serializable]
	public class CurveAnimationInstance
	{
		public CurveAnimationType animationType;

		public CurveAnimationUse animationUse;

		public AnimationCurve inCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

		public AnimationCurve outCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);

		public AnimationCurve boopCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);

		public Vector3 animDirection;

		public bool loop;

		public bool playOnAwake;

		public float speed = 1f;

		public float multiplier = 1f;

		public UnityEvent statEvent;

		public UnityEvent endEvent;

		public UnityEvent delayedEvent;

		public float delay;

		public bool isPlaying;

		[HideInInspector]
		public Coroutine animation;

		public AnimationCurve Curve()
		{
			if (animationUse == CurveAnimationUse.Boop)
			{
				return boopCurve;
			}
			if (animationUse != CurveAnimationUse.In)
			{
				return outCurve;
			}
			return inCurve;
		}
	}
}
