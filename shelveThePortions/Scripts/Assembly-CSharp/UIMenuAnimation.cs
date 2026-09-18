using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuAnimation : Singleton<UIMenuAnimation>
{
	private CanvasGroup currentCanvasGroup;

	private CanvasGroup currentActivatedPanelCG;

	[HideInInspector]
	public bool isAnimationRunning;

	protected override void Awake()
	{
		base.Awake();
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		isAnimationRunning = false;
		currentCanvasGroup = null;
	}

	public void ShowMenu(CanvasGroup canvasGroup, Action OnComplete = null)
	{
		if (isAnimationRunning)
		{
			return;
		}
		isAnimationRunning = true;
		Singleton<CurserManager>.Instance.SetCurser(CurserTypes.Normal);
		if (currentCanvasGroup == null)
		{
			currentCanvasGroup = canvasGroup;
			currentCanvasGroup.gameObject.SetActive(value: true);
			SetCanvasGroup(currentCanvasGroup, flag: false);
			TweenController.DOFloat(canvasGroup.gameObject, 0f, 1f, TweenDuration.Super_Short, delegate(float f)
			{
				if (currentCanvasGroup != null)
				{
					currentCanvasGroup.alpha = f;
				}
			}, delegate
			{
				isAnimationRunning = false;
				SetCanvasGroup(currentCanvasGroup, flag: true);
				if (OnComplete != null)
				{
					OnComplete();
				}
			}, ignoreTimeScale: true);
			return;
		}
		currentCanvasGroup.interactable = false;
		currentCanvasGroup.blocksRaycasts = false;
		TweenController.DOFloat(currentCanvasGroup.gameObject, 1f, 0f, TweenDuration.Super_Short, delegate(float f)
		{
			if (currentCanvasGroup != null)
			{
				currentCanvasGroup.alpha = f;
			}
		}, delegate
		{
			TweenController.DOFloat(canvasGroup.gameObject, 0f, 1f, TweenDuration.Super_Short, delegate(float f)
			{
				if (canvasGroup != null)
				{
					canvasGroup.alpha = f;
				}
			}, delegate
			{
				currentCanvasGroup.gameObject.SetActive(value: false);
				currentCanvasGroup = canvasGroup;
				SetCanvasGroup(canvasGroup, flag: true);
				if (OnComplete != null)
				{
					OnComplete();
				}
				isAnimationRunning = false;
			}, ignoreTimeScale: true);
		}, ignoreTimeScale: true);
	}

	public void SetCurrnetActivatedPanelCG(CanvasGroup currentActivatedPanelCG)
	{
		this.currentActivatedPanelCG = currentActivatedPanelCG;
		SetCanvasGroup(currentActivatedPanelCG, flag: true);
	}

	public void HideCurrentMenu(Action OnComplete = null)
	{
		if (isAnimationRunning || currentCanvasGroup == null)
		{
			return;
		}
		isAnimationRunning = true;
		SetCanvasGroup(currentCanvasGroup, flag: false);
		TweenController.DOFloat(currentCanvasGroup.gameObject, 1f, 0f, TweenDuration.Super_Short, delegate(float f)
		{
			if (currentCanvasGroup != null)
			{
				currentCanvasGroup.alpha = f;
			}
		}, delegate
		{
			isAnimationRunning = false;
			currentCanvasGroup = null;
			OnComplete?.Invoke();
		}, ignoreTimeScale: true);
	}

	public void ShowCanvasGroup(CanvasGroup currentCanvasGroup, GameObject selectedButton = null, Action OnComplete = null)
	{
		Singleton<CurserManager>.Instance.SetCurser(CurserTypes.Normal);
		if (currentCanvasGroup.alpha == 1f)
		{
			SetCanvasGroup(currentCanvasGroup, flag: true);
			OnComplete?.Invoke();
			return;
		}
		SetCanvasGroup(currentCanvasGroup, flag: false);
		TweenController.DOFloat(currentCanvasGroup.gameObject, 0f, 1f, TweenDuration.Instant, delegate(float f)
		{
			if (currentCanvasGroup != null)
			{
				currentCanvasGroup.alpha = f;
			}
		}, delegate
		{
			SetCanvasGroup(currentCanvasGroup, flag: true);
			OnComplete?.Invoke();
		}, ignoreTimeScale: true);
	}

	public void HideCanvasGroup(CanvasGroup currentCanvasGroup, Action OnComplete = null)
	{
		if (currentCanvasGroup.alpha == 0f)
		{
			OnComplete?.Invoke();
			return;
		}
		SetCanvasGroup(currentCanvasGroup, flag: false);
		TweenController.DOFloat(currentCanvasGroup.gameObject, 1f, 0f, TweenDuration.Instant, delegate(float f)
		{
			if (currentCanvasGroup != null)
			{
				currentCanvasGroup.alpha = f;
			}
		}, delegate
		{
			OnComplete?.Invoke();
		}, ignoreTimeScale: true);
	}

	private void SetCanvasGroup(CanvasGroup canvasGroup, bool flag)
	{
		canvasGroup.blocksRaycasts = flag;
		canvasGroup.interactable = flag;
	}
}
