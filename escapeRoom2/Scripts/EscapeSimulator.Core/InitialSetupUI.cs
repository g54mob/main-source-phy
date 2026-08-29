using UnityEngine;
using UnityEngine.UI;

public class InitialSetupUI : PineUIComponent
{
	public Canvas root;

	public Image Options;

	public Image Options_Layout;

	public Image Options_Layout_Content;

	public Image Options_Layout_Content_BrightnessImage;

	public Image Options_Layout_Content_Spacer_0;

	public Transform Options_Layout_Content_B_L;

	public Text Options_Layout_Content_B_R_Message;

	public Image Options_Layout_Content_Spacer_1;

	public Image Options_Layout_Content_ButtonContainer;

	public Image Tutorial;

	public RawImage Tutorial_Background;

	public Image Tutorial_Layout;

	public Image Tutorial_Layout_Content;

	public Image Tutorial_Layout_Content_Image_0;

	public Text Tutorial_Layout_Content_Title;

	public Text Tutorial_Layout_Content_Message;

	public Image Tutorial_Layout_Content_Spacer;

	public Transform Tutorial_Layout_Content_ButtonContainer;

	public Image[] Options_Layout_Content_Spacer__List;

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
		}
	}
}
