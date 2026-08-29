using UnityEngine;
using UnityEngine.UI;

public class RectSelectionUI : PineUIComponent
{
	public Canvas root;

	public Button Cancel;

	public Image Rect;

	public Image Rect_Fill;

	public Text Rect_Text;

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
			PineUI.addButtonListeners(Cancel);
		}
	}
}
