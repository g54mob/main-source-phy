using UnityEngine.UI;

public class CCVariantUI : PineUIComponent
{
	public CCVariantButton root;

	public Image Image;

	public Image HoverFrame;

	public Image SelectionFrame;

	public Image Locked;

	public Image Locked_Image;

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
			PineUI.addButtonListeners(root);
		}
	}
}
