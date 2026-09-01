using UnityEngine;

public class ForceUpdateUI : MonoBehaviour
{
	private RectTransform rectTransform;

	private void Update()
	{
		rectTransform = GetComponent<RectTransform>();
		rectTransform.ForceUpdateRectTransforms();
	}
}
