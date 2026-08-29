using UnityEngine;
using UnityEngine.UI;

public class KeyboardUI : PineUIComponent
{
	public ControllerKeyboardData root;

	public Button Key;

	public Text Key_Text;

	public Button Space_Key;

	public Text Space_Key_Text;

	public Image ControllerHints;

	public Image ControllerHints_Image;

	public Text ControllerHints_Image_Title;

	public Image ControllerHints_Image1;

	public Text ControllerHints_Image1_Title;

	public Image ControllerHints_Image2;

	public Text ControllerHints_Image2_Title;

	public Image VRHints;

	public Button VRHints_VRDeleteButton;

	public Text VRHints_VRDeleteButton_Title;

	public Button VRHints_VRBackButton;

	public Text VRHints_VRBackButton_Title;

	public Transform JoystickPointer;

	public Image JoystickPointer_Image;

	public Image JoystickPointer_Image_Image;

	public Image[] ControllerHints_Image_List;

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
			PineUI.addButtonListeners(Key);
			PineUI.addButtonListeners(Space_Key);
			PineUI.addButtonListeners(VRHints_VRDeleteButton);
			PineUI.addButtonListeners(VRHints_VRBackButton);
		}
	}
}
