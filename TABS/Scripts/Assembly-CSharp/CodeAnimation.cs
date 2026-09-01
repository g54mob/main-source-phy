using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CodeAnimation : MonoBehaviour, GameObjectPooling.IPoolable
{
	public float animationSpeed = 1f;

	public float randomSpeed;

	public bool loopIn;

	public bool playInOnAwake;

	public bool loopBoop;

	public bool playBoopOnAwake;

	private Vector3 defaultScale;

	private Vector3 defaultLocalPosition;

	private Vector3 defaultRectPosition;

	private Vector3 defaultLocalRotation;

	public CodeAnimationInstance[] animations;

	private RectTransform rectTransform;

	public CodeAnimationInstance.AnimationUse currentState;

	[HideInInspector]
	public bool isPlaying;

	[HideInInspector]
	public float animationValue;

	private Action AnimationChangeAction;

	public bool X = true;

	public bool Y = true;

	public bool Z = true;

	public bool useTimeScale = true;

	public bool setFirstFrame = true;

	private CodeAnimationInstance currentAnimation;

	private bool inited;

	public bool IsManagedByPool { get; set; }

	public Action ReleaseSelf { get; set; }

	public bool IsVisible
	{
		get
		{
			if (currentState != CodeAnimationInstance.AnimationUse.In)
			{
				return currentState == CodeAnimationInstance.AnimationUse.Boop;
			}
			return true;
		}
	}

	public bool IsInAndNotPlaying
	{
		get
		{
			if (currentState == CodeAnimationInstance.AnimationUse.In)
			{
				return !isPlaying;
			}
			return false;
		}
	}

	public bool IsInAndPlaying
	{
		get
		{
			if (currentState == CodeAnimationInstance.AnimationUse.In)
			{
				return isPlaying;
			}
			return false;
		}
	}

	public event Action InPlayed;

	public event Action OutPlayed;

	public event Action<CodeAnimationInstance.AnimationUse> AnimationComplete;

	private void Start()
	{
		if (!IsManagedByPool)
		{
			Init();
		}
	}

	private void Init()
	{
		if (!inited)
		{
			inited = true;
			rectTransform = GetComponent<RectTransform>();
			SetDefaults();
			if (playInOnAwake)
			{
				PlayIn();
			}
			if (loopBoop)
			{
				PlayBoop();
			}
			if (animations != null && animations.Length != 0 && setFirstFrame)
			{
				ApplyValues(animations[0], 0f);
			}
		}
	}

	private void SetDefaults()
	{
		defaultScale = base.transform.localScale;
		defaultLocalPosition = base.transform.localPosition;
		defaultLocalRotation = base.transform.localRotation.eulerAngles;
		if (rectTransform != null)
		{
			defaultRectPosition = rectTransform.anchoredPosition;
		}
	}

	public void Animate(CodeAnimationInstance.AnimationUse use)
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		Init();
		currentState = use;
		StopAllCoroutines();
		for (int i = 0; i < animations.Length; i++)
		{
			if (animations[i].animationUse == use)
			{
				StartCoroutine(PlayAnimations(animations[i]));
			}
		}
	}

	public void PlayIn()
	{
		Animate(CodeAnimationInstance.AnimationUse.In);
		this.InPlayed?.Invoke();
	}

	public void PlayOut()
	{
		Animate(CodeAnimationInstance.AnimationUse.Out);
		this.OutPlayed?.Invoke();
	}

	public void PlayBoop()
	{
		Animate(CodeAnimationInstance.AnimationUse.Boop);
	}

	public void FinishAnimation()
	{
		StartCoroutine(WaitAFrame(animationSpeed));
		animationSpeed = float.PositiveInfinity;
		IEnumerator WaitAFrame(float previousSpeed)
		{
			yield return null;
			animationSpeed = previousSpeed;
		}
	}

	private IEnumerator PlayAnimations(CodeAnimationInstance animation)
	{
		currentAnimation = animation;
		if ((double)Time.deltaTime > 0.1)
		{
			yield return null;
		}
		isPlaying = true;
		animation.startEvent.Invoke();
		if (animation.randomMultiplier != 0f)
		{
			animation.multiplier += UnityEngine.Random.Range(0f - animation.randomMultiplier, animation.randomMultiplier);
		}
		if (randomSpeed != 0f)
		{
			animationSpeed += UnityEngine.Random.Range(0f - randomSpeed, randomSpeed);
		}
		StartCoroutine(DelayTimedEvent(animation.eventTiming / animationSpeed, animation.timedEvent));
		float t = animation.curve[animation.curve.length - 1].time;
		float c = 0f;
		while (c < t)
		{
			ApplyValues(animation, c);
			c += (useTimeScale ? Time.deltaTime : Time.unscaledDeltaTime) * animationSpeed;
			yield return null;
		}
		ApplyValues(animation, t);
		animation.endEvent.Invoke();
		this.AnimationComplete?.Invoke(currentState);
		isPlaying = false;
		if (loopIn)
		{
			PlayIn();
		}
		if (loopBoop)
		{
			PlayBoop();
		}
	}

	public void SetTime(float t)
	{
		StopAllCoroutines();
		if (currentAnimation != null)
		{
			ApplyValues(currentAnimation, currentAnimation.curve[currentAnimation.curve.length - 1].time * t);
		}
	}

	private void ApplyValues(CodeAnimationInstance animation, float time)
	{
		if (animation.animationType == CodeAnimationInstance.AnimationType.rectPosition && rectTransform != null)
		{
			rectTransform.anchoredPosition = defaultRectPosition + animation.direction * animation.curve.Evaluate(time) * animation.multiplier;
		}
		if (animation.animationType == CodeAnimationInstance.AnimationType.position)
		{
			base.transform.localPosition = defaultLocalPosition + animation.direction * animation.curve.Evaluate(time) * animation.multiplier;
		}
		if (animation.animationType == CodeAnimationInstance.AnimationType.scale)
		{
			Vector3 vector = defaultScale * animation.curve.Evaluate(time) * animation.multiplier;
			base.transform.localScale = new Vector3(X ? vector.x : base.transform.localScale.x, Y ? vector.y : base.transform.localScale.y, Z ? vector.z : base.transform.localScale.z);
		}
		if (animation.animationType == CodeAnimationInstance.AnimationType.floatNumber)
		{
			animationValue = animation.curve.Evaluate(time) * animation.multiplier;
		}
		if (animation.animationType == CodeAnimationInstance.AnimationType.rotation)
		{
			base.transform.localRotation = Quaternion.Euler(animation.curve.Evaluate(time) * animation.direction + defaultLocalRotation);
		}
		if (AnimationChangeAction != null)
		{
			AnimationChangeAction();
		}
	}

	public void AddAnimationChangeAction(Action action)
	{
		AnimationChangeAction = (Action)Delegate.Combine(AnimationChangeAction, action);
	}

	private IEnumerator DelayTimedEvent(float time, UnityEvent eventToCall)
	{
		yield return new WaitForSeconds(time);
		eventToCall.Invoke();
	}

	internal void EnterState(CodeAnimationInstance.AnimationUse newState)
	{
		if (currentState != newState)
		{
			Animate(newState);
		}
	}

	public void StopAnimations()
	{
		loopIn = false;
		StopAllCoroutines();
	}

	public void Initialize()
	{
		Init();
	}

	public void Reset()
	{
	}

	public void Release()
	{
		StopAllCoroutines();
		Transform transform = base.transform;
		if (transform != null)
		{
			transform.localScale = defaultScale;
			transform.localPosition = defaultLocalPosition;
			transform.localRotation = Quaternion.Euler(defaultLocalRotation);
		}
		if (rectTransform != null)
		{
			rectTransform.anchoredPosition = defaultRectPosition;
		}
		inited = false;
	}
}
