using UnityEngine.UI;

public class AudioItemUI : PineUIComponent
{
	public Button root;

	public RawImage Texture;

	public Button PlayButton;

	public Text PlayButton_Text;

	public Button StopButton;

	public Text StopButton_Text;

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
