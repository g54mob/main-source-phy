using UnityEngine;

public class ScrollToZoom : MonoBehaviour
{
	public Camera cam;

	public float speed = 1f;

	public float LerpSpeed = 6f;

	public float min = 1f;

	public float max = 7f;

	public float startZoomInAmount;

	private float sizeToBe = 1f;

	public Transform[] lowerLeftAligners = new Transform[0];

	private float lastSize;

	private void Awake()
	{
		sizeToBe = cam.orthographicSize;
	}

	private void Update()
	{
		if (StatMaster.inMenu)
		{
			return;
		}
		sizeToBe += InputManager.ZoomValue() * speed;
		sizeToBe = Mathf.Clamp(sizeToBe, min, max);
		cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, sizeToBe, LerpSpeed * Time.deltaTime);
		if (cam.orthographicSize != lastSize)
		{
			Vector3 position = cam.ViewportToWorldPoint(Vector3.zero);
			position.z = 0f;
			for (int i = 0; i < lowerLeftAligners.Length; i++)
			{
				lowerLeftAligners[i].position = position;
			}
			lastSize = cam.orthographicSize;
		}
	}
}
