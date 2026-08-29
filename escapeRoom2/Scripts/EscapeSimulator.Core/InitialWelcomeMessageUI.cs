using UnityEngine;
using UnityEngine.UI;

public class InitialWelcomeMessageUI : PineUIComponent
{
	public Canvas root;

	public ES2FrameController ES2Frame;

	public CanvasGroup Fader;

	public Image Fader_Image_1;

	public Image Fader_Image_1_Content;

	public Image Fader_Image_1_Content_Image_0;

	public Text Fader_Image_1_Content_Title;

	public Text Fader_Image_1_Content_Message;

	public Image Fader_Image_1_Content_Spacer;

	public Image Fader_Image_1_Content_ButtonContainer;

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
