using UnityEngine;
using UnityEngine.UI;

public class PasswordPropertyUI : PineUIComponent
{
	public RectTransform root;

	public Button Label;

	public Text Label_Text;

	public PasswordElementUI PasswordElement;

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
			PineUI.addButtonListeners(Label);
		}
	}
}
