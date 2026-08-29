using UnityEngine.UI;

public class PackUI : PineUIComponent
{
	public Button root;

	public Text Name;

	public Image Name_IsNew;

	public Image SelectionIndicator;

	public Image Line;

	public Text PreviewLbl;

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
