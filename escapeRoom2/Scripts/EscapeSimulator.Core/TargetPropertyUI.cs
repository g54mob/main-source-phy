using UnityEngine;
using UnityEngine.UI;

public class TargetPropertyUI : PineUIComponent
{
	public RectTransform root;

	public Button Label;

	public Text Label_Text;

	public Button Button;

	public Image Button_Icon;

	public Image Button_Modified;

	public Slider Slider;

	public Image Slider_Background;

	public Image Slider_FillArea_Fill;

	public Image Slider_HandleSlideArea_Handle;

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
