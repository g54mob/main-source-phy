using System.Collections;
using UnityEngine;

namespace Landfall.TABC
{
	public class CodeAnimation : MonoBehaviour
	{
		public CodeAnimationInstance[] animations;

		[HideInInspector]
		public Vector3 startScale;

		[HideInInspector]
		public Vector3 startLocalPos;

		[HideInInspector]
		public Vector3 startAnchoredPos;

		[HideInInspector]
		public Vector3 startRotation;

		[HideInInspector]
		public CodeAnimationUse currentState = CodeAnimationUse.Out;

		private RectTransform rectTransform;

		public bool X = true;

		public bool Y = true;

		public bool Z = true;

		public bool useTimeScale = true;

		public float speedMultiplier = 1f;

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

		public void PlayIn()
		{
			if (base.gameObject.activeInHierarchy)
			{
				PlayAnimationWithUse(CodeAnimationUse.In);
			}
		}

		public void PlayOut()
		{
			if (base.gameObject.activeInHierarchy)
			{
				PlayAnimationWithUse(CodeAnimationUse.Out);
			}
		}

		public void PlayBoop()
		{
			if (base.gameObject.activeInHierarchy)
			{
				PlayAnimationWithUse(CodeAnimationUse.Boop);
			}
		}

		public void Stop()
		{
			StopAllCoroutines();
		}

		private void ResetAnimationState()
		{
			ApplyAnimationFrame(GetAnimationWithUse(CodeAnimationUse.In), 0f);
		}

		private CodeAnimationInstance GetAnimationWithUse(CodeAnimationUse use)
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

		public void PlayAnimation(CodeAnimationInstance animation)
		{
			if (animation.isPlaying && animation.animation != null)
			{
				StopCoroutine(animation.animation);
			}
			animation.animation = StartCoroutine(DoAnimation(animation));
		}

		public void PlayAnimationWithUse(CodeAnimationUse animationUse)
		{
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

		private IEnumerator DoAnimation(CodeAnimationInstance animation)
		{
			animation.statEvent.Invoke();
			animation.isPlaying = true;
			float c = 0f;
			float t = animation.Curve()[animation.Curve().length - 1].time;
			while (c < t)
			{
				c += (useTimeScale ? Time.deltaTime : Time.unscaledDeltaTime) * animation.speed * speedMultiplier;
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

		private void ApplyAnimationFrame(CodeAnimationInstance anim, float time)
		{
			if (anim.animationType == CodeAnimationType.Scale)
			{
				Vector3 vector = startScale * anim.Curve().Evaluate(time) * anim.multiplier;
				Vector3 localScale = new Vector3(X ? vector.x : base.transform.localScale.x, Y ? vector.y : base.transform.localScale.y, Z ? vector.z : base.transform.localScale.z);
				base.transform.localScale = localScale;
			}
			else if (anim.animationType == CodeAnimationType.Position)
			{
				base.transform.localPosition = startLocalPos + anim.animDirection * anim.Curve().Evaluate(time) * anim.multiplier;
			}
			else if (anim.animationType == CodeAnimationType.RectPosition)
			{
				rectTransform.anchoredPosition = startAnchoredPos + anim.animDirection * anim.Curve().Evaluate(time) * anim.multiplier;
			}
			else if (anim.animationType == CodeAnimationType.Rotation)
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

		internal void EnterState(CodeAnimationUse newState)
		{
			if (currentState != newState)
			{
				PlayAnimationWithUse(newState);
			}
		}
	}
}
