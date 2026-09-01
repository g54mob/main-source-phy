using UnityEngine;
using UnityEngine.UI;

public class ScenarioEditorFade : MonoBehaviour
{
	public Image fade;

	public float targetFade;

	private void Update()
	{
		Color color = fade.color;
		color.a = Mathf.Lerp(color.a, targetFade, 10f * Time.unscaledDeltaTime);
		fade.color = color;
	}

	public void SetOn()
	{
		fade.raycastTarget = true;
		targetFade = 0.96f;
	}

	public void SetOff()
	{
		fade.raycastTarget = false;
		targetFade = 0f;
	}
}
