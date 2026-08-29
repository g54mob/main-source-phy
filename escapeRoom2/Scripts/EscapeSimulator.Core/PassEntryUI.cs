using UnityEngine.UI;

public class PassEntryUI : PineUIComponent
{
	public Button root;

	public Image Background;

	public Image Disabled;

	public Image Selected;

	public Text Text;

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
