using UnityEngine.UI;

public class PropertiesHeaderUI : PineUIComponent
{
	public Image root;

	public Text Text;

	public Button Fold;

	public Image Fold_In;

	public Image Fold_Out;

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
