using UnityEngine.UI;

public class EmotePickUI : PineUIComponent
{
	public Button root;

	public Image Arc;

	public Image Icon;

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
