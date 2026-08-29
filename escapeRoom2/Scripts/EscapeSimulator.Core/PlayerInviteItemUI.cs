using UnityEngine.UI;

public class PlayerInviteItemUI : PineUIComponent
{
	public Button root;

	public RawImage Avatar;

	public Image ControllerSelected;

	public Text Name;

	public Text online;

	public Text inGame;

	public Text offline;

	public Text InES2;

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
