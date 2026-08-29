using UnityEngine;
using UnityEngine.UI;

public class WalkthroughUI : PineUIComponent
{
	public Canvas root;

	public Image Background;

	public Text Text;

	public Image WalkthroughScroll;

	public CustomWalkthroughStepUI WalkthroughScroll_Content_CustomWalkthroughStepUI;

	public Button WalkthroughScroll_Content_AddStepButton;

	public Text WalkthroughScroll_Content_AddStepButton_Text;

	public Image WalkthroughScroll_ES2ScrollbarVertical;

	public Image WalkthroughScroll_ES2ScrollbarVertical_BGImage;

	public Image WalkthroughScroll_ES2ScrollbarVertical_SlidingArea_Handle;

	public Image WalkthroughScroll_ES2ScrollbarVertical_SlidingArea_Handle_BGHandle;

	public Button SaveAndBack;

	public Text SaveAndBack_Text;

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
			PineUI.addButtonListeners(WalkthroughScroll_Content_AddStepButton);
			PineUI.addButtonListeners(SaveAndBack);
		}
	}
}
