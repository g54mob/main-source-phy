using UnityEngine;
using UnityEngine.UI;

public class FloatRangePropertyUI : PineUIComponent
{
	public RectTransform root;

	public Button Label;

	public Text Label_Text;

	public Image Value;

	public Slider Value_Slider;

	public Image Value_Slider_Background;

	public Image Value_Slider_FillArea_Fill;

	public Image Value_Slider_HandleSlideArea_Handle;

	public InputField Value_Input;

	public Text Value_Input_Text;

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
