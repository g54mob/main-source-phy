using UnityEngine.UI;

public class AssetBrowserTabItemUI : PineUIComponent
{
	public Button root;

	public Image Icon;

	public Image Selected;

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
