using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Tutorial/Tutorial Menu")]
public class TutorialMenu : MonoBehaviour
{
	public enum State
	{
		Waiting = 0,
		Opening = 1,
		Open = 2,
		Closing = 3,
		Closed = 4
	}

	public RectTransform rect;

	public State currentState;

	[SerializeField]
	protected RectTransform bgScaler;

	[SerializeField]
	protected Image background;

	[SerializeField]
	protected Image backgroundTail;

	[SerializeField]
	protected Image avatar;

	[SerializeField]
	protected RectTransform content;

	[SerializeField]
	protected RectTransform[] tutorials;

	[SerializeField]
	protected Image next;

	[SerializeField]
	protected Image previous;

	[SerializeField]
	protected Image close;

	[SerializeField]
	protected Text pageNumbers;

	[SerializeField]
	[Tooltip("The lower the value the faster it wil be")]
	protected float verticalStretchScale = 0.8f;

	[SerializeField]
	[Header("Positioning")]
	protected float avatarClosedPos;

	[SerializeField]
	protected float avatarOpenPos;

	[Header("Timing")]
	[SerializeField]
	protected float delayedStart = 1f;

	[SerializeField]
	protected float avatarDuration;

	[SerializeField]
	protected float bubbleDuration;

	[SerializeField]
	protected float contentDuration;

	[Header("Content Values")]
	private RectTransform[] pages;

	private int currentPageNumber;

	private Coroutine animationRoutine;

	private bool isOpen;

	private int currentTutorial;

	private Image[] images;

	private Text[] text;

	private void Awake()
	{
		avatar.rectTransform.anchoredPosition = new Vector2(avatarClosedPos, avatar.rectTransform.anchoredPosition.y);
		bgScaler.localScale = new Vector2(0f, 0f);
		close.gameObject.SetActive(false);
		next.gameObject.SetActive(false);
		previous.gameObject.SetActive(false);
		pageNumbers.gameObject.SetActive(false);
	}

	private void GetTutorialForZone(string zone)
	{
		switch (zone)
		{
		case "1":
			currentTutorial = 1;
			break;
		case "2":
			currentTutorial = 2;
			break;
		case "3":
			currentTutorial = 3;
			break;
		case "5":
			currentTutorial = 4;
			break;
		case "6":
			currentTutorial = 5;
			break;
		case "7":
			currentTutorial = 6;
			break;
		}
	}

	private void OnEnable()
	{
		switch (currentState)
		{
		case State.Waiting:
			StartCoroutine(DelayedOpen(currentTutorial));
			break;
		case State.Opening:
		case State.Open:
			SetOpen(currentTutorial);
			break;
		case State.Closing:
		case State.Closed:
			SetClosed(currentTutorial);
			break;
		}
	}

	private IEnumerator DelayedOpen(int tutorialNumber)
	{
		yield return new WaitForSeconds(delayedStart);
		currentState = State.Opening;
		OpenTutorial(tutorialNumber);
	}

	private void UpdatePageNumber()
	{
		pages[currentPageNumber].gameObject.SetActive(true);
		pageNumbers.text = "PAGE " + (currentPageNumber + 1) + " OF " + pages.Length;
	}

	public void NextPage()
	{
		pages[currentPageNumber].gameObject.SetActive(false);
		currentPageNumber++;
		if (currentPageNumber == pages.Length - 1)
		{
			next.gameObject.SetActive(false);
		}
		if (!previous.gameObject.activeSelf)
		{
			previous.gameObject.SetActive(true);
		}
		UpdatePageNumber();
	}

	public void PreviousPage()
	{
		pages[currentPageNumber].gameObject.SetActive(false);
		currentPageNumber--;
		if (currentPageNumber == 0)
		{
			previous.gameObject.SetActive(false);
		}
		if (!next.gameObject.activeSelf)
		{
			next.gameObject.SetActive(true);
		}
		UpdatePageNumber();
	}

	public void OpenTutorial(int tutorialNumber)
	{
		SetupTutorial(tutorialNumber);
		SetActive(false);
		if (animationRoutine != null)
		{
			StopCoroutine(animationRoutine);
		}
		animationRoutine = StartCoroutine(Animation(true));
	}

	private IEnumerator Animation(bool open)
	{
		if (isOpen != open)
		{
			isOpen = open;
			if (open)
			{
				currentState = State.Opening;
				yield return StartCoroutine(AvatarAnimation(open));
				StartCoroutine(AnimateBGx(open, bubbleDuration * 0.96f));
				yield return StartCoroutine(AnimateBGy(open, bubbleDuration));
				yield return StartCoroutine(ContentFade(open));
				currentState = State.Open;
			}
			else
			{
				currentState = State.Closing;
				yield return StartCoroutine(ContentFade(open));
				SetActive(false);
				StartCoroutine(AnimateBGy(open, bubbleDuration * 0.2f));
				yield return StartCoroutine(AnimateBGx(open, bubbleDuration * 0.2f));
				yield return StartCoroutine(AvatarAnimation(open));
				currentState = State.Closed;
			}
			animationRoutine = null;
		}
	}

