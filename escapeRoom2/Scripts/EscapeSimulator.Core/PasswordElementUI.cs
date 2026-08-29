using UnityEngine.UI;

public class PasswordElementUI : PineUIComponent
{
	public InputField root;

	public Text Text;

	public Button Remove;

	public Image Remove_Background;

	public Image Remove_Icon;

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
			PineUI.addButtonListeners(Remove);
		}
	}
}
