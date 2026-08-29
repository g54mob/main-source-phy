using UnityEngine;
using UnityEngine.UI;

public class SearchUI : PineUIComponent
{
	public Canvas root;

	public Image Tips;

	public Image Rect;

	public RectTransform Rect_Content;

	public Button Rect_Content_Item;

	public RectTransform Outside;

	public Image Outside_Filter;

	public Text Outside_Filter_Counter;

	public InputField Outside_Filter_Input;

	public Text Outside_Filter_Input_Text;

	public Image Outside_Filter_Icon;

	public Image Outside_Filter_Gear;

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
			PineUI.addButtonListeners(Rect_Content_Item);
		}
	}
}
