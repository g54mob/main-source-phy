using UnityEngine;
using UnityEngine.UI;

public class MenuNewsUI : PineUIComponent
{
	public CanvasGroup root;

	public Button MenuButton;

	public Image MenuButton_MainImage;

	public Image MenuButton_MainImage_Image;

	public Text MenuButton_MessageText;

	public Image MenuButton_OpenURL;

	public Image MenuButton_SelectionFrame;

	public Image MenuButton_ExclamationPoint;

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
			PineUI.addButtonListeners(MenuButton);
		}
	}
}
