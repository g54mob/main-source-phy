using UnityEngine;
using UnityEngine.UI;

public class EditorImportUI : PineUIComponent
{
	public Canvas root;

	public Button Background;

	public Image Popup;

	public Image Header;

	public Button Header_Back;

	public Image Header_Back_Icon;

	public Button Header_Apply;

	public Image Header_Apply_Icon;

	public Text Header_Title;

	public Image ScrollRect;

	public Image ViewportRooms;

	public RectTransform ViewportRooms_Content;

	public RoomItemUI ViewportRooms_RoomItem;

	public Image ViewportEntries;

	public RectTransform ViewportEntries_Content;

	public ImportItemUI ViewportEntries_ImportItem;

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
			PineUI.addButtonListeners(Background);
			PineUI.addButtonListeners(Header_Back);
			PineUI.addButtonListeners(Header_Apply);
		}
	}
}
