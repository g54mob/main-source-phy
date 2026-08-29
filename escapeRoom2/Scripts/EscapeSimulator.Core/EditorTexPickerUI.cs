using UnityEngine;
using UnityEngine.UI;

public class EditorTexPickerUI : PineUIComponent
{
	public Canvas root;

	public Button Background;

	public Image Popup;

	public Image Viewport;

	public RectTransform Content;

	public PickTexItemUI PickTexItem;

	public Image Header;

	public Button Header_Folder;

	public Image Header_Folder_Icon;

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
			PineUI.addButtonListeners(Header_Folder);
		}
	}
}
