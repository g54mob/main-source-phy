using UnityEngine.UI;

public class PasswordButtonUI : PineUIComponent
{
	public Button root;

	public Image Selected;

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
			PineUI.addButtonListeners(root);
		}
	}
}
