using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UnitEditorColorWheelFlash : MonoBehaviour
{
	public AnimationCurve blinkCurve;

	private Image image;

	private void Awake()
	{
		image = GetComponent<Image>();
		image.enabled = false;
	}

	public void SetLastSibling()
	{
		base.transform.SetAsLastSibling();
	}

	public void Blink(Color color)
	{
		StartCoroutine(BlinkCorutine(color));
	}

	private IEnumerator BlinkCorutine(Color color)
	{
		image.enabled = true;
		Color c = color;
		float time = blinkCurve.keys[blinkCurve.keys.Length - 1].time;
		for (float t = 0f; t < time; t += Time.unscaledDeltaTime)
		{
			c.a = blinkCurve.Evaluate(t);
			image.color = c;
			yield return null;
		}
		c.a = blinkCurve.Evaluate(0f);
		image.color = c;
	}
}
