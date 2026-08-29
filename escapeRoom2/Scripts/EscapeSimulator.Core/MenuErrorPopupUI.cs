using UnityEngine.UI;

public class MenuErrorPopupUI : PineUIComponent
{
	public Button root;

	public Text errorTitle;

	public Text errorLabel;

	public Image Button;

	public Text Button_Title;

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
