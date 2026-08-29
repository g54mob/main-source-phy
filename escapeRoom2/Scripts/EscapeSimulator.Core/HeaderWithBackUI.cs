using UnityEngine.UI;

public class HeaderWithBackUI : PineUIComponent
{
	public Image root;

	public Button Back;

	public Text Text;

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
			PineUI.addButtonListeners(Back);
		}
	}
}
