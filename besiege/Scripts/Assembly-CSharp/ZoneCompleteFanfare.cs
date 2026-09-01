using System.Collections;
using UnityEngine;

[AddComponentMenu("Win Screen/ZoneCompleteFanfare")]
public class ZoneCompleteFanfare : MonoBehaviour
{
	public float fadeDuration = 0.1f;

	public float startWaitDuration = 1f;

	private Color startColour;

	public DynamicText textObject;

	public MeshRenderer leftHeaderSymbol;

	public MeshRenderer rightHeaderSymbol;

	protected IEnumerator stampCoroutine;

	protected IEnumerator lerpAlphaCoroutine;

	protected CreateDropShadows textShadows;

	private MeshRenderer headerRender;

	public virtual void Start()
	{
		if (textObject != null)
		{
			textShadows = textObject.GetComponent<CreateDropShadows>();
			SetTextActive(false);
		}
	}

	public virtual void StartAnimation(float wait = -1f, float duration = -1f)
	{
		wait = ((!(wait < 0f)) ? wait : startWaitDuration);
		duration = ((!(duration < 0f)) ? duration : fadeDuration);
		stampCoroutine = Animate(wait, duration);
		StartCoroutine(stampCoroutine);
	}

	public virtual void Disable()
	{
		if (stampCoroutine != null)
		{
			StopCoroutine(stampCoroutine);
		}
		if (lerpAlphaCoroutine != null)
		{
			StopCoroutine(lerpAlphaCoroutine);
		}
		if (textObject != null)
		{
			if ((bool)textShadows)
			{
				textShadows.Clear();
			}
			SetTextActive(false);
		}
	}

	protected virtual IEnumerator Animate(float wait, float duration)
	{
		yield return new WaitForSecondsRealtime(wait);
		if (textObject == null || leftHeaderSymbol == null || rightHeaderSymbol == null)
		{
			Debug.LogError(string.Concat("Stamp couldn't be fired (textObject=", textObject, " leftHeaderSymbol=", leftHeaderSymbol, " rightHeaderSymbol=", rightHeaderSymbol, ")!"));
		}
		else
		{
			SetTextActive(true);
			SetIcons();
			lerpAlphaCoroutine = LerpAlpha(1f, duration);
			yield return StartCoroutine(lerpAlphaCoroutine);
		}
	}

	public virtual void SetTextActive(bool active)
	{
		textObject.gameObject.SetActive(active);
		leftHeaderSymbol.gameObject.SetActive(active);
		rightHeaderSymbol.gameObject.SetActive(active);
	}

	public virtual void SetTextAlpha(float alpha)
	{
		Color color = headerRender.material.color;
		Color color2 = leftHeaderSymbol.material.GetColor("_TintColor");
		Color color3 = rightHeaderSymbol.material.GetColor("_TintColor");
		headerRender.material.color = new Color(color.r, color.g, color.b, alpha);
		leftHeaderSymbol.material.SetColor("_TintColor", new Color(color2.r, color2.g, color2.b, alpha));
		rightHeaderSymbol.material.SetColor("_TintColor", new Color(color3.r, color3.g, color3.b, alpha));
	}

	protected virtual IEnumerator LerpAlpha(float alpha, float time, float delay = 0f)
	{
		headerRender = textObject.GetComponent<MeshRenderer>();
		if (delay > 0f)
		{
			yield return new WaitForSecondsRealtime(delay);
		}
		if (alpha > 0f)
		{
			textShadows.Create();
		}
		else
		{
			textShadows.Clear();
		}
		for (float t = 0f; t <= time; t += TimeSlider.Instance.deltaTime)
		{
			float elapsed = t / time;
			SetTextAlpha(Mathf.Lerp(0f, alpha, elapsed));
			yield return null;
		}
	}

	protected virtual void SetIcons()
	{
		Transform transform = textObject.transform;
		Bounds bounds = textObject.bounds;
		Vector3 lossyScale = transform.lossyScale;
		Vector3 position = transform.position;
		float num = position.x + bounds.max.x * lossyScale.x;
		float num2 = position.x + bounds.min.x * lossyScale.x;
		float num3 = 0.5f;
		if (leftHeaderSymbol != null)
		{
			Transform transform2 = leftHeaderSymbol.transform;
			Vector3 position2 = transform2.position;
			transform2.position = new Vector3(num2 - num3, position2.y, position2.z);
		}
		else
		{
			Debug.LogError("SetIcons: leftHeaderSymbol is null!");
		}
		if (rightHeaderSymbol != null)
		{
			Transform transform3 = rightHeaderSymbol.transform;
			Vector3 position3 = transform3.position;
			transform3.position = new Vector3(num + num3 - 0.055f, position3.y, position3.z);
		}
		else
		{
			Debug.LogError("SetIcons: rightHeaderSymbol is null!");
		}
	}
}
