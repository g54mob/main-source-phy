using UnityEngine.UI;

public class CCImageVariantUI : PineUIComponent
{
	public Image root;

	public Button ImageButton;

	public Image ImageButton_Image;

	public Image SelectionFrame;

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
			PineUI.addButtonListeners(ImageButton);
		}
	}
}
