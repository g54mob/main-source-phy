using UnityEngine.UI;

public class FoldoutPropertyUI : PineUIComponent
{
	public Button root;

	public Image Folded;

	public Image Unfolded;

	public Text Label;

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
