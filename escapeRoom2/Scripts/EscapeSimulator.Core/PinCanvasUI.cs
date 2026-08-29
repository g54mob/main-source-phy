using UnityEngine;
using UnityEngine.UI;

public class PinCanvasUI : PineUIComponent
{
	public Canvas root;

	public Image TitleBarRight;

	public Button TitleBarRight_ScaleDown;

	public Image TitleBarRight_ScaleDown_Image;

	public Button TitleBarRight_ScaleUp;

	public Image TitleBarRight_ScaleUp_Image;

	public Image TitleBarRight_Spacer;

	public Button TitleBarRight_Unpin;

	public Image TitleBarRight_Unpin_Image;

	public Image TitleBarLeft;

	public Button TitleBarLeft_Unpin;

	public Image TitleBarLeft_Unpin_Image;

	public Image TitleBarLeft_Spacer;

	public Button TitleBarLeft_ScaleDown;

	public Image TitleBarLeft_ScaleDown_Image;

	public Button TitleBarLeft_ScaleUp;

	public Image TitleBarLeft_ScaleUp_Image;

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
			PineUI.addButtonListeners(TitleBarRight_ScaleDown);
			PineUI.addButtonListeners(TitleBarRight_ScaleUp);
			PineUI.addButtonListeners(TitleBarRight_Unpin);
			PineUI.addButtonListeners(TitleBarLeft_Unpin);
			PineUI.addButtonListeners(TitleBarLeft_ScaleDown);
			PineUI.addButtonListeners(TitleBarLeft_ScaleUp);
		}
	}
}
