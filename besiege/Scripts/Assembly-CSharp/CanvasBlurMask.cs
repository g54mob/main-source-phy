using UnityEngine;

[AddComponentMenu("Besiege/UI/Canvas/Canvas Blur Mask")]
[RequireComponent(typeof(RectTransform))]
public class CanvasBlurMask : MonoBehaviour
{
	private RectTransform rectTransform;

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
	}

	private void OnEnable()
	{
		UIHelper.AddBlurMask(rectTransform);
	}

	private void OnDisable()
	{
		UIHelper.RemoveBlurMask(rectTransform);
	}
}
