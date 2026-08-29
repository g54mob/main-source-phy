using UnityEngine.UI;

public class TexItemUI : PineUIComponent
{
	public Button root;

	public RawImage Texture;

	public Button FixButton;

	public Text FixButton_Text;

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
		}
	}
}
