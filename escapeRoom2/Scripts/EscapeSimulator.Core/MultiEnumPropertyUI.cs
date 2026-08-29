using UnityEngine;
using UnityEngine.UI;

public class MultiEnumPropertyUI : PineUIComponent
{
	public RectTransform root;

	public Button Label;

	public Text Label_Text;

	public Dropdown Content_Dropdown;

	public Text Content_Dropdown_Label;

	public Image Content_Dropdown_Label_newIcon;

	public Image Content_Dropdown_Label_BGArrow_Arrow;

	public Image Content_Dropdown_Template;

	public Image Content_Dropdown_Template_Viewport;

	public Image Content_Dropdown_Template_Viewport_Content;

	public Image Content_Dropdown_Template_Viewport_Content_Item_ItemBackground;

	public Image Content_Dropdown_Template_Viewport_Content_Item_ItemCheckmark;

	public Text Content_Dropdown_Template_Viewport_Content_Item_ItemLabel;

	public Image Content_Dropdown_Template_Viewport_Content_Item_ItemLabel_Image;

	public Image Content_Dropdown_Template_ES2ScrollbarVertical;

	public Image Content_Dropdown_Template_ES2ScrollbarVertical_BGImage;

	public Image Content_Dropdown_Template_ES2ScrollbarVertical_SlidingArea_Handle;

	public Image Content_Dropdown_Template_ES2ScrollbarVertical_SlidingArea_Handle_BGHandle;

	public Button Content_Button;

	public Image Content_Button_Icon;

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
