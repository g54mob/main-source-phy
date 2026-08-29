using UnityEngine;
using UnityEngine.UI;

public class CustomWalkthroughUI : PineUIComponent
{
	public Canvas root;

	public Button Background;

	public Image SafeAreas_Modal;

	public Text SafeAreas_Modal_Text;

	public Button SafeAreas_Modal_ExitToMenu;

	public Image SafeAreas_Modal_ScrollView;

	public Text SafeAreas_Modal_ScrollView_Content_NoStepsText;

	public CustomWalkthroughStepUI SafeAreas_Modal_ScrollView_Content_CustomWalkthroughStepUI;

	public Image SafeAreas_Modal_ScrollView_ScrollbarVertical;

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
			PineUI.addButtonListeners(Background);
			PineUI.addButtonListeners(SafeAreas_Modal_ExitToMenu);
		}
	}
}
