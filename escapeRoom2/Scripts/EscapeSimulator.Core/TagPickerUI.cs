using UnityEngine;
using UnityEngine.UI;

public class TagPickerUI : PineUIComponent
{
	public Canvas root;

	public ES2FrameUI Frame;

	public Image SafeAreas_Modal;

	public UndraggableRect SafeAreas_Modal_ScrollView;

	public Image SafeAreas_Modal_ScrollView_Viewport;

	public Image SafeAreas_Modal_ScrollView_Viewport_Content_Panel;

	public Scrollbar SafeAreas_Modal_ScrollView_ScrollbarVertical;

	public Image SafeAreas_Modal_ScrollView_ScrollbarVertical_BGImage;

	public Image SafeAreas_Modal_ScrollView_ScrollbarVertical_SlidingArea_Handle;

	public Image SafeAreas_Modal_ScrollView_ScrollbarVertical_SlidingArea_Handle_BGHandle;

	public Button SafeAreas_Modal_Reset;

	public Text SafeAreas_Modal_Reset_Text;

	public Image Gradient;

	public Transform BotRight;

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
			PineUI.addButtonListeners(SafeAreas_Modal_Reset);
		}
	}
}
