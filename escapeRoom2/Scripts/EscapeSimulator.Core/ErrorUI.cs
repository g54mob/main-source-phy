using UnityEngine;
using UnityEngine.UI;

public class ErrorUI : PineUIComponent
{
	public Canvas root;

	public Text Title;

	public Text Message;

	public Transform btnTemplate;

	public Button btnTemplate_errorButton;

	public Text btnTemplate_errorButton_Text;

	public Image btnTemplate_errorControllerHint;

	public Text btnTemplate_errorControllerHint_Text;

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
			PineUI.addButtonListeners(btnTemplate_errorButton);
		}
	}
}
