using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(RectTransform))]
public class ScreenSafeAreaScaler : MonoBehaviour
{
	private Rect lastSafeArea = new Rect(0f, 0f, 0f, 0f);

	private float lastScreenWidth;

	private float lastScreenHeight;

	private ScreenOrientation lastOrientation = ScreenOrientation.AutoRotation;

	private Canvas canvas;

	private RectTransform rectTransform;

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		canvas = rectTransform.GetComponentInParent<Canvas>();
	}

	private void Start()
	{
		sync();
	}

	private void Update()
	{
		sync();
	}

	private void sync()
	{
		Camera camera = canvas.worldCamera;
		if (camera == null)
		{
			camera = Camera.main;
		}
		if (camera == null)
		{
			camera = Object.FindObjectOfType<Camera>();
		}
		if (!(canvas == null) && !(camera == null) && (Screen.safeArea != lastSafeArea || Screen.orientation != lastOrientation || (float)Screen.width != lastScreenWidth || (float)Screen.height != lastScreenHeight))
		{
			Vector2 b = new Vector2(1f / (canvas.pixelRect.width / camera.rect.width), 1f / (canvas.pixelRect.height / camera.rect.height));
			rectTransform.anchorMin = Vector2.Scale(Screen.safeArea.min, b);
			rectTransform.anchorMax = Vector2.Scale(Screen.safeArea.max, b);
			lastSafeArea = Screen.safeArea;
			lastOrientation = Screen.orientation;
			lastScreenWidth = Screen.width;
			lastScreenHeight = Screen.height;
		}
	}
}
