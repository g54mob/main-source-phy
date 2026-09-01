using UnityEngine;

public class SafeArea : MonoBehaviour
{
	private class TitleSafe
	{
		private Vector2 baseResolution;

		private Vector2 safeResolution;

		private Vector2 edgeBuffer;

		public Rect safeAreaRect { get; }

		public TitleSafe(float scaledDown)
		{
			baseResolution = new Vector2(Screen.width, Screen.height);
			safeResolution = new Vector2(baseResolution.x * scaledDown, baseResolution.y * scaledDown);
			edgeBuffer = new Vector2((baseResolution.x - safeResolution.x) * 0.5f, (baseResolution.y - safeResolution.y) * 0.5f);
			safeAreaRect = new Rect(edgeBuffer.x / baseResolution.x, edgeBuffer.y / baseResolution.y, safeResolution.x / baseResolution.x, safeResolution.y / baseResolution.y);
		}
	}

	private SettingsInstance settingsSlider;

	public static readonly string SAFE_AREA_SETTINGS_KEY = "TITLE_SAFE_SPACE";

	private RectTransform Panel;

	private Rect LastSafeArea = new Rect(0f, 0f, 0f, 0f);

	private Vector2Int LastScreenSize = new Vector2Int(0, 0);

	private ScreenOrientation LastOrientation = ScreenOrientation.AutoRotation;

	[SerializeField]
	private bool ConformX = true;

	[SerializeField]
	private bool ConformY = true;

	[SerializeField]
	private bool Logging;

	private int minimumScaleDown = 85;

	private void Awake()
	{
		Panel = GetComponent<RectTransform>();
		if (Panel == null)
		{
			Debug.LogError("Cannot apply safe area - no RectTransform found on " + base.name + " will now be destroyed");
			Object.Destroy(base.gameObject);
		}
	}

	private void OnEnable()
	{
		GlobalSettingsHandler service = ServiceLocator.GetService<GlobalSettingsHandler>();
		if (service != null)
		{
			settingsSlider = service.GetSettingsInstance(SAFE_AREA_SETTINGS_KEY);
			if (settingsSlider != null)
			{
				minimumScaleDown = (int)settingsSlider.min;
				settingsSlider.OnSliderValueChanged += UpdateSafeTitleSafe;
				UpdateSafeTitleSafe(settingsSlider.currentSliderValue);
			}
			else
			{
				Debug.LogError("Unable to find settings instance with key: " + SAFE_AREA_SETTINGS_KEY);
			}
		}
	}

	private void OnDisable()
	{
		if (settingsSlider != null)
		{
			settingsSlider.OnSliderValueChanged -= UpdateSafeTitleSafe;
		}
	}

	private void UpdateSafeTitleSafe(float safeSpaceSize)
	{
		Rect safeArea = GetSafeArea(safeSpaceSize);
		if (safeArea != LastSafeArea || Screen.width != LastScreenSize.x || Screen.height != LastScreenSize.y || Screen.orientation != LastOrientation)
		{
			LastScreenSize.x = Screen.width;
			LastScreenSize.y = Screen.height;
			LastOrientation = Screen.orientation;
			ApplySafeArea(safeArea);
		}
	}

	private Rect GetSafeArea(float safeSpaceSize)
	{
		if (safeSpaceSize <= (float)minimumScaleDown)
		{
			safeSpaceSize = minimumScaleDown;
		}
		Rect safeRatio = GetSafeRatio(safeSpaceSize);
		return new Rect((float)Screen.width * safeRatio.x, (float)Screen.height * safeRatio.y, (float)Screen.width * safeRatio.width, (float)Screen.height * safeRatio.height);
	}

	private Rect GetSafeRatio(float i)
	{
		return new TitleSafe(i / 100f).safeAreaRect;
	}

	private void ApplySafeArea(Rect rect)
	{
		LastSafeArea = rect;
		if (!ConformX)
		{
			rect.x = 0f;
			rect.width = Screen.width;
		}
		if (!ConformY)
		{
			rect.y = 0f;
			rect.height = Screen.height;
		}
		Vector2 position = rect.position;
		Vector2 anchorMax = rect.position + rect.size;
		position.x /= Screen.width;
		position.y /= Screen.height;
		anchorMax.x /= Screen.width;
		anchorMax.y /= Screen.height;
		if (Panel == null)
		{
			Panel = GetComponent<RectTransform>();
		}
		Panel.anchorMin = position;
		Panel.anchorMax = anchorMax;
		if (Logging)
		{
			Debug.LogFormat("New safe area applied to {0}: x={1}, y={2}, w={3}, h={4} on full extents w={5}, h={6}", base.name, rect.x, rect.y, rect.width, rect.height, Screen.width, Screen.height);
		}
	}
}
