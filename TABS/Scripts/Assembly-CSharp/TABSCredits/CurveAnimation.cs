using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace TABSCredits
{
	public class CurveAnimation : MonoBehaviour
	{
		public CurveAnimationInstance[] animations;

		[HideInInspector]
		public Vector3 startScale;

		[HideInInspector]
		public Vector3 startLocalPos;

		[HideInInspector]
		public Vector3 startAnchoredPos;

		[HideInInspector]
		public Vector3 startRotation;

		[HideInInspector]
		public CurveAnimationUse currentState = CurveAnimationUse.Out;

		private RectTransform rectTransform;

		public bool useTimeScale = true;

		public bool X = true;

		public bool Y = true;

		public bool Z = true;

		public bool setFirstFrame;

		public bool stopAllAnimations;

		private bool inited;

		private void Init()
		{
			if (!inited)
			{
				startScale = base.transform.localScale;
				startLocalPos = base.transform.localPosition;
				rectTransform = GetComponent<RectTransform>();
				if ((bool)rectTransform)
				{
					startAnchoredPos = rectTransform.anchoredPosition;
				}
				startRotation = base.transform.localEulerAngles;
				if (setFirstFrame)
				{
					ApplyAnimationFrame(animations[0], 0f);
				}
			}
		}

		private void Awake()
		{
			Init();
		}

		private void OnDisable()
		{
			StopAllCoroutines();
			ResetAnimationState();
		}

		private void OnEnable()
		{
			ResetAnimationState();
			for (int i = 0; i < animations.Length; i++)
			{
				if (animations[i].playOnAwake)
				{
					PlayAnimation(animations[i]);
				}
			}
		}

		public void StopLooping()
		{
			for (int i = 0; i < animations.Length; i++)
			{
				animations[i].loop = false;
			}
		}

		public void PlayIn()
		{
			PlayAnimationWithUse(CurveAnimationUse.In);
		}

		public void PlayOut()
		{
			PlayAnimationWithUse(CurveAnimationUse.Out);
		}

		public void PlayBoop()
		{
			PlayAnimationWithUse(CurveAnimationUse.Boop);
		}

		public void Stop()
		{
			StopAllCoroutines();
		}

		private void ResetAnimationState()
		{
			ApplyAnimationFrame(GetAnimationWithUse(CurveAnimationUse.In), 0f);
		}

		private CurveAnimationInstance GetAnimationWithUse(CurveAnimationUse use)
		{
			for (int i = 0; i < animations.Length; i++)
			{
				if (animations[i].animationUse == use)
				{
					return animations[i];
				}
			}
			return animations[0];
		}

		public void PlayAnimation(CurveAnimationInstance animation)
		{
			if (stopAllAnimations)
			{
				StopAllCoroutines();
			}
			if (animation.isPlaying && animation.animation != null)
			{
				StopCoroutine(animation.animation);
			}
			animation.animation = StartCoroutine(DoAnimation(animation));
		}

		public void PlayAnimationWithUse(CurveAnimationUse animationUse)
		{
			if (stopAllAnimations)
			{
				StopAllCoroutines();
			}
			currentState = animationUse;
			for (int i = 0; i < animations.Length; i++)
			{
				if (animations[i].animationUse == animationUse)
				{
					if (animations[i].isPlaying && animations[i].animation != null)
					{
						StopCoroutine(animations[i].animation);
					}
					animations[i].animation = StartCoroutine(DoAnimation(animations[i]));
				}
			}
		}

		private IEnumerator DoAnimation(CurveAnimationInstance animation)
		{
			StartCoroutine(DelayEvent(animation.delay / animation.speed, animation.delayedEvent));
			animation.statEvent.Invoke();
			animation.isPlaying = true;
			float c = 0f;
			float t = animation.Curve()[animation.Curve().length - 1].time;
			while (c < t)
			{
				c += (useTimeScale ? (Time.deltaTime * animation.speed) : (Time.unscaledDeltaTime * animation.speed));
				ApplyAnimationFrame(animation, c);
				yield return null;
			}
			ApplyAnimationFrame(animation, t);
			animation.isPlaying = false;
			animation.endEvent.Invoke();
			if (animation.loop)
			{
				PlayAnimationWithUse(animation.animationUse);
			}
		}

		private IEnumerator DelayEvent(float seconds, UnityEvent eventToCall)
		{
			yield return new WaitForSeconds(seconds);
			eventToCall.Invoke();
		}

		private void ApplyAnimationFrame(CurveAnimationInstance anim, float time)
		{
			if (anim.animationType == CurveAnimationType.Scale)
			{
				Vector3 vector = startScale * anim.Curve().Evaluate(time) * anim.multiplier;
				Vector3 localScale = new Vector3(X ? vector.x : base.transform.localScale.x, Y ? vector.y : base.transform.localScale.y, Z ? vector.z : base.transform.localScale.z);
				base.transform.localScale = localScale;
			}
			else if (anim.animationType == CurveAnimationType.Position)
			{
				base.transform.localPosition = startLocalPos + anim.animDirection * anim.Curve().Evaluate(time) * anim.multiplier;
			}
			else if (anim.animationType == CurveAnimationType.RectPosition)
			{
				rectTransform.anchoredPosition = startAnchoredPos + anim.animDirection * anim.Curve().Evaluate(time) * anim.multiplier;
			}
			else if (anim.animationType == CurveAnimationType.Rotation)
			{
				base.transform.localEulerAngles = startRotation + anim.animDirection * anim.Curve().Evaluate(time) * anim.multiplier;
			}
		}

		public bool IsPlaying()
		{
			bool result = false;
			for (int i = 0; i < animations.Length; i++)
			{
				if (animations[i].isPlaying)
				{
					result = true;
				}
			}
			return result;
		}
	}
}
