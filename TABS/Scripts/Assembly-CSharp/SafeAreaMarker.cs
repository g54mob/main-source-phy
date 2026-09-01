using TFBGames;
using UnityEngine;

public class SafeAreaMarker : MonoBehaviour
{
	[SerializeField]
	private float fadeSpeed = 1f;

	[SerializeField]
	private bool fadeMarkers = true;

	private CanvasGroup canvasGroup;

	private SettingsInstance settingsSliderInstance;

	private UISliderItem sliderItem;

	private void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
		if (fadeMarkers)
		{
			canvasGroup.alpha = 0f;
		}
		else
		{
			canvasGroup.alpha = 1.1f;
		}
	}

	private void Start()
	{
		GlobalSettingsHandler service = ServiceLocator.GetService<GlobalSettingsHandler>();
		if (service != null)
		{
			settingsSliderInstance = service.GetSettingsInstance(SafeArea.SAFE_AREA_SETTINGS_KEY);
			if (settingsSliderInstance != null)
			{
				settingsSliderInstance.OnSliderValueChanged += ShowSafeSpace;
			}
		}
	}

	private void Update()
	{
		if (canvasGroup.alpha > 0f && fadeMarkers)
		{
			float alpha = canvasGroup.alpha;
			alpha -= fadeSpeed * Time.unscaledDeltaTime;
			canvasGroup.alpha = alpha;
		}
		if (canvasGroup.alpha <= 0f)
		{
			canvasGroup.alpha = 0f;
			canvasGroup.gameObject.SetActive(value: false);
		}
	}

	private void ShowSafeSpace(float obj)
	{
		if (!(sliderItem == null) && sliderItem.isSelected)
		{
			canvasGroup.gameObject.SetActive(value: true);
			canvasGroup.alpha = 1f;
		}
	}

	public void LinkSliderToSafeAreaMarker(MenuSettingsButton settingsButton)
	{
		UISliderItem componentInChildren = settingsButton.GetComponentInChildren<UISliderItem>();
		if ((bool)componentInChildren)
		{
			sliderItem = componentInChildren;
		}
	}
}
