using UnityEngine;
using UnityEngine.UI;

public class ZoomCanvasUI : PineUIComponent
{
	public Canvas root;

	public Button NormalActions_PinRight;

	public Image NormalActions_PinRight_Background;

	public Image NormalActions_PinRight_Container;

	public Image NormalActions_PinRight_Container_Image;

	public Button NormalActions_PinLeft;

	public Image NormalActions_PinLeft_Background;

	public Image NormalActions_PinLeft_Container;

	public Image NormalActions_PinLeft_Container_Image;

	public Image ClosePinFirstBackground;

	public Text ClosePinFirstBackground_ClosePinFirstText;

	public CanvasGroup ItemNameBG;

	public Text ItemNameBG_ItemName;

	public Button ItemNameBG_ItemName_ItemKey;

	public Button ItemNameBG_ItemName_ItemHint;

	public Button ItemNameBG_ItemName_ItemTool;

	public Button ItemNameBG_ItemName_ItemContainer;

	public Button ItemNameBG_ItemName_ItemTrashcan;

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
			PineUI.addButtonListeners(NormalActions_PinRight);
			PineUI.addButtonListeners(NormalActions_PinLeft);
			PineUI.addButtonListeners(ItemNameBG_ItemName_ItemKey);
			PineUI.addButtonListeners(ItemNameBG_ItemName_ItemHint);
			PineUI.addButtonListeners(ItemNameBG_ItemName_ItemTool);
			PineUI.addButtonListeners(ItemNameBG_ItemName_ItemContainer);
			PineUI.addButtonListeners(ItemNameBG_ItemName_ItemTrashcan);
		}
	}
}
