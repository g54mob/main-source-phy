using UnityEngine;
using UnityEngine.UI;

public class EditorAssetBrowserUI : PineUIComponent
{
	public Canvas root;

	public Button Background;

	public Image Header;

	public Text Header_Title;

	public Image Header_Search;

	public InputField Header_Search_Input;

	public Button Header_Folder;

	public Image Header_Folder_Icon;

	public CanvasGroup Header_AssetTypeTabs;

	public Image Header_AssetTypeTabs_Line;

	public AssetBrowserTabItemUI Header_AssetTypeTabs_AssetBrowserTabItem;

	public Button Header_Quit;

	public Image Popup;

	public Image Viewport;

	public GridLayoutGroup Content;

	public TexItemUI TexItem;

	public AudioItemUI AudioItem;

	public ScriptItemUI ScriptItem;

	public AddNewItemUI AddNewItem;

	public CustomModelItemUI CustomModelItem;

	public MaterialItemUI MaterialItem;

	public FolderItemUI FolderItem;

	public Image ES2ScrollbarVertical;

	public Image ES2ScrollbarVertical_BGImage;

	public Image ES2ScrollbarVertical_SlidingArea_Handle;

	public Image ES2ScrollbarVertical_SlidingArea_Handle_BGHandle;

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
			PineUI.addButtonListeners(Header_Folder);
			PineUI.addButtonListeners(Header_Quit);
		}
	}
}
