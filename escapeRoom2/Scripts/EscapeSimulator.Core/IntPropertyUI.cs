using UnityEngine;
using UnityEngine.UI;

public class IntPropertyUI : PineUIComponent
{
	public RectTransform root;

	public Button Label;

	public Text Label_Text;

	public Slider Slider;

	public Image Slider_Background;

	public Image Slider_FillArea_Fill;

	public Image Slider_HandleSlideArea_Handle;

	public Image InputField;

	public Button InputField_Drag;

	public Image InputField_Drag_Icon;

	public InputField InputField_Input;

	public Text InputField_Input_Text;

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
