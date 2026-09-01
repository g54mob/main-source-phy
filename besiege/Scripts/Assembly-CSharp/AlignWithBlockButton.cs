using UnityEngine;
using UnityEngine.UI;

public class AlignWithBlockButton : MonoBehaviour
{
	public Transform button;

	public Vector2 offset;

	private Camera hudCam;

	private RectTransform canvas;

	private CanvasScaler scaler;

	private void Start()
	{
		if (button == null)
		{
			button = GameObject.Find("BLOCK BUTTONS").transform.FindChild("t_BASIC/SteeringHinge");
		}
		hudCam = GameObject.FindGameObjectWithTag("hudCamera").GetComponent<Camera>();
		canvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
		scaler = GetComponentInParent<CanvasScaler>();
		(base.transform as RectTransform).anchorMin = Vector2.zero;
		(base.transform as RectTransform).anchorMax = Vector2.zero;
	}

	private void Update()
	{
		Vector2 a = hudCam.WorldToViewportPoint(button.position);
		Vector2 size = canvas.rect.size;
		Vector2 vector = new Vector2(canvas.rect.size.x / scaler.referenceResolution.x, canvas.rect.size.y / scaler.referenceResolution.y);
		a = Vector2.Scale(a, size);
		a += new Vector2(offset.x / vector.x, offset.y / vector.y);
		(base.transform as RectTransform).anchoredPosition = a;
	}
}