	private IEnumerator AvatarAnimation(bool open)
	{
		float start = avatar.rectTransform.anchoredPosition.x;
		float goalPos = ((!open) ? avatarClosedPos : avatarOpenPos);
		float startPct = 1f - Mathf.Abs(start - goalPos) / (avatarOpenPos - avatarClosedPos);
		for (float t = startPct * avatarDuration; t < avatarDuration; t += Time.unscaledDeltaTime)
		{
			avatar.rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(start, goalPos, t / avatarDuration), avatar.rectTransform.anchoredPosition.y);
			yield return null;
		}
		avatar.rectTransform.anchoredPosition = new Vector2(goalPos, avatar.rectTransform.anchoredPosition.y);
	}

	private IEnumerator AnimateBGx(bool open, float duration)
	{
		if (open)
		{
			yield return StartCoroutine(ScaleBGx(0f, 1.04f, duration * 0.65f));
			yield return StartCoroutine(ScaleBGx(1.04f, 0.9f, duration * 0.16f));
			yield return StartCoroutine(ScaleBGx(0.9f, 1.012f, duration * 0.11f));
			yield return StartCoroutine(ScaleBGx(1.012f, 1f, duration * 0.06f));
		}
		else
		{
			yield return StartCoroutine(ScaleBGx(1f, 0f, duration));
		}
	}

	private IEnumerator ScaleBGx(float start, float target, float duration)
	{
		for (float t = 0f; t < duration; t += Time.unscaledDeltaTime)
		{
			float pct = t / duration;
			bgScaler.localScale = new Vector2(Mathf.Lerp(start, target, pct), bgScaler.localScale.y);
			yield return null;
		}
		bgScaler.localScale = new Vector2(target, bgScaler.localScale.y);
	}

	private IEnumerator AnimateBGy(bool open, float duration)
	{
		if (open)
		{
			yield return StartCoroutine(ScaleBGy(0f, 1.02f, duration * 0.65f));
			yield return StartCoroutine(ScaleBGy(1.02f, 0.97f, duration * 0.18f));
			yield return StartCoroutine(ScaleBGy(0.97f, 1f, duration * 0.13f));
			yield return StartCoroutine(ScaleBGy(1.005f, 1f, duration * 0.04f));
		}
		else
		{
			yield return StartCoroutine(ScaleBGy(1f, 0f, duration));
		}
	}

	private IEnumerator ScaleBGy(float start, float target, float duration)
	{
		for (float t = 0f; t < duration; t += Time.unscaledDeltaTime)
		{
			float pct = t / duration;
			bgScaler.localScale = new Vector2(bgScaler.localScale.x, Mathf.Lerp(start, target, pct));
			yield return null;
		}
		bgScaler.localScale = new Vector2(bgScaler.localScale.x, target);
	}

	private IEnumerator ContentFade(bool open)
	{
		SetActive(true);
		float startAlpha = 0f;
		float goalAlpha = ((!open) ? 0f : 1f);
		if ((float)images.Length > 0f)
		{
			startAlpha = images[0].color.a;
		}
		else if ((float)text.Length > 0f)
		{
			startAlpha = text[0].color.a;
		}
		float startPctAlpha = 1f - Mathf.Abs(startAlpha - goalAlpha);
		for (float t = startPctAlpha * contentDuration; t < contentDuration; t += Time.unscaledDeltaTime)
		{
			float alpha = Mathf.Lerp(startAlpha, goalAlpha, t / contentDuration);
			SetContentAlpha(alpha);
			yield return null;
		}
		SetContentAlpha(goalAlpha);
	}

	private void SetContentAlpha(float alpha)
	{
		Color color;
		for (int i = 0; i < images.Length; i++)
		{
			color = images[i].color;
			images[i].color = new Color(color.r, color.g, color.b, alpha);
		}
		for (int j = 0; j < text.Length; j++)
		{
			color = text[j].color;
			text[j].color = new Color(color.r, color.g, color.b, alpha);
		}
		SetColorForImage(next, alpha);
		SetColorForImage(previous, alpha);
		SetColorForImage(close, alpha);
		color = pageNumbers.color;
		pageNumbers.color = new Color(color.r, color.g, color.b, alpha);
	}

	private void SetColorForImage(Image image, float alpha)
	{
		Color color = image.color;
		image.color = new Color(color.r, color.g, color.b, alpha);
	}

	private void SetActive(bool enabled)
	{
		pages[currentPageNumber].gameObject.SetActive(enabled);
		close.gameObject.SetActive(enabled);
		next.gameObject.SetActive(enabled);
		previous.gameObject.SetActive(enabled);
		pageNumbers.gameObject.SetActive(enabled);
	}

	public void CloseTutorial()
	{
		if (animationRoutine != null)
		{
			StopCoroutine(animationRoutine);
		}
		images = pages[currentPageNumber].GetComponentsInChildren<Image>();
		text = pages[currentPageNumber].GetComponentsInChildren<Text>();
		animationRoutine = StartCoroutine(Animation(false));
	}

	private void SetupTutorial(int tutorialNumber)
	{
		tutorials[tutorialNumber].gameObject.SetActive(true);
		int childCount = tutorials[tutorialNumber].transform.childCount;
		pages = new RectTransform[childCount];
		for (int i = 0; i < childCount; i++)
		{
			pages[i] = tutorials[tutorialNumber].transform.GetChild(i) as RectTransform;
		}
		currentPageNumber = 0;
		previous.gameObject.SetActive(false);
		images = pages[currentPageNumber].GetComponentsInChildren<Image>(true);
		text = pages[currentPageNumber].GetComponentsInChildren<Text>(true);
		pageNumbers.text = "PAGE " + (currentPageNumber + 1) + " OF " + pages.Length;
	}

	public void SetOpen(int tutorialNumber)
	{
		SetupTutorial(tutorialNumber);
		SetActive(true);
		avatar.rectTransform.anchoredPosition = new Vector2(avatarOpenPos, avatar.rectTransform.anchoredPosition.y);
		bgScaler.localScale = new Vector2(1f, 1f);
		SetContentAlpha(1f);
	}

	public void SetClosed(int tutorialNumber)
	{
		SetActive(false);
		avatar.rectTransform.anchoredPosition = new Vector2(avatarClosedPos, avatar.rectTransform.anchoredPosition.y);
		bgScaler.localScale = new Vector2(0f, 0f);
	}
}
