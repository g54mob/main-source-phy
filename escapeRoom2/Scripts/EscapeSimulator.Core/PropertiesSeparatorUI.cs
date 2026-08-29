using UnityEngine.UI;

public class PropertiesSeparatorUI : PineUIComponent
{
	public Image root;

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
