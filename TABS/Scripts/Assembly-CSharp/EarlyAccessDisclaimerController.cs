using System;
using InControl;
using Landfall.TABS_Input;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EarlyAccessDisclaimerController : MonoBehaviour
{
	[SerializeField]
	private Button okButton;

	[SerializeField]
	private Button earlyAccessLinkButton;

	[SerializeField]
	private TMP_Text bodyText;

	[SerializeField]
	private Color buttonIconColor = Color.white;

	[SerializeField]
	[TextArea(15, 100)]
	private string earlyAccessMessageBody;

	[SerializeField]
	private TABSBooter tabsBooter;

	private TextMeshProUGUI buttonText;

	private string acceptButtonGlyphText;

	private TMP_SubMeshUI iconSubMesh;

	private const string steamEarlyAccessText = "Early Access";

	private const string gamePassText = "Preview";

	private const string okButtonWords = "OK";

	private GlyphService iconService;

	private const int ADD_LISTENER_FRAME_DELAY = 10;

	private int frameCountOnStart;

	private bool hasAddedOkayButtonListener;

	private void Start()
	{
		frameCountOnStart = Time.frameCount;
		SetPlatformText();
		Navigation navigation = new Navigation
		{
			mode = Navigation.Mode.Explicit,
			selectOnDown = earlyAccessLinkButton,
			selectOnUp = earlyAccessLinkButton
		};
		okButton.navigation = navigation;
		navigation.selectOnDown = okButton;
		navigation.selectOnUp = okButton;
		earlyAccessLinkButton.navigation = navigation;
		InputService service = ServiceLocator.GetService<InputService>();
		if (service != null)
		{
			service.InputChanged += OnInputSourceChanged;
			service.InputDeviceStyleChanged += OnDeviceStyleChanged;
		}
		okButton.Select();
		buttonText = okButton.GetComponentInChildren<TextMeshProUGUI>();
		iconService = ServiceLocator.GetService<GlyphService>();
		SetOKButtonText(PlayerActions.Instance.InputType, PlayerActions.Instance.LastDeviceStyle);
	}

	private void Update()
	{
		if (!hasAddedOkayButtonListener && Time.frameCount > frameCountOnStart + 10)
		{
			okButton.onClick.AddListener(OnOkayClicked);
			hasAddedOkayButtonListener = true;
		}
	}

	private void OnOkayClicked()
	{
		if (tabsBooter != null)
		{
			tabsBooter.Init();
		}
	}

	private void SetPlatformText()
	{
		string text = "Early Access";
		text = "Early Access";
		if (bodyText != null)
		{
			bodyText.text = string.Format(earlyAccessMessageBody, text);
		}
	}

	private void SetOKButtonText(InputType type, InputDeviceStyle deviceStyle)
	{
		acceptButtonGlyphText = iconService.GetActionGlyph(PlayerActions.Instance.m_accept, InputType.Controller, deviceStyle);
		string text = ((type == InputType.Controller) ? acceptButtonGlyphText : "");
		buttonText.text = text + " OK";
	}

	private void OnInputSourceChanged(InputType type)
	{
		switch (type)
		{
		case InputType.Controller:
			okButton.Select();
			break;
		default:
			throw new ArgumentOutOfRangeException("type", type, null);
		case InputType.Keyboard:
		case InputType.Any:
			break;
		}
		SetOKButtonText(type, PlayerActions.Instance.LastDeviceStyle);
	}

	private void OnDeviceStyleChanged(InputDeviceStyle deviceStyle)
	{
		SetOKButtonText(PlayerActions.Instance.InputType, deviceStyle);
	}

	private void OnDisable()
	{
		InputService service = ServiceLocator.GetService<InputService>();
		if (service != null)
		{
			service.InputChanged -= OnInputSourceChanged;
			service.InputDeviceStyleChanged -= OnDeviceStyleChanged;
		}
	}

	private void OnDestroy()
	{
		if (okButton != null)
		{
			okButton.onClick.RemoveListener(OnOkayClicked);
		}
	}
}
