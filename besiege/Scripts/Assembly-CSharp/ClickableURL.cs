using UnityEngine;

[RequireComponent(typeof(DynamicText))]
public class ClickableURL : MonoBehaviour
{
	[SerializeField]
	private string url;

	[SerializeField]
	private Color hiliteColor = Color.white;

	private DynamicText dynamicText;

	private Color originalColor;

	private void Start()
	{
		dynamicText = GetComponent<DynamicText>();
		originalColor = dynamicText.color;
		if (string.IsNullOrEmpty(url))
		{
			url = dynamicText.GetText();
		}
	}

	private void OnMouseOver()
	{
		dynamicText.color = hiliteColor;
	}

	private void OnMouseExit()
	{
		dynamicText.color = originalColor;
	}

	private void OnMouseDown()
	{
		Application.OpenURL(url);
	}
}
