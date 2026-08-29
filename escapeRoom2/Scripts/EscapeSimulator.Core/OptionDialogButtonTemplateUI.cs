using UnityEngine;
using UnityEngine.UI;

public class OptionDialogButtonTemplateUI : PineUIComponent
{
	public RectTransform root;

	public Button Button;

	public Text Button_Text;

	public Image Controller;

	public Image Controller_ControllerHint;

	public Text Controller_ControllerHint_Text;

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
			PineUI.addButtonListeners(Button);
		}
	}
}
