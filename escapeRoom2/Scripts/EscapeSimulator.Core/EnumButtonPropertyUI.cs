using UnityEngine;
using UnityEngine.UI;

public class EnumButtonPropertyUI : PineUIComponent
{
	public RectTransform root;

	public Button Label;

	public Text Label_Text;

	public Dropdown Dropdown;

	public Text Dropdown_Label;

	public Image Dropdown_Label_newIcon;

	public Image Dropdown_Label_BGArrow_Arrow;

	public Image Dropdown_Template;

	public Image Dropdown_Template_Viewport;

	public Image Dropdown_Template_Viewport_Content;

	public Image Dropdown_Template_Viewport_Content_Item_ItemBackground;

	public Image Dropdown_Template_Viewport_Content_Item_ItemCheckmark;

	public Text Dropdown_Template_Viewport_Content_Item_ItemLabel;

	public Image Dropdown_Template_Viewport_Content_Item_ItemLabel_Image;

	public Image Dropdown_Template_ES2ScrollbarVertical;

	public Image Dropdown_Template_ES2ScrollbarVertical_BGImage;

	public Image Dropdown_Template_ES2ScrollbarVertical_SlidingArea_Handle;

	public Image Dropdown_Template_ES2ScrollbarVertical_SlidingArea_Handle_BGHandle;

	public Button Button;

	public Image Button_Icon;

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
