using UnityEngine.UI;

public class PickTexItemUI : PineUIComponent
{
	public Button root;

	public RawImage Texture;

	public Button FixButton;

	public Text FixButton_Text;

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
