using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GenericUIPopup : SingleInstanceFindOnly<GenericUIPopup>
{
	public enum PopupPosition
	{
		Top = 0,
		Bottom = 1
	}

	private const string ITweenMoveName = "GenericUIPopupMove";

	[SerializeField]
	private float duration = 0.8f;

	[SerializeField]
	private bool playAudio;

	[SerializeField]
	private Vector3 lerpPosDirection = new Vector3(-200f, 0f);

	[SerializeField]
	private float fadeDuration = 0.15f;

	[SerializeField]
	private Text messageText;

	[SerializeField]
	private Image backgroundImage;

	[SerializeField]
	private Image iconImage;

	private Vector3 backgroundStartPosition;

	private IEnumerator hideCoroutine;

	public override string Name
	{
		get
		{
			return "GenericUIPopup";
		}
	}

	public float DefaultDuration { get; private set; }

	public float DefaultFadeDuration { get; private set; }

	public bool DefaultPlayAudio { get; private set; }

	protected override void Awake()
	{
		base.Awake();
		backgroundStartPosition = backgroundImage.transform.localPosition;
		DefaultDuration = duration;
		DefaultFadeDuration = fadeDuration;
		DefaultPlayAudio = playAudio;
	}

	public void Show(string text)
	{
		Show(text, DefaultDuration, DefaultFadeDuration, DefaultPlayAudio, PopupPosition.Top);
	}

	public void Show(string text, float duration)
	{
		Show(text, duration, PopupPosition.Top);
	}

	public void Show(string text, PopupPosition position)
	{
		Show(text, DefaultDuration, DefaultFadeDuration, DefaultPlayAudio, position);
	}

	public void Show(string text, float duration, PopupPosition position)
	{
		Show(text, duration, DefaultFadeDuration, DefaultPlayAudio, position);
	}

	public void Show(string text, float duration, float fadeDuration, bool playAudio, PopupPosition position)
	{
		if (messageText == null)
		{
			Debug.LogError("Showing GenericUIPopup, but messageText is null (text='" + text + "')!");
			return;
		}
		messageText.text = text;
		this.duration = duration;
		this.fadeDuration = fadeDuration;
		this.playAudio = playAudio;
		SetPosition(position);
		Show();
	}

	private void SetPosition(PopupPosition position)
	{
		RectTransform component = base.gameObject.GetComponent<RectTransform>();
		Vector2 anchoredPosition = component.anchoredPosition;
		if (position == PopupPosition.Top)
		{
			component.anchorMin = new Vector2(0.5f, 1f);
			component.anchorMax = new Vector2(0.5f, 1f);
			component.pivot = new Vector2(0.5f, 1f);
			anchoredPosition.y = -200f;
		}
		else
		{
			component.anchorMin = new Vector2(0.5f, 0f);
			component.anchorMax = new Vector2(0.5f, 0f);
			component.pivot = new Vector2(0.5f, 1f);
			anchoredPosition.y = 350f;
		}
		component.anchoredPosition = anchoredPosition;
	}

	private void Show()
	{
		if (base.gameObject.activeInHierarchy)
		{
			if (hideCoroutine != null)
			{
				StopCoroutine(hideCoroutine);
			}
			FadeIn();
			MoveIn();
			if (playAudio)
			{
				GetComponent<AudioSource>().Play();
			}
			hideCoroutine = IEHide();
			StartCoroutine(hideCoroutine);
		}
	}

	private IEnumerator IEHide()
	{
		float currentDuration = 0f;
		while (currentDuration < duration)
		{
			currentDuration += TimeSlider.Instance.deltaTime;
			yield return 0;
		}
		FadeOut();
		MoveOut();
	}

	private void MoveIn()
	{
		iTween.StopByName("GenericUIPopupMove");
		Vector3 localPosition = backgroundStartPosition - lerpPosDirection;
		if (backgroundImage.transform.localPosition == backgroundStartPosition)
		{
			backgroundImage.transform.localPosition = localPosition;
		}
		TweenBackgroundTo(backgroundStartPosition);
	}

	private void MoveOut()
	{
		iTween.StopByName("GenericUIPopupMove");
		TweenBackgroundTo(backgroundStartPosition - lerpPosDirection);
	}

	private void TweenBackgroundTo(Vector3 localPosition)
	{
		iTween.MoveTo(backgroundImage.gameObject, iTween.Hash("position", localPosition, "islocal", true, "time", fadeDuration, "name", "GenericUIPopupMove", "ignoretimescale", true));
	}

	private void FadeIn()
	{
		CrossFadeAlpha(1f, fadeDuration);
	}

	private void FadeOut()
	{
		CrossFadeAlpha(0f, fadeDuration);
	}

	private void SetAlpha(float alpha)
	{
		if (messageText == null || messageText.canvasRenderer == null)
		{
			Debug.LogWarning("Setting alpha, but messageText or messageText.canvasRenderer is null!");
			return;
		}
		messageText.canvasRenderer.SetAlpha(alpha);
		backgroundImage.canvasRenderer.SetAlpha(alpha);
		iconImage.canvasRenderer.SetAlpha(alpha);
	}

	private void CrossFadeAlpha(float alpha, float duration)
	{
		messageText.CrossFadeAlpha(alpha, duration, true);
		backgroundImage.CrossFadeAlpha(alpha, duration, true);
		iconImage.CrossFadeAlpha(alpha, duration, true);
	}

	private void OnEnable()
	{
		SetAlpha(0f);
	}

	private void OnDisable()
	{
		SetAlpha(0f);
	}
}
