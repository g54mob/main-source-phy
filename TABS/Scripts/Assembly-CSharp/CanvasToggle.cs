using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CanvasToggle : MonoBehaviour
{
	[SerializeField]
	private float fadeSpeed = 10f;

	[SerializeField]
	private bool useFades;

	private Canvas canvas;

	private CanvasGroup canvasGroup;

	private bool fadeOutCanvasGroup;

	private bool fadeInCanvasGroup;

	private bool hasCanvasGroup;

	public bool startOff;

	private void Awake()
	{
		canvas = GetComponent<Canvas>();
		canvasGroup = GetComponent<CanvasGroup>();
	}

	private void Start()
	{
		if (canvasGroup != null)
		{
			hasCanvasGroup = true;
		}
		if (startOff)
		{
			TurnOff();
		}
	}

	public void SetCanvasToggle(bool turnCanvasOn)
	{
		if (hasCanvasGroup)
		{
			if (turnCanvasOn)
			{
				canvas.enabled = true;
				fadeOutCanvasGroup = false;
				canvasGroup.alpha = 1f;
			}
			else
			{
				fadeOutCanvasGroup = true;
			}
		}
		else
		{
			canvas.enabled = turnCanvasOn;
		}
	}

	private void Update()
	{
		if (!hasCanvasGroup)
		{
			return;
		}
		if (canvasGroup.alpha > 0f && fadeOutCanvasGroup && useFades)
		{
			float alpha = canvasGroup.alpha;
			alpha -= fadeSpeed * Time.unscaledDeltaTime;
			canvasGroup.alpha = alpha;
			if (canvasGroup.alpha <= 0f)
			{
				fadeOutCanvasGroup = false;
			}
		}
		else if (fadeInCanvasGroup && useFades)
		{
			float alpha2 = canvasGroup.alpha;
			alpha2 += fadeSpeed * Time.unscaledDeltaTime;
			canvasGroup.alpha = alpha2;
			if (canvasGroup.alpha >= 1f)
			{
				fadeInCanvasGroup = false;
			}
		}
		else if (canvasGroup.alpha <= 0f)
		{
			canvas.enabled = false;
		}
	}

	public void FadeIn()
	{
		canvas.enabled = true;
		if (useFades)
		{
			canvasGroup.alpha = 0f;
		}
		fadeInCanvasGroup = true;
	}

	public void FadeOut()
	{
		canvasGroup.alpha = 1f;
		fadeOutCanvasGroup = true;
	}

	public void TurnOn()
	{
		canvas.enabled = true;
		canvasGroup.interactable = true;
		canvasGroup.alpha = 1f;
		fadeInCanvasGroup = false;
		fadeOutCanvasGroup = false;
	}

	public void TurnOff()
	{
		canvas.enabled = false;
		canvasGroup.interactable = false;
		fadeInCanvasGroup = false;
		fadeOutCanvasGroup = false;
	}
}
