using UnityEngine.UI;

public class TagButtonUI : PineUIComponent
{
	public Button root;

	public Image Background;

	public Image Icon;

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
		}
	}
}
