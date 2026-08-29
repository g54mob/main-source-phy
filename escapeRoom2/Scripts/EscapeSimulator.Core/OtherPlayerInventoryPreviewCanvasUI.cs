using UnityEngine;
using UnityEngine.UI;

public class OtherPlayerInventoryPreviewCanvasUI : PineUIComponent
{
	public CanvasGroup root;

	public Image Container;

	public SlotTemplateUI Container_SlotTemplate;

	public Image Container_Empty;

	public Text Container_EmptyTxt;

	public Image Container_Top;

	public Text Container_Top_PlayerName;

	public Image Container_Top_Info_Image1;

	public Text Container_Top_Info_HintAmount;

	public Image Container_Top_Info_Image;

	public Text Container_Top_Info_KeyAmount;

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
