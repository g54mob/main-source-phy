using UnityEngine.UI;

public class ImportItemUI : PineUIComponent
{
	public Button root;

	public Text Label;

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
