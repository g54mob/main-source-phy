using UnityEngine.UI;

public class CCGroupTemplateUI : PineUIComponent
{
	public Button root;

	public Text Title;

	public Image SelectionIndicator;

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
