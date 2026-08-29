using UnityEngine;
using UnityEngine.UI;

public class EditorPaintUI : PineUIComponent
{
	public Canvas root;

	public Image Tools;

	public Toggle Tools_Brush;

	public Toggle Tools_Eraser;

	public Image ToolsExtra;

	public Button ToolsExtra_Fill;

	public Image ToolsExtra_Fill_Icon;

	public Image Properties;

	public Text Properties_SizeLabel;

	public Slider Properties_Size;

	public Text Properties_HardnessLabel;

	public Slider Properties_Hardness;

	public Text Properties_OpacityLabel;

	public Slider Properties_Opacity;

	public Button Brush;

	public Image Brush_Background;

	public Image Brush_Fill;

	public Image Buttons;

	public Button Buttons_CancelExit;

	public Text Buttons_CancelExit_Text;

	public Button Buttons_Apply;

	public Text Buttons_Apply_Text;

	public ColorPicker ColorPicker;

	public Button ColorPicker_Background;

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
			PineUI.addToggleListeners(Tools_Brush);
			PineUI.addToggleListeners(Tools_Eraser);
			PineUI.addButtonListeners(ToolsExtra_Fill);
			PineUI.addSliderListeners(Properties_Size);
			PineUI.addSliderListeners(Properties_Hardness);
			PineUI.addSliderListeners(Properties_Opacity);
			PineUI.addButtonListeners(Brush);
			PineUI.addButtonListeners(Buttons_CancelExit);
			PineUI.addButtonListeners(Buttons_Apply);
			PineUI.addButtonListeners(ColorPicker_Background);
		}
	}
}
