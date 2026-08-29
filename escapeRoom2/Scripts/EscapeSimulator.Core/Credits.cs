using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class Credits : MonoBehaviour
{
	private enum CreditsState
	{
		FadeInBackground = 0,
		AnimateDividerLine = 1,
		FadeInTitleAndSkipButton = 2,
		WaitBeforeFadeInLogo = 3,
		FadeInLogo = 4,
		WaitBeforeFadeInCredits = 5,
		FadeInCredits = 6,
		MoveCredits = 7,
		StartCreditsExit = 8,
		FadeOutBackground = 9,
		CreditsExited = 10
	}

	public Menu menu;

	public EventSystem eventSystem;

	public Image creditsFader;

	public RectTransform creditsDivider;

	public Text creditsTitle;

	public Image pineStudioLogo;

	public Sprite[] creditsImages;

	public CanvasGroup creditsTextGroup;

	public RectTransform creditsScroll;

	public Image creditSkipFillIcon;

	public Text creditSkipText;

	public RectTransform publisherLogo;

	public CanvasGroup canvasGroup;

	public Image gradient;

	public VideoPlayer mainScreenBackgroundVideo;

	private Vector2 creditsScrollGoalPos;

	private Color vrStartColor;

	private float creditSkipTimer;

	private float currentDelayTime;

	private float creditsExitTime;

	private CreditsState creditsState;

	private Action onCreditsFinished;

	public void showCredits(Action creditsFinished = null)
	{
		onCreditsFinished = creditsFinished;
		CanvasGroup obj = canvasGroup;
		bool blocksRaycasts = (canvasGroup.interactable = true);
		obj.blocksRaycasts = blocksRaycasts;
		creditsFader.gameObject.SetActive(value: true);
		canvasGroup.alpha = 0f;
		creditsDivider.localScale = new Vector3(0f, 1f, 1f);
		creditsTitle.color = new Color(creditsTitle.color.r, creditsTitle.color.g, creditsTitle.color.b, 0f);
		pineStudioLogo.color = new Color(pineStudioLogo.color.r, pineStudioLogo.color.g, pineStudioLogo.color.b, 0f);
		creditsTextGroup.alpha = 0f;
		creditsScroll.anchoredPosition = Vector2.zero;
		creditsScrollGoalPos = Vector2.zero;
		creditsState = CreditsState.FadeInBackground;
	}

	private void Update()
	{
		updateCreditsSkipButton();
		updateCreditsStateMachine();
	}

	private void updateCreditsSkipButton()
	{
		if (creditsState >= CreditsState.FadeInTitleAndSkipButton)
		{
			bool button = Controller.getButton(ControllerButtonActionType.UIConfirmPrimary, ControllerButtonActionType.UIConfirmSecondary, ControllerButtonActionType.UIBack);
			bool flag = Input.GetMouseButton(0) || button;
			bool flag2 = creditsState >= CreditsState.StartCreditsExit;
			creditSkipTimer += Time.deltaTime * (float)((flag && !flag2) ? 1 : (-1));
			creditSkipTimer = Mathf.Clamp(creditSkipTimer, 0f, 1f);
			creditSkipFillIcon.fillAmount = creditSkipTimer;
			if (creditSkipTimer >= 1f)
			{
				exitCredits();
			}
		}
	}

	private void updateCreditsStateMachine()
	{
		EventSystem current = EventSystem.current;
		if (current != null)
		{
			current.SetSelectedGameObject(null);
		}
		if (creditsState >= CreditsState.MoveCredits)
		{
			creditsScrollGoalPos += new Vector2(0f, Time.deltaTime * 75f);
			creditsScroll.anchoredPosition = Vector2.Lerp(creditsScroll.anchoredPosition, creditsScrollGoalPos, Time.deltaTime * 10f);
			if (creditsScroll.anchoredPosition.y > 4700f)
			{
				exitCredits();
			}
		}
		if (creditsState == CreditsState.FadeInBackground)
		{
			canvasGroup.alpha += Time.deltaTime;
			if (canvasGroup.alpha >= 1f)
			{
				if (!Is.Switch)
				{
					mainScreenBackgroundVideo.enabled = false;
				}
				Color color = gradient.color;
				color.a = 1f;
				gradient.color = color;
				creditsState = CreditsState.AnimateDividerLine;
			}
		}
		else if (creditsState == CreditsState.AnimateDividerLine)
		{
			creditsDivider.localScale = Vector3.MoveTowards(creditsDivider.localScale, Vector3.one, Time.deltaTime * 2f);
			if (Vector3.Distance(creditsDivider.localScale, Vector3.one) <= 0.0001f)
			{
				creditsState = CreditsState.FadeInTitleAndSkipButton;
			}
		}
		else if (creditsState == CreditsState.FadeInTitleAndSkipButton)
		{
			Color color2 = creditSkipText.color;
			color2.a += Time.deltaTime;
			creditSkipText.color = color2;
			Color color3 = creditSkipFillIcon.color;
			color3.a += Time.deltaTime;
			creditSkipFillIcon.color = color3;
			Color color4 = creditsTitle.color;
			color4.a += Time.deltaTime;
			creditsTitle.color = color4;
			if (color4.a >= 1f)
			{
				currentDelayTime = 0.25f;
				creditsState = CreditsState.WaitBeforeFadeInLogo;
			}
		}
		else if (creditsState == CreditsState.WaitBeforeFadeInLogo)
		{
			currentDelayTime -= Time.deltaTime;
			if (currentDelayTime <= 0f)
			{
				creditsState = CreditsState.FadeInLogo;
			}
		}
		else if (creditsState == CreditsState.FadeInLogo)
		{
			Color color5 = pineStudioLogo.color;
			color5.a += Time.deltaTime;
			pineStudioLogo.color = color5;
			if (color5.a >= 1f)
			{
				currentDelayTime = 0.25f;
				creditsState = CreditsState.WaitBeforeFadeInCredits;
			}
		}
		else if (creditsState == CreditsState.WaitBeforeFadeInCredits)
		{
			currentDelayTime -= Time.deltaTime;
			if (currentDelayTime <= 0f)
			{
				creditsState = CreditsState.FadeInCredits;
			}
		}
		else if (creditsState == CreditsState.FadeInCredits)
		{
			creditsTextGroup.alpha += Time.deltaTime;
			if (creditsTextGroup.alpha >= 1f)
			{
				creditsState = CreditsState.MoveCredits;
			}
		}
		else if (creditsState == CreditsState.StartCreditsExit)
		{
			float num = Mathf.Clamp01(Time.time - creditsExitTime);
			creditsDivider.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, Interpolation.SmootherStep.evaluate(num));
			creditsTextGroup.alpha = 1f - num;
			Color color6 = creditSkipText.color;
			color6.a = 1f - num;
			creditSkipText.color = color6;
			Color color7 = creditSkipFillIcon.color;
			color7.a = 1f - num;
			creditSkipFillIcon.color = color7;
			Color color8 = creditsTitle.color;
			color8.a = 1f - num;
			creditsTitle.color = color8;
			Color color9 = pineStudioLogo.color;
			color9.a = 1f - num;
			pineStudioLogo.color = color9;
			if (num >= 1f)
			{
				if (!Is.Switch)
				{
					mainScreenBackgroundVideo.enabled = true;
				}
				currentDelayTime = 0.3f;
				creditsState = CreditsState.FadeOutBackground;
			}
		}
		else if (creditsState == CreditsState.FadeOutBackground)
		{
			canvasGroup.alpha -= Time.deltaTime;
			if (canvasGroup.alpha <= 0f)
			{
				CanvasGroup obj = canvasGroup;
				bool interactable = (canvasGroup.blocksRaycasts = false);
				obj.interactable = interactable;
				Color color10 = gradient.color;
				color10.a = 0f;
				gradient.color = color10;
				onCreditsFinished();
				creditsState = CreditsState.CreditsExited;
			}
		}
	}

	public void exitCredits()
	{
		if (creditsState < CreditsState.StartCreditsExit)
		{
			creditsState = CreditsState.StartCreditsExit;
			creditsExitTime = Time.time;
		}
	}

	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
