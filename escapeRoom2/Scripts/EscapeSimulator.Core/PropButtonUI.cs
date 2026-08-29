using UnityEngine.UI;

public class PropButtonUI : PineUIComponent
{
	public Button root;

	public Image Background;

	public Image Selected;

	public Image Icon;

	public Text Name;

	public Button Favorite;

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
		}
	}
}
