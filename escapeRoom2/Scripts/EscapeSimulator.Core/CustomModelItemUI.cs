using UnityEngine.UI;

public class CustomModelItemUI : PineUIComponent
{
	public Button root;

	public RawImage Texture;

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
