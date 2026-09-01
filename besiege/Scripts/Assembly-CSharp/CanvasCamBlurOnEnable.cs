using UnityEngine;

public class CanvasCamBlurOnEnable : MonoBehaviour
{
	[SerializeField]
	private BlurCamCanvas blurrer;

	private void OnEnable()
	{
		blurrer.AddTarget(GetComponent<RectTransform>());
	}

	private void OnDisable()
	{
		blurrer.RemoveTarget(GetComponent<RectTransform>());
	}
}
