using UnityEngine;
using UnityEngine.UI;

public class OtherPlayerInventoryCanvasUI : PineUIComponent
{
	public Canvas root;

	public CanvasGroup PlayerNameBg;

	public Text PlayerNameBg_PlayerName;

	public GridContainer Container;

	public SlotTemplateUI Container_SlotTemplate;

	public Image Container_Empty;

	public Image Container_Empty_HoverBG;

	public Image Container_Empty_Selected;

	public Text Container_Empty_ItemName;

	public Image Container_HoverMarker;

	public Image Container_SelectionMarker;

	public Image Container_PreciseMarker;

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
