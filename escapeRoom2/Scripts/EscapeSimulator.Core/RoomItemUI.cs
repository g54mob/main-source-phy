using UnityEngine.UI;

public class RoomItemUI : PineUIComponent
{
	public Button root;

	public Text Name;

	public Text Name_Path;

	public Text Description;

	public Image Preview;

	public Button Load;

	public Text Load_Text;

	public Button X;

	public Text X_Text;

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
			PineUI.addButtonListeners(X);
		}
	}
}
