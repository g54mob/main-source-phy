using UnityEngine.UI;

public class RoomToolButtonUI : PineUIComponent
{
	public Button root;

	public Image Background;

	public Image Selected;

	public RawImage Icon;

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
