using UnityEngine;
using UnityEngine.UI;

public class SlotTemplateUI : PineUIComponent
{
	public Image root;

	public Image HoverBG;

	public Image Recenter;

	public Image Selected;

	public Image Hover;

	public CanvasGroup Hover_StreamlinedUI;

	public ItemUI Hover_StreamlinedUI_StreamlinedtemNameBG;

	public Text Hover_StreamlinedUI_StreamlinedtemNameBG_ItemName;

	public Button Hover_StreamlinedUI_StreamlinedtemNameBG_ItemKey;

	public Button Hover_StreamlinedUI_StreamlinedtemNameBG_ItemHint;

	public Button Hover_StreamlinedUI_StreamlinedtemNameBG_ItemContainer;

	public Button Hover_StreamlinedUI_StreamlinedtemNameBG_ItemTrashcan;

	public Image Hover_StreamlinedUI_StreamlinedtemNameBG_Underline;

	public Image Hover_StreamlinedUI_StreamlinedHoverActions_Left_Image;

	public Text Hover_StreamlinedUI_StreamlinedHoverActions_Left_leftLbl;

	public Image Hover_StreamlinedUI_StreamlinedHoverActions_Right_Image;

	public Text Hover_StreamlinedUI_StreamlinedHoverActions_Right_rightLbl;

	public CanvasGroup Hover_StreamlinedUIAnchoredLeft;

	public Button Pocket;

	public Image Circle;

	public Image CollectionBackground;

	public Text CollectionBackground_CollectionCount;

	public Image CollectionBackgroundPC;

	public Text CollectionBackgroundPC_CollectionCount;

	public Button PinIcon;

	public Button ShirtIcon;

	public Text Shortcut;

	public ItemUI InventoryItemUI;

	public Text InventoryItemUI_ItemName;

	public Button InventoryItemUI_ItemKey;

	public Button InventoryItemUI_ItemHint;

	public Button InventoryItemUI_ItemContainer;

	public Button InventoryItemUI_ItemTrashcan;

	public Image PingBg;

	public Image PingBg_PingCircle;

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
			PineUI.addButtonListeners(Hover_StreamlinedUI_StreamlinedtemNameBG_ItemKey);
			PineUI.addButtonListeners(Hover_StreamlinedUI_StreamlinedtemNameBG_ItemHint);
			PineUI.addButtonListeners(Hover_StreamlinedUI_StreamlinedtemNameBG_ItemContainer);
			PineUI.addButtonListeners(Hover_StreamlinedUI_StreamlinedtemNameBG_ItemTrashcan);
			PineUI.addButtonListeners(Pocket);
			PineUI.addButtonListeners(PinIcon);
			PineUI.addButtonListeners(ShirtIcon);
			PineUI.addButtonListeners(InventoryItemUI_ItemKey);
			PineUI.addButtonListeners(InventoryItemUI_ItemHint);
			PineUI.addButtonListeners(InventoryItemUI_ItemContainer);
			PineUI.addButtonListeners(InventoryItemUI_ItemTrashcan);
		}
	}
}
