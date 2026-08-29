using UnityEngine;
using UnityEngine.UI;

public class ClientCodeUI : PineUIComponent
{
	public Canvas root;

	public ES2FrameController Frame;

	public InputField InputField;

	public Text InputField_Placeholder;

	public Text InputField_Text;

	public ControllerKeyboardData ControllerKeyboard;

	public Button ControllerKeyboard_Key;

	public Text ControllerKeyboard_Key_Text;

	public Image ControllerKeyboard_ControllerHints;

	public Image ControllerKeyboard_ControllerHints_Image;

	public Text ControllerKeyboard_ControllerHints_Image_Title;

	public Image ControllerKeyboard_ControllerHints_Image1;

	public Text ControllerKeyboard_ControllerHints_Image1_Title;

	public Image ControllerKeyboard_ControllerHints_Image2;

	public Text ControllerKeyboard_ControllerHints_Image2_Title;

	public Image ControllerKeyboard_VRHints;

	public Button ControllerKeyboard_VRHints_VRDeleteButton;

	public Text ControllerKeyboard_VRHints_VRDeleteButton_Title;

	public Button ControllerKeyboard_VRHints_VRBackButton;

	public Text ControllerKeyboard_VRHints_VRBackButton_Title;

	public Image ControllerKeyboard_JoystickPointer_Image;

	public Image ControllerKeyboard_JoystickPointer_Image_Image;

	public Image[] ControllerKeyboard_ControllerHints_Image_List;

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
			PineUI.addButtonListeners(ControllerKeyboard_Key);
			PineUI.addButtonListeners(ControllerKeyboard_VRHints_VRDeleteButton);
			PineUI.addButtonListeners(ControllerKeyboard_VRHints_VRBackButton);
		}
	}
}
