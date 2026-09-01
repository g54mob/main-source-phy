using InControl;
using Landfall.TABS_Input;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PagingTriggerIcon : MonoBehaviour
{
	[SerializeField]
	private bool isLeft;

	[SerializeField]
	private bool disableImageOnGamepad;

	[SerializeField]
	private TextMeshProUGUI textMesh;

	[SerializeField]
	private Button pageButton;

	[SerializeField]
	private bool disablePageButtonOnGamepad = true;

	private const string ENLARGE_TEXT_SIZE = "<size=130>";

	private string buttonGlyphText;

	private InputService inputService;

	private GlyphService iconService;

	private PlayerAction action;

	[SerializeField]
	private Image buttonImage;

	private void Start()
	{
		if (textMesh == null)
		{
			textMesh = GetComponentInChildren<TextMeshProUGUI>();
		}
		if (pageButton == null)
		{
			pageButton = GetComponentInChildren<Button>();
		}
		iconService = ServiceLocator.GetService<GlyphService>();
		inputService = ServiceLocator.GetService<InputService>();
		inputService.InputChanged += OnInputChanged;
		inputService.InputDeviceStyleChanged += OnInputDeviceStyleChanged;
		action = (isLeft ? PlayerActions.Instance.m_pageLeft : PlayerActions.Instance.m_pageRight);
		SetUpGamepadIcons(PlayerActions.Instance.InputType, PlayerActions.Instance.LastDeviceStyle);
	}

	private void OnDestroy()
	{
		if (inputService != null)
		{
			inputService.InputChanged -= OnInputChanged;
			inputService.InputDeviceStyleChanged -= OnInputDeviceStyleChanged;
		}
	}

	private void OnInputChanged(InputType inputType)
	{
		SetUpGamepadIcons(inputType, PlayerActions.Instance.LastDeviceStyle);
	}

	private void OnInputDeviceStyleChanged(InputDeviceStyle deviceStyle)
	{
		SetUpGamepadIcons(PlayerActions.Instance.InputType, deviceStyle);
	}

	private void SetUpGamepadIcons(InputType inputType, InputDeviceStyle deviceStyle)
	{
		buttonGlyphText = iconService.GetActionGlyph(action, InputType.Controller, deviceStyle);
		switch (inputType)
		{
		case InputType.Controller:
			if (textMesh != null)
			{
				textMesh.text = "<size=130>" + buttonGlyphText;
			}
			if (buttonImage != null)
			{
				buttonImage.enabled = !disableImageOnGamepad;
			}
			if (pageButton != null && disablePageButtonOnGamepad)
			{
				pageButton.enabled = false;
			}
			break;
		case InputType.Keyboard:
		case InputType.Any:
		{
			string text = (isLeft ? "<" : ">");
			if (textMesh != null)
			{
				textMesh.text = text;
			}
			if (buttonImage != null)
			{
				buttonImage.enabled = true;
			}
			if (pageButton != null && disablePageButtonOnGamepad)
			{
				pageButton.enabled = true;
			}
			break;
		}
		}
	}
}
