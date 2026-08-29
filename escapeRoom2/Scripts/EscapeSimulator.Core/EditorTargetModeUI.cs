using UnityEngine;
using UnityEngine.UI;

public class EditorTargetModeUI : PineUIComponent
{
	public Canvas root;

	public Image TargetPin;

	public Image ResetBackground;

	public Button ResetBackground_Reset;

	public Text ResetBackground_Reset_Text;

	public Image Buttons;

	public Button Buttons_CancelExit;

	public Text Buttons_CancelExit_Text;

	public Button Buttons_Apply;

	public Text Buttons_Apply_Text;

	public PasswordPickerUI PasswordPicker;

	public Image Properties;

	public object data;

	private bool isInitialized;

	protected override void Awake()
	{
		init();
	}

	public void init()
	{
		if (!isInitialized)
		{
			isInitialized = true;
			PineUI.addButtonListeners(ResetBackground_Reset);
			PineUI.addButtonListeners(Buttons_CancelExit);
			PineUI.addButtonListeners(Buttons_Apply);
		}
	}
}
