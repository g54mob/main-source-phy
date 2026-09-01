using UnityEngine;
using UnityEngine.UI;

public class DebugIconRect : MonoBehaviour
{
	private CanvasScaler canvasScaler;

	private RectTransform rectTransform;

	private void Awake()
	{
		rectTransform = base.transform as RectTransform;
		canvasScaler = GetComponentInParent<CanvasScaler>();
	}

	private void Update()
	{
		_ = canvasScaler.GetComponent<CanvasScaler>().referenceResolution;
		_ = (Vector2)canvasScaler.transform.lossyScale;
		Vector2 vector = RectPosToScreenSpace(rectTransform.TransformPoint(new Vector2(rectTransform.rect.xMin, rectTransform.rect.yMin)));
		Vector2 vector2 = RectPosToScreenSpace(rectTransform.TransformPoint(new Vector2(rectTransform.rect.xMax, rectTransform.rect.yMax)));
		float num = vector2.x - vector.x;
		float num2 = vector2.y - vector.y;
		Debug.Log("Width: " + num + ", Height: " + num2);
	}

	private Vector2 RectPosToScreenSpace(Vector3 pos)
	{
		Vector2 referenceResolution = canvasScaler.referenceResolution;
		Vector2 vector = canvasScaler.transform.lossyScale;
		return ((pos - canvasScaler.transform.position) / vector / referenceResolution + new Vector2(0.5f, 0.5f)) * new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
	}
}
