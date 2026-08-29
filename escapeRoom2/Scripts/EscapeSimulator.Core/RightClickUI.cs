using UnityEngine;
using UnityEngine.UI;

public class RightClickUI : PineUIComponent
{
	public Canvas root;

	public Image Rect;

	public RectTransform Rect_Content;

	public Button Rect_Content_Item;

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
			PineUI.addButtonListeners(Rect_Content_Item);
		}
	}
}
